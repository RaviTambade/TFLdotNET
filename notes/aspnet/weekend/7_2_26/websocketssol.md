## **Step-by-step C# solution** for the **Live Patient Monitoring Dashboard using WebSockets**, designed for **.NET / ASP.NET Core students**.

This teaches:

- 👉 Architecture
- 👉 Real-time engineering
- 👉 Healthcare responsibility
- 👉 Industry practices


## Live Patient Monitoring System Using WebSockets

> *“In .NET you are not just writing programs.
> You are building enterprise systems.”*

# ✅ STEP 1: Understand Why WebSockets (Concept First)

### Problem

Hospital needs:

* Continuous updates
* Low latency
* Multiple clients
* No polling

### Conclusion

REST ❌
WebSocket ✅

Because:

- ✔ Persistent connection
- ✔ Server → Client push
- ✔ Bi-directional
- ✔ Low delay


# ✅ STEP 2: System Architecture (Design First)

```
Patient Sensors (Simulated)
        ↓
ASP.NET Core Server
(WebSocket Server)
        ↓
-----------------------
Doctor Dashboard
Nurse Dashboard
Admin Dashboard
-----------------------
(Web Clients / Desktop Clients)
```



# ✅ STEP 3: Create ASP.NET Core WebSocket Server

### Create Project

```bash
dotnet new web -n HospitalMonitor
cd HospitalMonitor
```



# ✅ STEP 4: Enable WebSockets in Program.cs

### Program.cs

```csharp
using System.Net.WebSockets;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.UseWebSockets();

var clients = new List<WebSocket>();

app.Map("/ws", async context =>
{
    if (context.WebSockets.IsWebSocketRequest)
    {
        var socket = await context.WebSockets.AcceptWebSocketAsync();
        clients.Add(socket);

        Console.WriteLine("Client Connected");

        await ReceiveLoop(socket, clients);
    }
    else
    {
        context.Response.StatusCode = 400;
    }
});

app.Run();

async Task ReceiveLoop(WebSocket socket, List<WebSocket> clients)
{
    var buffer = new byte[1024];

    try
    {
        while (socket.State == WebSocketState.Open)
        {
            await socket.ReceiveAsync(buffer, CancellationToken.None);
            await Task.Delay(1000);
        }
    }
    finally
    {
        clients.Remove(socket);
        await socket.CloseAsync(
            WebSocketCloseStatus.NormalClosure,
            "Closed",
            CancellationToken.None);
    }
}
```

👉 This creates a WebSocket endpoint at:

```
ws://localhost:5000/ws
```


# ✅ STEP 5: Add Patient Data Generator + Broadcast

Now we add **live data streaming**.

### Update Program.cs (Full Version)

```csharp
using System.Net.WebSockets;
using System.Text;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.UseWebSockets();

var clients = new List<WebSocket>();

app.Map("/ws", async context =>
{
    if (context.WebSockets.IsWebSocketRequest)
    {
        var socket = await context.WebSockets.AcceptWebSocketAsync();
        clients.Add(socket);

        Console.WriteLine("Client Connected");

        await Listen(socket, clients);
    }
    else
    {
        context.Response.StatusCode = 400;
    }
});

_ = Task.Run(async () =>
{
    while (true)
    {
        var data = GeneratePatientData();

        var json = JsonSerializer.Serialize(data);
        var bytes = Encoding.UTF8.GetBytes(json);

        await Broadcast(bytes, clients);

        await Task.Delay(2000);
    }
});

app.Run();


// ---------------- Helper Methods ----------------

async Task Broadcast(byte[] data, List<WebSocket> clients)
{
    foreach (var client in clients.ToList())
    {
        if (client.State == WebSocketState.Open)
        {
            await client.SendAsync(
                data,
                WebSocketMessageType.Text,
                true,
                CancellationToken.None);
        }
    }
}

async Task Listen(WebSocket socket, List<WebSocket> clients)
{
    var buffer = new byte[1024];

    try
    {
        while (socket.State == WebSocketState.Open)
        {
            await socket.ReceiveAsync(buffer, CancellationToken.None);
        }
    }
    finally
    {
        clients.Remove(socket);
    }
}

PatientData GeneratePatientData()
{
    var rand = new Random();

    return new PatientData
    {
        PatientId = "P1023",
        HeartRate = rand.Next(60, 100),
        Oxygen = rand.Next(95, 100),
        Status = "Stable",
        Time = DateTime.Now
    };
}

// ---------------- Model ----------------

class PatientData
{
    public string PatientId { get; set; }
    public int HeartRate { get; set; }
    public int Oxygen { get; set; }
    public string Status { get; set; }
    public DateTime Time { get; set; }
}
```

# ✅ STEP 6: Build C# WebSocket Client (Console App)

### Create Client Project

```bash
dotnet new console -n MonitorClient
cd MonitorClient
```


### Program.cs (Client)

```csharp
using System.Net.WebSockets;
using System.Text;

var socket = new ClientWebSocket();

await socket.ConnectAsync(
    new Uri("ws://localhost:5000/ws"),
    CancellationToken.None);

Console.WriteLine("Connected to Server");

var buffer = new byte[1024];

while (socket.State == WebSocketState.Open)
{
    var result = await socket.ReceiveAsync(buffer, CancellationToken.None);

    var message = Encoding.UTF8.GetString(
        buffer, 0, result.Count);

    Console.WriteLine("Patient Update:");
    Console.WriteLine(message);
    Console.WriteLine("---------------------");
}
```



# ✅ STEP 7: Run & Test System

### Steps

1️⃣ Run Server

```bash
dotnet run
```

2️⃣ Run Multiple Clients

```bash
dotnet run
```

(open in multiple terminals)

3️⃣ Observe:

- ✔ All clients receive updates
- ✔ Same data
- ✔ Every 2 seconds

# ✅ STEP 8: Demonstrate Stateful Connection

### Activity

* Close client
* Server logs removal
* Restart client
* Reconnects

Explain:

WebSocket = Session
REST = No memory

# ✅ STEP 9: Add Reconnection (Optional)

### Client Enhancement

```csharp
async Task Connect()
{
    while (true)
    {
        try
        {
            await socket.ConnectAsync(uri, CancellationToken.None);
            break;
        }
        catch
        {
            await Task.Delay(3000);
        }
    }
}
```



# ✅ STEP 10: Security Discussion (Healthcare)

Explain:
 
✅ JWT auth
✅ Role access
✅ Audit logs
✅ HIPAA-like compliance

 
# ✅ STEP 11: Testing Scenarios

| Case         | Expected           |
| ------------ | ------------------ |
| 10 clients   | All updated        |
| Server crash | Clients disconnect |
| Restart      | Reconnect          |
| Bad packet   | Ignored            |

 

# 🌟 Advanced Extension (For Strong Students)

Add:

- ✅ SignalR
- ✅ Database logging
- ✅ Alert thresholds
- ✅ Mobile app client
- ✅ Load balancer

 
# 🌺 Mentor’s Closing Message

> *Dear Student, this project is not about WebSockets.*
> *It is about learning how real hospitals trust software.*

> *Code carefully. Lives may depend on it.*