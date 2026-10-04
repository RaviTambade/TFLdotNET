# Database as a Service + Infrastructure as a Service

**Mentor:** Ravi Tambade, Chief Mentor – Transflower Learning
**Audience:** TAP Students | Software Developers
**Session:** Public Cloud Fundamentals + Hands-on GCP

### Core Theme

> **Don't just deploy an application. Understand the infrastructure on which your application runs.**



# 1. Mentor Starts With a Question

**Ravi Sir:**

> "Students, we build applications. But one simple question — where does our application actually run?"

**Student:**

> "On a server, Sir."

**Ravi Sir:**

> "Good. Where is that server?"

**Student:**

> "In the company?"

**Ravi Sir:**

> "Always?"

**Student:**

> "It can also be in the cloud."

Exactly.

```text
              APPLICATION
                   |
                   v
          ASP.NET Core / Java
          Python / Node.js
                   |
                   v
                SERVER
                   |
          +--------+--------+
          |                 |
       Physical          Virtual
        Server            Machine
```

And today we will understand:

```text
Physical Infrastructure
          ↓
Virtual Machine
          ↓
Operating System
          ↓
Runtime
          ↓
Application
          ↓
Database
```

# 2. What Is Public Cloud?

Public cloud means computing infrastructure and services are provided by a cloud provider over the internet.

Examples:

* Google Cloud Platform — GCP
* Microsoft Azure
* Amazon Web Services — AWS

```text
                  PUBLIC CLOUD
                       |
       +---------------+---------------+
       |               |               |
      GCP             AWS            Azure
       |               |               |
    Compute          EC2            VM
    Storage          S3             Storage
    Database         RDS            SQL
    Network          VPC            Network
```

# 3. Cloud Service Models

Ravi Sir draws three boxes on the board.

```text
                CLOUD SERVICES
                      |
        +-------------+-------------+
        |             |             |
       SaaS          PaaS          IaaS
        |             |             |
     Software       Platform    Infrastructure
     as a Service   as a Service as a Service
```

And another important cloud service:

```text
                    DBaaS
                     |
          Database as a Service
```

We are going to concentrate on:

```text
IaaS  → Infrastructure as a Service

DBaaS → Database as a Service
```

# 4. IaaS — Infrastructure as a Service

**Ravi Sir:**

> "What do we mean by infrastructure?"

**Students:**

> "Servers, storage, network..."

Correct.

IaaS provides virtual infrastructure.

```text
              IaaS
               |
       +-------+-------+
       |       |       |
     Compute Storage Network
       |       |       |
       VM      Disk     IP
       |
    Linux
       |
    .NET
       |
   Application
```

The cloud provider manages the underlying physical infrastructure. You manage the virtual machine and the software running inside it.



# 5. Traditional Data Center vs Cloud

## Traditional Data Center

```text
Company
   |
   +-- Buy Server
   |
   +-- Install Hardware
   |
   +-- Install Operating System
   |
   +-- Install Database
   |
   +-- Configure Network
   |
   +-- Backup
   |
   +-- Maintain Hardware
```

There is a lot of infrastructure to manage.

## IaaS

```text
Developer
    |
    | Request VM
    v
+-----------------------+
|       GCP             |
|                       |
|   Compute Engine VM   |
|                       |
|   CPU                 |
|   RAM                 |
|   Disk                |
|   Network             |
+-----------------------+
          |
          v
       Debian
          |
          v
       .NET 10
          |
          v
    ASP.NET Core
```

The cloud provider gives us the infrastructure. We configure and manage the virtual machine.


# 6. Virtual Machine — The Practical Part

**Ravi Sir:**

> "Cloud is not magic. Cloud is infrastructure made available to us through software."

We create:

```text
GCP
 |
 +-- Compute Engine
       |
       +-- VM Instance
             |
             +-- CPU
             +-- RAM
             +-- Disk
             +-- Network
             +-- IP Address
```

Now our virtual machine becomes our development and deployment environment.


# 7. Enter Linux

Now the developer becomes familiar with the operating system inside the VM.

```text
GCP
 |
 v
Virtual Machine
 |
 v
Debian Linux
 |
 +-- Files
 +-- Processes
 +-- Memory
 +-- Network
 +-- Users
 +-- Packages
```

First command:

```bash
uname -a
```

Then:

