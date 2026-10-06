# Exploring GCP and Creating a Virtual Machine

> **Mentor Thought:**
> Before learning DevOps tools, understand the **computer infrastructure** on which our application runs.


## 1. Learning Objective

In this session, we will understand how a cloud platform such as **Google Cloud Platform (GCP)** provides infrastructure and how a developer can create and access a **Virtual Machine (VM)**.

#### At the end of this session, you should be able to:

* Understand what GCP is.
* Understand the concept of a cloud Virtual Machine.
* Create a VM using GCP Compute Engine.
* Understand public IP addresses.
* Understand firewall rules.
* Allow HTTP traffic to a VM.
* Deploy a simple web application.
* Access that application from a browser.



## 2. What is GCP?

**GCP = Google Cloud Platform**

GCP is Google's cloud computing platform. Instead of purchasing and maintaining our own physical servers, we can rent computing resources from Google.

```text
Traditional IT

Company
   |
   +---- Physical Server
   |
   +---- Storage
   |
   +---- Network
   |
   +---- Firewall
   |
   +---- Data Center
```

With cloud:

```text
                Google Cloud
                     |
        +------------+------------+
        |            |            |
      Compute      Storage      Database
        |
       VM
        |
     Linux OS
        |
    Application
```

The cloud provider manages the physical infrastructure. The developer focuses more on the **software and application**.



## 3. Think Like a Developer

Suppose we have created an ASP.NET Core application:

```text
ASP.NET Core Application
          |
          v
      Linux / Windows
          |
          v
       Computer
```

The application needs a computer to execute. Where is that computer? In traditional development:

```text
Developer
   |
   v
My Laptop
   |
   v
Application
```

In production:

```text
Internet
   |
   v
Cloud
   |
   v
Virtual Machine
   |
   v
Operating System
   |
   v
Application
```

This is where **GCP Compute Engine** becomes important.


## 4. What is a Virtual Machine?

A **Virtual Machine** is a software-defined computer running inside a physical server.

For example:

```text
Physical Google Server
+--------------------------------------+
|                                      |
|       Hypervisor / Virtualization    |
|                                      |
|   +-------------+  +-------------+   |
|   | VM 1        |  | VM 2        |   |
|   | Debian      |  | Ubuntu      |   |
|   | 2 CPU       |  | 4 CPU       |   |
|   | 4 GB RAM    |  | 8 GB RAM    |   |
|   +-------------+  +-------------+   |
|                                      |
+--------------------------------------+
```

To us, the VM behaves like an independent computer. We can:

```text
Login
   |
Install software
   |
Run commands
   |
Install database
   |
Run web application
   |
Configure networking
```


## 5. GCP Compute Engine

GCP provides Virtual Machines through: **Compute Engine** Think of Compute Engine as:  **"Give me a computer in Google's data center."** A VM can have:

```text
VM
 |
 +-- CPU
 |
 +-- RAM
 |
 +-- Disk
 |
 +-- Operating System
 |
 +-- Network Interface
 |
 +-- IP Address
```

## 6. Architecture of Our Lab

Our simple learning architecture:

```text
                    INTERNET
                        |
                        |
                 Public IP Address
                        |
                        v
              +-------------------+
              |   GCP Firewall    |
              |                   |
              |   Allow TCP :80   |
              +---------+---------+
                        |
                        v
              +-------------------+
              |   GCP VM          |
              |                   |
              |   Debian Linux    |
              |                   |
              |   Web Server      |
              |   Port 80         |
              +-------------------+
                        |
                        v
                  Web Application
```

The important concept is:  **Creating a VM does not automatically mean that every network port is accessible from the Internet.** The firewall controls incoming network traffic.

## 7. Step 1 — Open GCP Console

Open the Google Cloud Console. Select or create your **GCP Project**. A project provides a logical boundary for resources.

```text
Google Cloud
      |
      v
   Project
      |
      +---- VM
      +---- Network
      +---- Firewall
      +---- Storage
      +---- APIs
```

