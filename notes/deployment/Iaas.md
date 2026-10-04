# Database as a Service + Infrastructure as a Service

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

Let me draw three boxes on the board.

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