```bash
cat /etc/os-release
```

For our practical environment, the VM was running:

```text
Debian GNU/Linux 13 (trixie)
```

This is an important lesson:

> **Before installing software, understand your operating system.**


# 8. Installing .NET 10

**Ravi Sir:**

> "Students, here is an important lesson. Don't blindly copy and paste commands."

Initially:

```bash
sudo apt install dotnet-sdk-10.0
```

failed.

Why?

Because the appropriate Microsoft package repository had not yet been configured.

So we asked Linux:

```bash
cat /etc/os-release
```

and discovered:

```text
Debian GNU/Linux 13
```

Now we know which repository configuration we need.

After configuring the correct Debian repository:

```bash
sudo apt update
```

and:

```bash
sudo apt install dotnet-sdk-10.0
```

the installation succeeds.

### Mentor Lesson

> **An error is not failure. An error is information.**

```text
Command
   |
   v
ERROR
   |
   v
Read the error
   |
   v
Understand the environment
   |
   v
Check the OS
   |
   v
Correct the repository
   |
   v
Install
```

This is how an engineer troubleshoots.


# 9. From VM to Application

Now we have:

```text
GCP
 |
 v
Compute Engine
 |
 v
Debian 13
 |
 v
.NET 10
 |
 v
ASP.NET Core
 |
 v
TFLPortalWeekend
```

We have moved from **infrastructure** to **application development**.


# 10. But Why Can't My Browser Access It?

**Student:**

> "Sir, the application is running inside the VM, but I cannot access it from my laptop."

**Ravi Sir:**

> "Very good question."

Because there are multiple layers involved.

```text
Laptop
  |
  | Internet
  v
GCP Network
  |
  v
Firewall
  |
  v
VM
  |
  v
Port 5102
  |
  v
ASP.NET Core
```

Your application can be perfectly healthy:

```text
ASP.NET Core
     |
     v
Running
     |
     v
localhost:5102
```

but external traffic can still be blocked.


# 11. Firewall

Therefore:

```text
Application
     |
     v
Port 5102
     |
     v
GCP Firewall
     |
     v
Internet
```

For a classroom demonstration, we can create a firewall rule to allow incoming traffic on the required port.

For example:

```text
TCP :5102
```

### Important Production Lesson

For a temporary classroom experiment, a broad rule may be convenient.

For production:

> **Do not blindly expose everything to the internet.**

Use:

* Restricted source IPs
* Appropriate firewall rules
* HTTPS
* Authentication
* Authorization
* Reverse proxy/load balancer
* Monitoring


# 12. Now DBaaS

Now Ravi Sir asks:

> "Where does our application run?"

**Student:**

> "On the VM."

> "Good. Where will the database live?"

This introduces:

# Database as a Service

Instead of installing and maintaining MySQL ourselves:

```text
VM
 |
 +-- Linux
 |
 +-- Install MySQL
 |
 +-- Configure MySQL
 |
 +-- Backup
 |
 +-- Replication
 |
 +-- Patch
 |
 +-- Monitor
```

we can use a managed database service:

```text
              CLOUD
                |
                v
       +----------------+
       |     DBaaS      |
       |                |
       | Managed MySQL  |
       +----------------+
                |
                v
         TFLInsuranceDB
```

The cloud provider manages much of the database infrastructure and operational work.


# 13. Traditional Database vs DBaaS

## Traditional Database

```text
Developer / DBA
       |
       +-- Buy Server
       +-- Install OS
       +-- Install MySQL
       +-- Configure
       +-- Backup
       +-- Patch
       +-- Replication
       +-- Handle Hardware Failure
       +-- Monitor
```

## DBaaS

```text
                 CLOUD PROVIDER
                       |
             +---------+---------+
             |                   |
        Infrastructure      Database Service
             |                   |
          Servers             MySQL
          Storage             PostgreSQL
          Network             SQL Server
             |
             v
          Managed
```

The developer can concentrate more on:

```text
Application
    +
Business Logic
    +
Data
```

rather than managing the physical database infrastructure.


# 14. What Does DBaaS Save Us From?

Ravi Sir asks:

> "Do I need to buy a physical server?"

**No.**

> "Do I need to install the operating system?"

**Usually no.**

> "Do I need to manually install the database engine?"

**Usually no.**

> "Do I need to manually manage hardware failures?"

**No.**