## 8. Step 2 — Open Compute Engine

From the GCP Console:

```text
Navigation Menu
      |
      v
Compute Engine
      |
      v
VM instances
```

Click:

**Create Instance**

You are now creating a virtual computer.


## 9. Step 3 — Configure the VM

For a learning environment, keep the VM configuration simple.

Example:

```text
Name:
    tfl-web-server

Region:
    Choose a nearby region

Zone:
    Select an available zone

Machine type:
    Small / appropriate learning configuration

Boot disk:
    Debian / Ubuntu Linux

Disk:
    Small learning disk
```

The exact machine options and pricing can change, so choose a current configuration appropriate for your lab.


## 10. Step 4 — Allow HTTP Traffic

While creating the VM, you may see:

```text
Firewall

[ ] Allow HTTP traffic
[ ] Allow HTTPS traffic
```

For our HTTP web-server lab:

```text
[x] Allow HTTP traffic
```

This tells GCP to configure network access so that HTTP traffic can reach the VM.

Conceptually:

```text
Browser
   |
   | HTTP
   | TCP Port 80
   v
GCP Firewall
   |
   | ALLOW
   v
VM
   |
   v
Web Server
```


## 11. What is HTTP?

HTTP means:

**HyperText Transfer Protocol**

A browser communicates with a web server using HTTP/HTTPS.

For example:

```text
http://34.xx.xx.xx
```

The default HTTP port is:

```text
TCP 80
```

HTTPS normally uses:

```text
TCP 443
```

Therefore:

```text
HTTP
 |
 +---- TCP
        |
        +---- Port 80
```

## 12. Why Do We Need a Firewall?

Imagine your VM has many doors:

```text
              VM
       +----------------+
       |                |
       |  Port 22       | SSH
       |  Port 80       | HTTP
       |  Port 443      | HTTPS
       |  Port 3306     | MySQL
       |  Port 8080     | Application
       |                |
       +----------------+
```

We don't want every door open to the Internet. The firewall acts like a security guard.

```text
Internet
   |
   v
+----------------+
|   FIREWALL     |
|                |
| Port 22   ?    |
| Port 80   YES  |
| Port 443  ?    |
| Port 3306 NO   |
+----------------+
        |
        v
       VM
```

## 13. Firewall Rule

A firewall rule can be thought of as:

```text
WHO?
   |
   v
SOURCE

WHAT?
   |
   v
PROTOCOL + PORT

ACTION?
   |
   v
ALLOW / DENY
```

For our HTTP lab:

```text
Source:
    Internet

Protocol:
    TCP

Port:
    80

Action:
    ALLOW
```

Conceptually:

```text
0.0.0.0/0
     |
     | TCP :80
     v
  ALLOW
```

`0.0.0.0/0` represents IPv4 addresses from everywhere. For a production system, you should carefully restrict exposure where possible rather than opening services unnecessarily to the entire Internet.

## 14. VM Network Interface

Our VM has a network interface.

```text
                    VM
             +---------------+
             |               |
Internet --->| Network       |
             | Interface     |
             +---------------+
                    |
             Private IP
                    |
             Public IP
```

A VM can have a private/internal IP used within the cloud network and, when configured, an external/public IP used for Internet access.

Example:

```text
Public IP

34.xxx.xxx.xxx
```

We can use the public IP from our browser when the application and firewall are correctly configured.

## 15. Step 5 — Create the VM

Click:

**Create**

GCP provisions the virtual machine.

Conceptually:

```text
Create
  |
  v
Allocate Infrastructure
  |
  v
Create VM
  |
  v
Attach Disk
  |
  v
Boot Operating System
  |
  v
Configure Network
  |
  v
VM Running
```

After creation, you should see something similar to:

```text
VM instances

NAME              STATUS
--------------------------------
tfl-web-server    Running
```

## 16. Step 6 — Connect to the VM

GCP provides browser-based SSH access for Linux VMs.

Click:

**SSH**

