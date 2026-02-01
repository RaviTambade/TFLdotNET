## **Lab Handbook: Polyglot gRPC Communication**

### *Node.js Client ↔ ASP.NET Core gRPC Server*

## 1. Lab Overview

**Lab Type:** Mentor‑guided, hands‑on system integration
**TFL Stage:** Service Communication & System Integration
**Duration:** 2–3 Hours

**Difficulty:** Intermediate
**Outcome Focus:** System‑level thinking, contract‑first design, polyglot integration

## 2. Problem Statement (Given to Students)

> Two engineering teams are building a healthcare system.
>
> * Team A builds the **core hospital service** using **ASP.NET Core**.
> * Team B builds a **client system** using **Node.js**.
>
> Both teams must communicate **efficiently, reliably, and language‑independently**.
>
> **Your task:** Enable communication using **gRPC**.

## 3. Learning Objectives (TFL Aligned)

By completing this lab, the learner will be able to:

* Design a **contract-first service** using Protocol Buffers
* Implement a **gRPC server** in ASP.NET Core
* Consume the service from a **Node.js gRPC client**
* Explain **why gRPC is preferred over REST** for internal systems
* Visualize service boundaries and responsibilities

## 4. Architecture View

```
Node.js Client (Reception Desk)
        |
        | gRPC (HTTP/2 + Protobuf)
        v
ASP.NET Core gRPC Server (Hospital System)
```

**Mentor Note:**

> This is *service‑to‑service communication*, not UI‑to‑server.

## 5. Technology Stack

| Layer     | Technology                |
| --------- | ------------------------- |
| Contract  | Protocol Buffers (.proto) |
| Server    | ASP.NET Core gRPC         |
| Client    | Node.js gRPC              |
| Transport | HTTP/2                    |


## 6. Lab Module 1 – Contract Design (Proto)
### Objective

Create a language‑neutral service contract.

### File

`greeter.proto`

```proto
syntax = "proto3";

package hospital;

service AppointmentService {
  rpc GreetPatient (PatientRequest) returns (PatientReply);
}

message PatientRequest {
  string patientName = 1;
}

message PatientReply {
  string message = 1;
}
```

### TFL Thinking Point

> The **contract is senior to the code**.

## 7. Lab Module 2 – ASP.NET Core gRPC Server

### Objective

Implement the hospital service.

### Key Responsibilities

* Accept request
* Apply business logic
* Return response

### Service Implementation

```csharp
public class AppointmentService : AppointmentServiceBase
{
    public override Task<PatientReply> GreetPatient(
        PatientRequest request,
        ServerCallContext context)
    {
        return Task.FromResult(new PatientReply
        {
            Message = $"Welcome {request.PatientName}, your appointment is confirmed."
        });
    }
}
```

### Mentor Insight

> Server owns **rules, truth, and validation**.


## 8. Lab Module 3 – Node.js gRPC Client

### Objective

Consume the remote service as if it were a local method.

### Client Invocation

```js
client.GreetPatient(
  { patientName: "Amit" },
  (err, response) => {
    console.log(response.message);
  }
);
```

### Teaching Analogy

> This looks like a function call.
> It’s actually **crossing machines**.


## 9. Execution Steps

1. Start ASP.NET Core gRPC server
2. Verify server is listening
3. Run Node.js client
4. Observe response from server

**Expected Output:**

```
Welcome Amit, your appointment is confirmed.
```

## 10. Reflection Questions (Mandatory)

Students must answer:

1. Why not REST for this use case?
2. What problem does Protocol Buffers solve?
3. Who controls the contract – client or server?
4. Where is gRPC used in real companies?

## 11. Assignments & Extensions

### Level 1 (Easy)

* Add hospital name to response

### Level 2 (Medium)

Add fields:

```proto
int32 age = 2;
string department = 3;
```

### Level 3 (Advanced)

* Implement **server streaming**

## 12. Assessment Criteria (TFL Style)

| Area          | Evaluation           |
| ------------- | -------------------- |
| Contract      | Correct proto design |
| Server        | Logic clarity        |
| Client        | Proper invocation    |
| Concepts      | Explanation ability  |
| Communication | System articulation  |

> **TFL Rule:** If you cannot explain it, you have not learned it.

## 13. Industry Mapping

| Industry Practice | Lab Mapping        |
| ----------------- | ------------------ |
| Microservices     | gRPC communication |
| Polyglot teams    | Node + .NET        |
| Internal APIs     | Binary protocols   |


## 14. Mentor Closing Note

> You did not build an API.
> You designed **communication between systems**.

<hr/>

# 🧭 Step-by-Step Instructions

## **TLF Lab: Polyglot gRPC Communication**