> "What about backups?"

The managed service can provide automated backup capabilities according to its configuration and service plan.

> "What about patching?"

The provider handles much of the database infrastructure maintenance.

### The important idea

> **DBaaS moves database infrastructure management from the customer to the cloud provider.**


# 15. IaaS + DBaaS Together

This is the important architecture.

```text
                         PUBLIC CLOUD
                              |
              +---------------+---------------+
              |                               |
             IaaS                            DBaaS
              |                               |
       Compute Engine                    Managed DB
              |                               |
        Virtual Machine                  MySQL
              |                               |
          Debian Linux                        |
              |                               |
           .NET 10                            |
              |                               |
       ASP.NET Core                           |
              |                               |
              +---------------+---------------+
                              |
                              v
                       TFLInsuranceDB
```

The application does not necessarily need to install MySQL inside the VM.

Instead:

```text
ASP.NET Core
      |
      | SQL / Database Connection
      v
Cloud Database
      |
      v
TFLInsuranceDB
```


# 16. Complete Transflower Architecture

Now connect everything we have learned.

```text
                           INTERNET
                               |
                               v
                     +-------------------+
                     |    GCP CLOUD      |
                     +-------------------+
                         |           |
                         |           |
                        IaaS        DBaaS
                         |           |
                         v           v
                 +-----------+   +-----------+
                 | Compute   |   | Managed   |
                 | Engine VM |   | MySQL     |
                 +-----------+   +-----------+
                       |              |
                       v              v
                    Debian       TFLInsuranceDB
                       |
                       v
                    .NET 10
                       |
                       v
                 ASP.NET Core
                       |
                       v
                 TFLInsurance API
                       |
              +--------+--------+
              |                 |
           Browser           Postman
```


# 17. The Bigger Picture

Students should understand this hierarchy:

```text
                 CLOUD
                   |
       +-----------+-----------+
       |           |           |
      IaaS        PaaS        SaaS
       |
    Compute
       |
      VM
       |
      OS
       |
    Runtime
       |
  Application
       |
      API
       |
      Data
       |
     DBaaS
```

But remember:

> **IaaS and DBaaS are not competitors. They can work together.**

```text
IaaS
 |
 | Hosts the application
 v
ASP.NET Core
 |
 | Connects to
 v
DBaaS
 |
 v
MySQL
```

# 18. Roles in the Cloud World

This also explains why different engineering roles exist.

```text
                    CLOUD
                      |
          +-----------+-----------+
          |                       |
      Infrastructure           Application
          |                       |
          v                       v
    System Engineer          Software Engineer
    DevOps Engineer          Full Stack Developer
    Deployment Engineer
          |
          v
       VM / Network
       Security
       Deployment
```

Different engineers may be responsible for different layers.

A developer should at least understand how their application moves through these layers.


# 19. Final Mentor Conversation

**Student:**

> "Sir, is cloud just putting our application on the internet?"

**Ravi Sir:**

> "No."

```text
Cloud ≠ Just Internet Hosting
```

Cloud is a collection of services:

```text
Compute
Storage
Network
Database
Security
Monitoring
Messaging
AI
Analytics
```

The developer should understand **which service solves which problem**.


# 20. Today's Takeaway

## IaaS

> **IaaS gives you infrastructure.**

```text
VM
CPU
RAM
Disk
Network
IP
```

## DBaaS

> **DBaaS gives you a managed database service.**

```text
MySQL
PostgreSQL
SQL Server
Oracle
```

## Our Application

```text
              GCP
               |
       +-------+-------+
       |               |
      IaaS            DBaaS
       |               |
       v               v
      VM             MySQL
       |               |
    Debian             |
       |               |
    .NET 10            |
       |               |
 ASP.NET Core ---------+
       |
      API
       |
 Browser / Postman
```
# Ravi Sir's Closing Message

> **"Framework शिकण्याआधी machine समजा.
> Application deploy करण्याआधी infrastructure समजा.
> Database वापरण्याआधी database service model समजा."**

> **"Cloud म्हणजे फक्त server दुसऱ्याच्या data center मध्ये ठेवणे नाही. Cloud म्हणजे infrastructure आणि services on demand वापरण्याची engineering approach."**

And finally:

> **Don't just learn how to write the application. Learn where the application lives, how it communicates, where its data lives, and who manages each layer.**