You will get a terminal.

```text
+------------------------------------------+
| user@tfl-web-server:~$                  |
|                                          |
| $ uname -a                               |
| $ lscpu                                  |
| $ free -h                                |
| $ df -h                                  |
| $ ip addr                                |
+------------------------------------------+
```

Now we are working with a real Linux computer running in the cloud.


## 17. Mentor Lab — Explore the VM

Run:

```bash
uname -a
```

This tells us about the operating system/kernel.



###### CPU

```bash
lscpu
```

Understand the CPU configuration.


###### Memory

```bash
free -h
```

Example:

```text
              total
Mem:           3.8Gi
```

###### Disk

```bash
df -h
```

Understand available disk space.

###### Network

```bash
ip addr
```

This shows network interfaces and IP information.

## 18. Step 7 — Install a Simple Web Server

Now let's prove that our VM can behave like a web server. For Debian/Ubuntu:

```bash
sudo apt update
```

Then install a simple web server:

```bash
sudo apt install nginx -y
```

Start it:

```bash
sudo systemctl start nginx
```

Check status:

```bash
sudo systemctl status nginx
```

We should see something similar to:

```text
Active: active (running)
```

## 19. Test Inside the VM

Run:

```bash
curl http://localhost
```

The VM itself sends an HTTP request to its local web server.

Architecture:

```text
VM
 |
 | HTTP :80
 v
Nginx
 |
 v
HTML Response
```

If this works, our web server is running.


## 20. Now Test From the Internet

Find the VM's external/public IP address in GCP.

For example:

```text
34.xxx.xxx.xxx
```

Open:

```text
http://34.xxx.xxx.xxx
```

in your browser.

Expected result:

```text
Welcome to nginx!
```

Now the complete journey is:

```text
Browser
   |
   | HTTP Request
   |
   v
Public IP
   |
   v
GCP Network
   |
   v
Firewall
   |
   | TCP :80 ALLOWED
   |
   v
VM
   |
   v
Nginx
   |
   v
HTTP Response
   |
   v
Browser
```

## 21. Very Important Debugging Concept

Suppose you installed Nginx but the browser cannot connect.

Don't randomly change things.

Think layer by layer.

```text
1. Is VM running?
        |
        v
2. Does VM have external IP?
        |
        v
3. Is firewall allowing TCP 80?
        |
        v
4. Is Nginx running?
        |
        v
5. Is Nginx listening on port 80?
        |
        v
6. Can localhost access Nginx?
        |
        v
7. Can external client access it?
```

This is **engineering thinking**.


## 22. Check Port 80 on Linux

Inside the VM:

```bash
sudo ss -tulpn
```

Look for something similar to:

```text
LISTEN
0.0.0.0:80
```

That tells us that a process is listening on HTTP port 80.


## 23. Three Different Things Students Often Confuse

This is an important distinction.

###### 1. Application

```text
Nginx
ASP.NET Core
Node.js
FastAPI
```

###### 2. Operating System

```text
Debian
Ubuntu
Windows Server
```

###### 3. Cloud Firewall

```text
Allow TCP 80
Allow TCP 443
Allow SSH
```

They are different layers.

```text
Internet
   |
   v
Cloud Firewall
   |
   v
Operating System
   |
   v
Web Server
   |
   v
Application
```

## 24. HTTP Firewall vs Application Port

Suppose your ASP.NET Core application runs on:

```text
http://localhost:5000
```

Then opening port 80 alone doesn't magically make port 5000 publicly accessible.

You need to understand the mapping.

For example:

```text
Internet
   |
   | TCP 80
   v
Nginx
   |
   | reverse proxy
   v
ASP.NET Core
   |
   | TCP 5000
   v
Application
```

This is a more production-oriented architecture.


## 25. Simple VM vs Production Architecture

###### Learning Architecture

```text
Internet
   |
   v
GCP Firewall
   |
   v
VM
   |
   v
Nginx
```

###### More realistic application architecture

