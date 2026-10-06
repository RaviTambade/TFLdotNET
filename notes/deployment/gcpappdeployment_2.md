
# SSH → Linux VM → Ports → Firewall → Deploying a Web API on GCP

> **Today's Mentor Question:**
> We created a Virtual Machine yesterday.
> **But how does a developer actually enter that machine, install software, run an application, and make that application accessible from the Internet?**


## 1. Yesterday's Journey

We started with:

```text
GCP
 |
 +-- Project
      |
      +-- Compute Engine
             |
             +-- Virtual Machine
                    |
                    +-- Debian Linux
                    +-- Public IP
                    +-- Firewall
```

Today we go one level deeper.

```text
Developer
    |
    | SSH
    v
GCP VM
    |
    +-- Linux
    |
    +-- Application
    |
    +-- Ports
    |
    +-- Firewall
```


# 2. What is SSH?

**SSH = Secure Shell**

SSH gives us a secure command-line connection to a remote computer. Think about this:

```text
Your Laptop
     |
     | SSH
     |
     v
+-------------------+
| GCP Virtual       |
| Machine           |
|                   |
| Debian Linux      |
+-------------------+
```

It is almost as if you are sitting in front of that remote computer.

# 3. Why Do We Need SSH?

Suppose your application is running on a VM in Google's data center.

You want to:

```text
Install Python
Install Node.js
Install .NET
Install Nginx
Create folders
Copy files
Run application
Check logs
Restart services
```

How do you do that?

**SSH.**

# 4. SSH Architecture

```text
+------------------+
| Developer Laptop |
|                  |
| SSH Client       |
+--------+---------+
         |
         | SSH
         | TCP 22
         |
         v
+------------------+
| GCP Firewall     |
+--------+---------+
         |
         v
+------------------+
| GCP VM           |
|                  |
| SSH Server       |
|      |           |
|    Linux         |
+------------------+
```

Notice something important:

```text
SSH → TCP Port 22
HTTP → TCP Port 80
HTTPS → TCP Port 443
```

# 5. What do  you mean by Port?

Imagine the VM is a building.

```text
                VM
        +----------------+
        |                |
        | Door 22        | SSH
        | Door 80        | HTTP
        | Door 443       | HTTPS
        | Door 3306      | MySQL
        |                |
        +----------------+
```

A **port** is a logical communication endpoint. An IP address identifies the computer. A port identifies the service on that computer.

```text
IP Address + Port
```

For example:

```text
34.xx.xx.xx:80
```

means: Connect to the computer at this IP address using port 80.

 

# 6. IP Address vs Port

Students often confuse these two.

### IP Address

```text
34.123.45.67
```

Answers:

> **Which computer?**

### Port

```text
80
```

Answers:

> **Which service?**

Together:

```text
34.123.45.67:80
```

means:

```text
Computer   +  Web service
```

# 7. Common Ports

| Service                    | Protocol |                         Port |
| -------------------------- | -------- | ---------------------------: |
| SSH                        | TCP      |                           22 |
| HTTP                       | TCP      |                           80 |
| HTTPS                      | TCP      |                          443 |
| MySQL                      | TCP      |                         3306 |
| PostgreSQL                 | TCP      |                         5432 |
| Node.js development server | TCP      |                         3000 |
| ASP.NET Core               | TCP      | 5000/5001 or configured port |
| FastAPI/Uvicorn            | TCP      |                         8000 |

> Ports are conventions, not permanent laws. Applications can be configured to listen on different ports.


# 8. GCP Firewall

Now remember yesterday's firewall.

```text
Internet
    |
    v
+------------------+
| GCP Firewall     |
+------------------+
    |
    +---- TCP 22  → SSH
    |
    +---- TCP 80  → HTTP
    |
    +---- TCP 443 → HTTPS
    |
    v
   VM
```

The firewall decides which traffic is allowed to reach the VM.


# 9. Security Thinking

Suppose we have:

```text
Internet
    |
    +---- 22
    +---- 80
    +---- 443
    +---- 3306
    +---- 5000
    +---- 8000
```

Should we open all of them? **No.** Open only what is required. For example:

```text
Production Web Server

22   → SSH     → restricted access
80   → HTTP    → maybe redirect to HTTPS
443  → HTTPS   → public
3306 → MySQL   → NOT public
```

This is an important DevOps/security principle: **Minimize the attack surface.**

# 10. Connect to the VM

From GCP:

