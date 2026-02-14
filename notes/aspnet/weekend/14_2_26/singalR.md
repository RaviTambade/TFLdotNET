

# 🎯 SignalR Lab
Let us build:

- ✅ One Web API
- ✅ One SignalR Hub
- ✅ One HTML client
- ✅ Real-time chat


When one user sends a message:

👉 All connected users see it **instantly** (no refresh)

That is **SignalR = Real-Time Communication**.


# ✅ Step 1: Create Project

Open terminal / command prompt:

```bash
dotnet new webapi -n SignalRDemo
cd SignalRDemo
```


# ✅ Step 2: Install SignalR Package

```bash
dotnet add package Microsoft.AspNetCore.SignalR
```


# ✅ Step 3: Create SignalR Hub

Create folder: `Hubs`

📁 `Hubs/ChatHub.cs`

```csharp
using Microsoft.AspNetCore.SignalR;

namespace SignalRDemo.Hubs
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }
    }
}
```

### 👉 What this does

```
Client → SendMessage()
Server → Broadcast → All Clients
```


# ✅ Step 4: Configure SignalR in Program.cs

Open `Program.cs`

### Add this line:

```csharp
using SignalRDemo.Hubs;
```

### Modify Program.cs like this:

```csharp
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddSignalR();

var app = builder.Build();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

// Register Hub
app.MapHub<ChatHub>("/chatHub");

app.Run();
```


# ✅ Step 5: Add Simple Client (HTML Page)

Create folder: `wwwroot`

📁 `wwwroot/chat.html`

```html
<!DOCTYPE html>
<html>
<head>
    <title>SignalR Chat</title>

    <script src="https://cdnjs.cloudflare.com/ajax/libs/microsoft-signalr/7.0.5/signalr.min.js"></script>
</head>

<body>

<h2>SignalR Chat Demo</h2>

<input id="user" placeholder="Your Name" />
<br><br>

<input id="message" placeholder="Message" />
<button onclick="send()">Send</button>

<hr>

<ul id="messages"></ul>

<script>

    const connection = new signalR.HubConnectionBuilder()
        .withUrl("/chatHub")
        .build();

    connection.start()
        .then(() => console.log("Connected"))
        .catch(err => console.error(err));

    connection.on("ReceiveMessage", function (user, message) {

        const li = document.createElement("li");

        li.textContent = user + ": " + message;

        document.getElementById("messages").appendChild(li);
    });

    function send() {

        const user = document.getElementById("user").value;
        const message = document.getElementById("message").value;

        connection.invoke("SendMessage", user, message);
    }

</script>

</body>
</html>
```


# ✅ Step 6: Enable Static Files

Open `Program.cs`

Add this line before `app.MapControllers()`:

```csharp
app.UseStaticFiles();
```

Final part should look like:

```csharp
app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseAuthorization();

app.MapControllers();
app.MapHub<ChatHub>("/chatHub");
```


# ✅ Step 7: Run Application

```bash
dotnet run
```

You will see something like:

```
https://localhost:7001
```


# ✅ Step 8: Open Chat in Browser

Open in 2 tabs / 2 browsers:

```
https://localhost:7001/chat.html
```

Now:

- ✔️ Type name
- ✔️ Send message
- ✔️ See instant update

🎉 Real-time chat working!


# 📊 Architecture (Explain to Students)

```
Browser 1 ----\
Browser 2 ----- > SignalR Hub ----> All Clients
Browser 3 ----/
```

Hub = Central Server


# ✅ How SignalR Works (Simple Language)

### 1️⃣ Connection

```js
new HubConnectionBuilder()
```

Creates live connection (WebSocket)


### 2️⃣ Client → Server

```js
connection.invoke("SendMessage")
```

Calls Hub method


### 3️⃣ Server → Clients

```csharp
Clients.All.SendAsync()
```

Broadcasts message


# ✅ Real-World Uses

SignalR is used for:

- ✔️ Chat apps
- ✔️ Live dashboards
- ✔️ Stock prices
- ✔️ Notifications
- ✔️ Online classes
- ✔️ Gaming updates


> “HTTP is like sending letters.
> SignalR is like a phone call.”

```
HTTP    → Request → Response → Close
SignalR → Open → Talk → Talk → Talk
```


# ✅ Mini Exercise for Students

Ask them:

- 1️⃣ Add timestamp to messages
- 2️⃣ Show user count
- 3️⃣ Add private chat
- 4️⃣ Store messages in DB


> SignalR allows .NET apps to send data to browsers **instantly** without refresh.