### *Node.js Client ↔ ASP.NET Core gRPC Server*



## Before You Start (Prerequisites)

Ensure the following are installed:

* ✅ .NET SDK 7 or later
* ✅ Node.js 18 or later
* ✅ npm (comes with Node.js)
* ✅ Basic knowledge of C# and JavaScript

## Step 1: Understand the Problem (DO NOT CODE YET)

Read the problem statement carefully:

> A Node.js client must communicate with an ASP.NET Core server using gRPC.

### What you should understand:

* Two **different languages**
* One **shared contract**
* Communication must be **fast and reliable**

✋ Mentor Rule: *If you don’t understand the problem, don’t touch the keyboard.*


## Step 2: Design the Contract (Protocol Buffers)

### 2.1 Create a working folder

```
TLF-gRPC-Lab/
```

### 2.2 Create proto folder

```
TLF-gRPC-Lab/protos
```

### 2.3 Create the proto file

📄 `protos/appointment.proto`

```proto
syntax = "proto3";

package hospital;

service AppointmentService {
  rpc GreetPatient (PatientRequest) returns (PatientReply);
}

message PatientRequest {
  string patientName = 1;
}

message PatientReply {
  string message = 1;
}
```

### What you achieved

* Defined **service**
* Defined **request message**
* Defined **response message**

🧠 Think: *This file is shared by all teams.*


## Step 3: Create ASP.NET Core gRPC Server

### 3.1 Create gRPC project

```bash
dotnet new grpc -n HospitalGrpcServer
cd HospitalGrpcServer
```

### 3.2 Replace default proto

* Delete existing proto file
* Copy `appointment.proto` into `Protos/`

### 3.3 Update project file

📄 `HospitalGrpcServer.csproj`

```xml
<ItemGroup>
  <Protobuf Include="Protos\appointment.proto" GrpcServices="Server" />
</ItemGroup>
```


## Step 4: Implement gRPC Service

### 4.1 Create service class

📄 `Services/AppointmentServiceImpl.cs`

```csharp
using Grpc.Core;
using Hospital;

public class AppointmentServiceImpl : AppointmentService.AppointmentServiceBase
{
    public override Task<PatientReply> GreetPatient(
        PatientRequest request,
        ServerCallContext context)
    {
        return Task.FromResult(new PatientReply
        {
            Message = $"Welcome {request.PatientName}, your appointment is confirmed."
        });
    }
}
```

### What this step teaches

* Server owns **business logic**
* Client only requests


## Step 5: Register gRPC Service

### 5.1 Open Program.cs

```csharp
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddGrpc();

var app = builder.Build();

app.MapGrpcService<AppointmentServiceImpl>();
app.MapGet("/", () => "gRPC Server Running");

app.Run();
```

### 5.2 Run the server

```bash
dotnet run
```

✔ Server is now running on `https://localhost:5001`


## Step 6: Create Node.js Client

### 6.1 Create client folder

```
TLF-gRPC-Lab/node-client
cd node-client
```

### 6.2 Initialize Node project

```bash
npm init -y
```

### 6.3 Install gRPC dependencies

```bash
npm install @grpc/grpc-js @grpc/proto-loader
```


## Step 7: Add Proto to Node Client

### 7.1 Create proto folder

```
node-client/protos
```

### 7.2 Copy `appointment.proto` into this folder

🧠 Rule: *Client and Server must use the same contract.*


## Step 8: Write Node.js Client Code

📄 `client.js`

```js
const grpc = require('@grpc/grpc-js');
const protoLoader = require('@grpc/proto-loader');
const path = require('path');

const packageDef = protoLoader.loadSync(
  path.join(__dirname, 'protos/appointment.proto')
);

const grpcObj = grpc.loadPackageDefinition(packageDef);
const hospital = grpcObj.hospital;

const client = new hospital.AppointmentService(
  'localhost:5001',
  grpc.credentials.createInsecure()
);

client.GreetPatient({ patientName: 'Amit' }, (err, response) => {
  if (err) {
    console.error(err);
    return;
  }
  console.log(response.message);
});
```


## Step 9: Execute the Client

```bash
node client.js
```

### Expected Output

```
Welcome Amit, your appointment is confirmed.
```

🎉 Congratulations! Communication is successful.


## Step 10: Reflection (MANDATORY)

Answer in your own words:

1. Why is proto shared?
2. Why not REST here?
3. What happens if proto changes?
4. Who owns validation logic?

## Step 11: Optional Enhancements

* Add age & department fields
* Change message format
* Add streaming support
* Secure with TLS


## Final TLF Message

> You didn’t connect two programs.
> You connected **two teams**.