```text
Compute Engine
      |
      v
VM Instances
      |
      v
tfl-web-server
      |
      v
SSH
```

Click **SSH**. A terminal opens. You are now inside the cloud computer.


# 11. First Linux Commands

Let's understand our machine.

### Who am I?

```bash
whoami
```

### Current directory

```bash
pwd
```

### Files

```bash
ls
```

### Detailed files

```bash
ls -la
```

### Operating system

```bash
cat /etc/os-release
```

### Kernel

```bash
uname -a
```

# 12. Mentor Connection

You previously worked with:

```bash
uname -a
lscpu
free -h
df -h
ip addr
ps aux
```

These are not just Linux commands. They are **operational knowledge**. 

```text
What OS am I running?
How much CPU?
How much RAM?
How much disk?
What IP?
Which processes are running?
```


# 13. Process = Running Program

Run:

```bash
ps aux
```

You will see many processes.

Think:

```text
Linux
 |
 +-- SSH
 |
 +-- System processes
 |
 +-- Nginx
 |
 +-- Other services
```

A process is basically a running program.



# 14. Start a Web Server

Let's install Nginx if it isn't already installed.

```bash
sudo apt update
```

Then:

```bash
sudo apt install nginx -y
```

Check:

```bash
sudo systemctl status nginx
```

You should see:

```text
active (running)
```

# 15. What Actually Happened?

We executed:

```bash
sudo apt install nginx
```

Linux downloaded the software.

Then:

```text
Nginx
   |
   v
Process
   |
   v
Listening
   |
   v
Port 80
```

Check it:

```bash
sudo ss -tulpn
```

Look for:

```text
:80
```

# 16. Test Locally

From the VM:

```bash
curl http://localhost
```

If Nginx is working, you should receive HTML. 

Architecture:

```text
VM
 |
 | localhost:80
 v
Nginx
 |
 v
HTML
```

This proves:

> **The application/web server is running.**

But does this prove the Internet can access it?

**No.**

# 17. Test From Your Laptop

Open:

```text
http://YOUR_PUBLIC_IP
```

Example:

```text
http://34.xx.xx.xx
```

Request flow:

```text
Your Browser
     |
     | HTTP
     v
Public IP
     |
     v
GCP Firewall
     |
     | TCP 80
     v
VM
     |
     v
Nginx
     |
     v
HTML
```

# 18. The Most Important Debugging Lesson

Suppose:

```bash
curl http://localhost
```

works.

But:

```text
http://PUBLIC_IP
```

doesn't work.

What does that tell us?

It means:

```text
Application
     |
     v
Working
```

but something between:

```text
Internet
     |
     v
VM
```

is preventing access.

Check:

```text
1. Public IP
2. GCP firewall
3. VM network configuration
4. OS firewall
5. Application binding
6. Port
```


# 19. Application Binding

This is a very important concept. Suppose an application listens only on:

```text
127.0.0.1:8000
```

It means: Accept connections only from this computer itself. It may work:

```bash
curl http://localhost:8000
```

but not from another computer. For a web application intended to receive external traffic, it may need to listen on an appropriate network interface, commonly:

```text
0.0.0.0
```

For example, FastAPI/Uvicorn:

```bash
uvicorn main:app --host 0.0.0.0 --port 8000
```

Architecture:

```text
Wrong:

127.0.0.1:8000
     ^
     |
 Local machine only


Correct for external access:

0.0.0.0:8000
     ^
     |
 Network interfaces
```

But remember:

> Listening on `0.0.0.0` does **not** itself open the port to the Internet. Firewall rules still matter.


# 20. Let's Deploy a Python API

Now our cloud VM becomes more interesting. Suppose we have:

```text
FastAPI
```

Application:

```text
main.py
```

Example:

```python
from fastapi import FastAPI

app = FastAPI()

@app.get("/")
def hello():
    return {
        "message": "Hello from Transflower GCP VM"
    }
```

Install Python dependencies:

```bash
sudo apt install python3-pip python3-venv -y
```

Create project:

```bash
mkdir tfl-api
cd tfl-api
```

Create virtual environment:

```bash
python3 -m venv venv
```

Activate:

```bash
source venv/bin/activate
```

Install:

```bash
pip install fastapi uvicorn
```

Run:

```bash
uvicorn main:app --host 0.0.0.0 --port 8000
```


# 21. Now We Have a Problem

Our application is running on:

```text
0.0.0.0:8000
```

But can the Internet access it? Not necessarily. Why? Because:

```text
Application
    |
    v
Port 8000
    |
    v
GCP Firewall
```

If the firewall doesn't allow TCP 8000, the request won't reach the application.


# 22. Creating a Firewall Rule for Port 8000

For a temporary learning lab, you can create a firewall rule allowing TCP 8000.

Conceptually:

```text
Firewall Rule

Name:
    allow-tfl-api

Protocol:
    TCP

Port:
    8000

Action:
    ALLOW
```

Then:

```text
Browser
   |
   | http://PUBLIC_IP:8000
   v
GCP Firewall
   |
   | TCP 8000
   | ALLOW
   v
VM
   |
   v
FastAPI
```

For production, don't casually expose development ports to the public Internet.


# 23. Access Swagger

FastAPI automatically provides interactive documentation.

Open:

```text
http://PUBLIC_IP:8000/docs
```

You should get:

```text
FastAPI Swagger UI
```

Now we have:

```text
Browser
   |
   v
GCP
   |
   v
Virtual Machine
   |
   v
Linux
   |
   v
Python
   |
   v
FastAPI
   |
   v
REST API
```

This is a real cloud deployment.


# 24. Developer's Journey

Notice what we have done.

```text
Python Code
    |
    v
FastAPI
    |
    v
Uvicorn
    |
    v
Linux Process
    |
    v
Port 8000
    |
    v
GCP Firewall
    |
    v
Public IP
    |
    v
Internet
    |
    v
Browser
```

This is much more valuable than simply knowing:

```python
@app.get("/")
```

We understand **where the code actually runs**.



# 25. Where Does Docker Come?

Now you can understand why Docker is useful. Without Docker:

```text
VM
 |
 +-- Python
 +-- FastAPI
 +-- Dependencies
 +-- Configuration
 +-- Application
```

With Docker:

```text
VM
 |
 +-- Docker
       |
       +-- Container
             |
             +-- Python
             +-- FastAPI
             +-- Dependencies
             +-- Application
```

Later we can evolve this into:

```text
Developer
    |
    v
Docker Image
    |
    v
Container
    |
    v
GCP VM
```

# 26. And Then CI/CD

Eventually:

```text
Developer
    |
    v
Git
    |
    v
GitHub
    |
    v
CI/CD Pipeline
    |
    +---- Build
    |
    +---- Test
    |
    +---- Docker Image
    |
    +---- Deploy
    |
    v
GCP
    |
    v
VM
    |
    v
Application
```

Now you are starting to see the **DevOps picture**.


# 27. Mentor's Architecture

```text
                  INTERNET
                      |
                      v
                +-----------+
                | Public IP |
                +-----+-----+
                      |
                      v
                +-----------+
                | Firewall  |
                +-----+-----+
                      |
             +--------+--------+
             |                 |
          TCP 80            TCP 8000
             |                 |
             v                 v
          Nginx            FastAPI
             |                 |
             +--------+--------+
                      |
                      v
                  Linux VM
                      |
                      v
                 GCP Compute
```



# 28. What Should You Remember?

### IP

```text
Which computer?
```

### Port

```text
Which service?
```

### Firewall

```text
Who is allowed to enter?
```

### SSH

```text
How does the administrator/developer enter the machine?
```

### Web Server

```text
Who receives HTTP requests?
```

### Application

```text
Who executes the business logic?
```


# 29. Transflower Mentor Rule

> **Don't memorize commands. Understand the request journey.**

When somebody says:

> "My API is not accessible."

Don't immediately change code.

Ask:

```text
Is the VM running?
       ↓
Does it have a public IP?
       ↓
Is the application running?
       ↓
Which port?
       ↓
Is the application listening on the right interface?
       ↓
Is GCP firewall allowing that port?
       ↓
Is OS firewall blocking it?
       ↓
Can localhost access it?
       ↓
Can external client access it?
```

That is **developer + DevOps thinking**.



## Next Lab

The natural next step is:

```text
GCP VM
   ↓
SSH
   ↓
Linux
   ↓
Git
   ↓
Clone GitHub Repository
   ↓
Python / Node.js / ASP.NET Core
   ↓
Run Application
   ↓
Nginx Reverse Proxy
   ↓
HTTP :80
   ↓
Application :8000 / :5000
```

That session will introduce an important production concept: **Reverse Proxy + Nginx + Application Server**.
