# Nginx Reverse Proxy + FastAPI Application on GCP VM

> **Mentor Question:**
> Yesterday our FastAPI application was running on `:8000`. But do we really want users to type `http://public-ip:8000`? 
>
> **No.**
>
> A production-style application should normally expose a standard web entry point such as **HTTP :80** or, preferably, **HTTPS :443**.


# 1. Where We Are

Our previous architecture was:

```text
Browser
   |
   | http://PUBLIC-IP:8000
   v
GCP Firewall
   |
   v
Linux VM
   |
   v
FastAPI
   |
   | :8000
   v
Application
```

It works for learning. But let's improve the architecture.

# 2. The New Architecture

We will introduce **Nginx** as a Reverse Proxy.

```text
                     INTERNET
                         |
                         |
                    HTTP :80
                         |
                         v
                +----------------+
                | GCP Firewall   |
                +-------+--------+
                        |
                        v
                +----------------+
                | Linux VM       |
                |                |
                | Nginx :80      |
                |     |          |
                |     v          |
                | FastAPI :8000  |
                +----------------+
```

The browser communicates with **Nginx**. Nginx communicates with **FastAPI**.



# 3. What is a Reverse Proxy?

Let's first understand the word **proxy**. A proxy acts as an intermediary.

```text
Client
   |
   v
Proxy
   |
   v
Server
```

A **reverse proxy** sits in front of our application servers.

```text
Internet
    |
    v
Reverse Proxy
    |
    +------> Application 1
    |
    +------> Application 2
    |
    +------> Application 3
```

Nginx is commonly used for this purpose.


# 4. Real-World Analogy

Imagine a large hospital.

```text
Patient
   |
   v
Reception
   |
   +----> Cardiology
   |
   +----> Orthopedics
   |
   +----> Neurology
```

The patient doesn't need to know which doctor or room handles the request. The **reception desk** routes the patient. Nginx plays a similar role.

```text
Browser
   |
   v
Nginx
   |
   +----> FastAPI
   |
   +----> Node.js
   |
   +----> ASP.NET Core
```


# 5. Why Use Nginx?

Nginx can provide:

* Reverse proxy
* HTTP/HTTPS entry point
* Static file serving
* SSL/TLS termination
* Request routing
* Load balancing
* Basic access controls
* Compression/caching in suitable scenarios

So instead of exposing every application directly to the Internet:

```text
Internet
   |
   +---- FastAPI :8000
   +---- Node :3000
   +---- .NET :5000
```

we can have:

```text
Internet
   |
   v
Nginx :80/:443
   |
   +---- FastAPI :8000
   +---- Node :3000
   +---- .NET :5000
```

# 6. Our Learning Application

Let's assume our FastAPI project is:

```text
tfl-api/
 |
 +-- main.py
 +-- venv/
```

`main.py`:

```python
from fastapi import FastAPI

app = FastAPI()

@app.get("/")
def hello():
    return {
        "message": "Hello from Transflower GCP VM"
    }
```

Run it:

```bash
uvicorn main:app --host 127.0.0.1 --port 8000
```

Notice:

```text
127.0.0.1:8000
```

Now FastAPI is accessible **only from the VM itself**. That is exactly what we want when Nginx is going to be the public entry point.


# 7. Why `127.0.0.1`?

Remember:

```text
127.0.0.1
```

means:

> This computer itself.

So:

```text
127.0.0.1:8000
```

means:

> FastAPI is listening on port 8000 locally.

Now:

```text
Internet
   |
   X
127.0.0.1:8000
```

The Internet cannot directly reach it.

But:

```text
Nginx
   |
   | localhost:8000
   v
FastAPI
```

can reach it. This gives us an additional layer of separation.


# 8. Step 1 — Install Nginx

On the GCP VM:

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
Active: active (running)
```

# 9. Test Nginx

From inside the VM:

```bash
curl http://localhost
```

You should get the Nginx response. Now from your browser:

```text
http://PUBLIC_IP
```

You should see the Nginx welcome page. Our architecture currently is:

```text
Browser
   |
   | HTTP :80
   v
GCP Firewall
   |
   v
Nginx
```

FastAPI is not involved yet.


# 10. Start FastAPI

Move into your application:

```bash
cd ~/tfl-api
```

Activate the virtual environment:

```bash
source venv/bin/activate
```

Run:

```bash
uvicorn main:app --host 127.0.0.1 --port 8000
```

Now we have:

```text
Nginx
  |
  | :80
  |
  v
Internet

FastAPI
  |
  | :8000
  |
  v
localhost
```

But Nginx doesn't know that it should forward requests to FastAPI. We need to configure it.


# 11. Nginx Configuration

Nginx configuration tells Nginx:  When a request arrives, where should I send it? Conceptually:

```text
Browser
   |
   | GET /
   v