```text
                 INTERNET
                     |
                     v
             Load Balancer
                     |
                     v
              HTTPS :443
                     |
                     v
               Web Server
                     |
                     v
              Application
                     |
                     v
                Database
```

As developers, we gradually move from:

```text
"How do I run my application?"
```

to:

```text
"How do I run my application securely,
reliably and at scale?"
```

That is the journey from **development to DevOps**.

## 26. Connection With DevOps

Creating the VM is only one small part of DevOps.

```text
             DEVOPS
                |
     +----------+----------+
     |          |          |
   Code       Build     Deploy
     |          |          |
     +----------+----------+
                |
          Infrastructure
                |
                v
          Cloud / GCP
                |
                v
             VM
                |
                v
          Application
```

A developer should understand:

* Linux
* Networking
* HTTP
* DNS
* Ports
* Firewall
* SSH
* Processes
* Logs
* Deployment
* Cloud infrastructure

Not necessarily become a full-time infrastructure engineer, but understand **how the application actually reaches the user**.

## 27. Transflower Mentor Analogy

Imagine a hotel.

```text
Hotel
 |
 +---- Building
 |
 +---- Rooms
 |
 +---- Doors
 |
 +---- Security Guard
 |
 +---- Reception
```

Map it to cloud:

```text
Hotel Building     → Physical Server
Room               → Virtual Machine
Room Number        → IP Address
Door                → Network Port
Security Guard     → Firewall
Reception           → Web Server
Guest               → Client/Browser
```

If the room exists but the door is locked:

```text
VM exists
   +
Port blocked
   =
Cannot access application
```

If the door is open but nobody is inside:

```text
Port 80 open    +  No web server   = Connection failure
```

If the web server is running but the firewall blocks traffic:

```text
Application running      +  Firewall DENY   = Internet cannot reach it
```

This is why we don't just say:

> **"My application is running."**

We ask:

> **"Can the client reach my application?"**


## 28. Hands-on Exercise

Create your own VM:

```text
VM Name:
    tfl-web-server

OS:
    Debian / Ubuntu

Network:
    External IP enabled

Firewall:
    HTTP allowed

Web Server:
    Nginx
```

Then execute:

```bash
uname -a

lscpu

free -h

df -h

ip addr

sudo apt update

sudo apt install nginx -y

sudo systemctl status nginx

curl http://localhost

sudo ss -tulpn
```

Finally open:

```text
http://<YOUR_PUBLIC_IP>
```


## 29. Troubleshooting Checklist

If the browser doesn't show the page:

```text
                    Browser
                       |
                       v
                Public IP correct?
                       |
                  YES / NO
                       |
                       v
                 Firewall :80?
                       |
                  YES / NO
                       |
                       v
                  VM Running?
                       |
                  YES / NO
                       |
                       v
                 Nginx Running?
                       |
                  YES / NO
                       |
                       v
               Port 80 Listening?
                       |
                  YES / NO
                       |
                       v
               curl localhost
```

**Don't troubleshoot randomly. Trace the request.**


## 30. What We Learned

```text
GCP
 |
 +-- Project
 |
 +-- Compute Engine
       |
       +-- Virtual Machine
              |
              +-- CPU
              +-- RAM
              +-- Disk
              +-- Linux
              +-- Network
              +-- Public IP
              +-- Firewall
              +-- Web Server
```

And the most important request flow:

```text
CLIENT
  |
  | HTTP Request
  v
PUBLIC IP
  |
  v
GCP FIREWALL
  |
  | TCP 80 ALLOWED
  v
VIRTUAL MACHINE
  |
  v
WEB SERVER
  |
  v
APPLICATION
  |
  v
HTTP RESPONSE
  |
  v
CLIENT
```

## Mentor's Closing Thought

> **"Cloud computing is not magic. Somewhere behind your cloud application there is CPU, memory, storage, networking and an operating system. GCP gives us these resources as services. As developers, we should understand the journey of our application from code to computer to network to user."**