Nginx
   |
   | proxy_pass
   v
http://127.0.0.1:8000
   |
   v
FastAPI
```


# 12. Create a Server Configuration

Create a configuration file:

```bash
sudo nano /etc/nginx/sites-available/tfl-api
```

Add:

```nginx
server {
    listen 80;

    server_name _;

    location / {
        proxy_pass http://127.0.0.1:8000;

        proxy_set_header Host $host;
        proxy_set_header X-Real-IP $remote_addr;
        proxy_set_header X-Forwarded-For $proxy_add_x_forwarded_for;
        proxy_set_header X-Forwarded-Proto $scheme;
    }
}
```

Save the file.

# 13. Enable the Configuration

Create a symbolic link:

```bash
sudo ln -s /etc/nginx/sites-available/tfl-api \
/etc/nginx/sites-enabled/tfl-api
```

Remove the default configuration if necessary:

```bash
sudo rm /etc/nginx/sites-enabled/default
```


# 14. Test Nginx Configuration

Never restart blindly. First:

```bash
sudo nginx -t
```

Expected:

```text
syntax is ok
test is successful
```

This is a good DevOps habit: **Validate configuration before restarting a service.**


# 15. Reload Nginx

```bash
sudo systemctl reload nginx
```

Now the architecture becomes:

```text
Browser
   |
   | HTTP :80
   v
GCP Firewall
   |
   v
Nginx :80
   |
   | proxy_pass
   |
   v
FastAPI :8000
```

# 16. Test From Browser

Open:

```text
http://PUBLIC_IP
```

You should now get:

```json
{
    "message": "Hello from Transflower GCP VM"
}
```

The browser doesn't know that FastAPI is running on port 8000. The browser only sees:

```text
PUBLIC_IP : 80
```

# 17. What Happened Behind the Scenes?

You typed:

```text
http://34.xx.xx.xx/
```

Browser generated:

```text
GET /
```

Request reached:

```text
GCP Firewall
```

Firewall allowed:

```text
TCP :80
```

Request reached:

```text
Nginx
```

Nginx looked at:

```nginx
location /
```

and forwarded the request to:

```text
127.0.0.1:8000
```

FastAPI processed:

```python
@app.get("/")
```

and returned:

```json
{
    "message": "Hello from Transflower GCP VM"
}
```

Nginx sent the response back to the browser.

# 18. Complete Request Journey

This is the picture I want students to remember.

```text
                       INTERNET
                           |
                           | HTTP
                           v
                  +----------------+
                  | Public IP      |
                  +-------+--------+
                          |
                          | TCP :80
                          v
                  +----------------+
                  | GCP Firewall   |
                  +-------+--------+
                          |
                          v
                  +----------------+
                  | Nginx          |
                  | Reverse Proxy  |
                  | :80            |
                  +-------+--------+
                          |
                          | proxy_pass
                          | 127.0.0.1:8000
                          v
                  +----------------+
                  | FastAPI        |
                  | Application    |
                  +-------+--------+
                          |
                          v
                     Business Logic
```

# 19. Notice Something Important

Our firewall only needs to expose:

```text
TCP 80
```

We don't need to expose:

```text
TCP 8000
```

because FastAPI is listening locally. So:

```text
Internet
   |
   | :80
   v
Nginx
   |
   | :8000
   v
FastAPI
```

Port 8000 is internal to the VM. This is a much cleaner architecture.

# 20. Security Improvement

Previously:

```text
Internet
   |
   +---- :8000 ----> FastAPI
```

Now:

```text
Internet
   |
   +---- :80 ----> Nginx
                       |
                       +---- :8000 ----> FastAPI
```

Therefore:

```text
Public:
    80

Private/internal:
    8000
```

This is one reason reverse proxies are useful.

# 21. What About HTTPS?

HTTP:

```text
http://example.com
```

is not encrypted.

HTTPS:

```text
https://example.com
```

uses TLS encryption. 

Production architecture normally looks like:

```text
Internet
    |
    | HTTPS :443
    v
+----------------+
| Nginx          |
| TLS termination|
+-------+--------+
        |
        | HTTP/internal
        v
   FastAPI :8000
```

Later we can configure a TLS certificate. For a public production site, HTTPS should be the normal target.


# 22. Domain Name

Currently users may access:

```text
http://34.xx.xx.xx
```

That's not friendly.

We want:

```text
https://api.transflower.example
```

DNS connects the name to the public IP.

Conceptually:

```text
api.example.com
       |
       | DNS
       v
34.xx.xx.xx
       |
       v
GCP VM
```

Now the architecture becomes:

```text
User
 |
 v
api.example.com
 |
 v
DNS
 |
 v
Public IP
 |
 v
Nginx
 |
 v
FastAPI
```

# 23. Multiple Applications

Now imagine the VM has:

```text
FastAPI      :8000
Node.js      :3000
ASP.NET Core :5000
```

Nginx can route them.

For example:

```text
api.example.com
        |
        v
     Nginx
        |
        +----> FastAPI :8000


web.example.com
        |
        v
     Nginx
        |
        +----> Node.js :3000
```

Or route by path:

```text
example.com/api
        |
        v
FastAPI :8000

example.com/admin
        |
        v
Node.js :3000
```

This is the beginning of **application gateway / reverse-proxy architecture**.

# 24. Production Architecture

Now our simple learning architecture starts looking like a real deployment:

```text
                         USERS
                           |
                           v
                    Internet / DNS
                           |
                           v
                    HTTPS :443
                           |
                           v
                  +----------------+
                  | Nginx /        |
                  | Reverse Proxy  |
                  +-------+--------+
                          |
             +------------+------------+
             |            |            |
             v            v            v
          FastAPI       Node.js     ASP.NET
           :8000         :3000        :5000
             |            |            |
             +------------+------------+
                          |
                          v
                       Database
```

# 25. From VM to DevOps

Now connect everything we've learned.

```text
                DEVELOPER
                    |
                    v
                 GitHub
                    |
                    v
                CI / CD
                    |
                    v
               Docker Image
                    |
                    v
                GCP VM
                    |
       +------------+------------+
       |                         |
       v                         v
    Nginx                    Application
    :80/:443                 :8000
       |                         |
       +------------+------------+
                    |
                    v
                 Database
```

This is no longer just:

> "I know Python."

It becomes:

> **"I understand how my application gets from source code to a running service that users can access."**

# 26. Mentor Exercise

Now ask students to perform the following.

### Task 1

Check Nginx:

```bash
sudo systemctl status nginx
```

### Task 2

Check port:

```bash
sudo ss -tulpn
```

### Task 3

Check FastAPI locally:

```bash
curl http://127.0.0.1:8000
```

### Task 4

Check Nginx locally:

```bash
curl http://localhost
```

### Task 5

Check Nginx configuration:

```bash
sudo nginx -t
```

### Task 6

From your laptop:

```text
http://PUBLIC_IP
```

# 27. Debugging Exercise

Suppose:

```bash
curl http://127.0.0.1:8000
```

works.

But:

```bash
curl http://localhost
```

fails.

Where is the problem?

```text
FastAPI
   |
   | WORKING
   v
127.0.0.1:8000

Nginx
   |
   | NOT WORKING
   v
localhost:80
```

Check:

```bash
sudo systemctl status nginx
sudo nginx -t
sudo ss -tulpn
```


# 28. Another Scenario

Suppose:

```bash
curl http://localhost
```

works.

But browser cannot access:

```text
http://PUBLIC_IP
```

Now think:

```text
FastAPI       → working
Nginx         → working
Local request → working

Internet request → failing
```

Where should you investigate?

```text
Public IP
    ↓
GCP Firewall
    ↓
Network configuration
    ↓
External connectivity
```

This is systematic debugging.


# 29. Mentor Rule

> **First prove locally. Then prove remotely.**

For example:

```text
Step 1
curl localhost:8000
        ↓
Application works?

Step 2
curl localhost
        ↓
Nginx works?

Step 3
Browser → Public IP
        ↓
Internet access works?
```

Don't test everything simultaneously.

**Reduce the problem layer by layer.**

# 30. Final Architecture

```text
                           USER
                            |
                            | HTTPS / HTTP
                            v
                     +-------------+
                     |    DNS      |
                     +------+------+
                            |
                            v
                     +-------------+
                     | Public IP   |
                     +------+------+
                            |
                            v
                     +-------------+
                     | GCP Firewall|
                     |             |
                     | 80 / 443    |
                     +------+------+
                            |
                            v
                  +--------------------+
                  | GCP Virtual Machine|
                  |                    |
                  |      NGINX         |
                  |        |           |
                  |        | proxy     |
                  |        v           |
                  |     FastAPI        |
                  |       :8000        |
                  +---------+----------+
                            |
                            v
                         Database
```

# Mentor's Closing Thought

> **"Yesterday we created a computer in the cloud. Today we made that computer behave like a web server. Tomorrow we should ask an even bigger question: How do we deploy our application repeatedly without manually logging into the server every time?"**

That question leads naturally to:

```text
Git
  ↓
GitHub
  ↓
CI/CD
  ↓
Docker
  ↓
Deployment
  ↓
GCP
```
