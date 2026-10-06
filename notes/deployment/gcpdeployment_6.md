# Dockerizing FastAPI and Running the Container on GCP VM

We have reached an important point in our journey:

```text
Git
  ↓
GitHub
  ↓
GitHub Actions
  ↓
Testing
  ↓
GCP VM
  ↓
Nginx
  ↓
FastAPI
```

Now the question from a developer is:  **“Sir, my application is working. Why do I need Docker?”**  That is exactly today's discussion.


## 1. The Problem We Have Today

Our FastAPI application currently depends on the VM environment.

```text
GCP VM

 ├── Python
 ├── pip
 ├── virtual environment
 ├── FastAPI
 ├── Uvicorn
 ├── application code
 └── configuration
```

Suppose tomorrow another developer says:  "It works on your machine, but it doesn't work on mine." This is a classic software-development problem. Why? Because the application depends on its environment.

```text
Application
     +
Python version
     +
Libraries
     +
Operating System
     +
Configuration
     +
Runtime
```

Docker gives us a way to **package the application and its runtime dependencies together**.

# 2. What Is Docker?

Think about a courier package. You don't send only the product. You package everything required to deliver it safely. Similarly:

```text
Docker Image
-------------------------
Application
Python runtime
Libraries
Configuration
Startup command
-------------------------
```

When we run that image, we get a:

```text
Container
```

So remember:

```text
Dockerfile
    ↓
Docker Image
    ↓
Docker Container
```

### Mentor Rule

> **Image is the package.
> Container is the running package.**

# 3. VM vs Container

This distinction is extremely important.

```text
              GCP VM
        -------------------
        Linux Operating System
        -------------------
        Docker
          |
    -------------------
    |       |         |
 Container Container Container
   API       API       Worker
```

A Virtual Machine provides a complete operating-system environment. A container shares the host operating system's kernel but isolates the application process and its filesystem/network/process environment.

So:

```text
VM = Virtual Computer
```

while:

```text
Container = Isolated Application Runtime
```

# 4. Our New Architecture

Previously:

```text
Internet
   |
   | HTTP :80
   ↓
GCP Firewall
   |
   ↓
Nginx
   |
   | :8000
   ↓
FastAPI
```

After Docker:

```text
Internet
   |
   | HTTP :80
   ↓
GCP Firewall
   |
   ↓
Nginx
   |
   | localhost:8000
   ↓
Docker Container
   |
   ↓
FastAPI
```

More precisely:

```text
                    GCP VM
 ------------------------------------------------
 |                                              |
 |   Nginx :80                                  |
 |      |                                       |
 |      | localhost:8000                        |
 |      ↓                                       |
 |   Docker                                     |
 |      |                                       |
 |      ↓                                       |
 |   Container                                  |
 |      |                                       |
 |      ↓                                       |
 |   FastAPI :8000                              |
 |                                              |
 ------------------------------------------------
```

Notice something important. We **don't need to expose port 8000 to the Internet**.

Only:

```text
80  → public
443 → public
```

Port `8000` remains internal to the VM.

# 5. Our FastAPI Project

Let's assume:

```text
tfl-api/
│
├── main.py
├── requirements.txt
├── Dockerfile
└── .dockerignore
```

Our `main.py`:

```python
from fastapi import FastAPI

app = FastAPI(title="TFL Insurance API",version="1.0")


@app.get("/")
def hello():
    return {
        "message": "Hello from Transflower"
    }


@app.get("/api/policies")
def get_policies():
    return [
        {"id": 1, "name": "Jeevan Labh", "premium": 15000 },
        { "id": 2, "name": "Jeevan Arogya", "premium": 12000 }
    ]
```

`requirements.txt`:

```text
fastapi
uvicorn
```

# 6. What Is a Dockerfile?

A Dockerfile is a **recipe for creating a Docker image**. For example:

```dockerfile
FROM python:3.12-slim
WORKDIR /app
COPY requirements.txt .
RUN pip install --no-cache-dir -r requirements.txt
COPY . .
EXPOSE 8000
CMD ["uvicorn", "main:app", "--host", "0.0.0.0", "--port", "8000"]
```

Let's understand it line by line.


## 7. FROM

```dockerfile
FROM python:3.12-slim
```

We are saying: "Start with an environment that already contains Python."

```text
Python Base Image
       ↓
Our Application
```

Instead of installing Python manually inside the container, we start from a Python image.

# 8. WORKDIR

```dockerfile
WORKDIR /app
```

This creates/selects the working directory inside the container.

```text
Container
   |
   └── /app
         |
         ├── main.py
         └── requirements.txt
```

Similar to:

```bash
cd /app
```

# 9. COPY requirements.txt

```dockerfile
COPY requirements.txt .
```

Copy our dependency file into the container.

Then:

```dockerfile
RUN pip install --no-cache-dir -r requirements.txt
```

Docker installs:

```text
FastAPI
Uvicorn
```

inside the image.


# 10. COPY Application

```dockerfile
COPY . .
```

This copies our project into `/app`. So the container becomes:

```text
/app
│
├── main.py
└── requirements.txt
```

# 11. EXPOSE

```dockerfile
EXPOSE 8000
```

This documents that our application uses port `8000`. Important mentor point:  `EXPOSE` does **not** by itself open the port to the Internet. Port publishing happens when we run the container.

# 12. CMD

```dockerfile
CMD [
    "uvicorn",
    "main:app",
    "--host",
    "0.0.0.0",
    "--port",
    "8000"
]
```

This tells Docker: "When the container starts, run FastAPI using Uvicorn." Why `0.0.0.0`? Because inside the container, the application must listen on the container's network interface. Remember our earlier lesson:

```text
127.0.0.1
    ↓
Only this machine/interface

0.0.0.0
    ↓
Listen on available interfaces
```

# 13. .dockerignore

We don't want to copy everything into our image. Create:

```text
.dockerignore
```

with:

```text
.git
.github
venv
__pycache__
*.pyc
.env
.pytest_cache
```

Think of it as:

```text
.gitignore
     ↓
Git

.dockerignore
     ↓
Docker
```

# 14. Build the Docker Image

From the project directory:

```bash
docker build -t tfl-api:1.0 .
```

Meaning:

```text
docker build
      |
      +-- create image
      |
      +-- name = tfl-api
      |
      +-- tag = 1.0
      |
      +-- context = current directory
```

Check images:

```bash
docker images
```

You should see something like:

```text
REPOSITORY     TAG     IMAGE ID
tfl-api        1.0     xxxxxxxxx
```

# 15. Run the Container

Now:

```bash
docker run -d \
  --name tfl-api \
  -p 127.0.0.1:8000:8000 \
  --restart unless-stopped \
  tfl-api:1.0
```

Let's understand this.

### `-d`

Run in detached/background mode.

### `--name`

```text
tfl-api
```

Gives the container a meaningful name.

### `-p`

```text
127.0.0.1:8000:8000
```

means:

```text
VM localhost:8000
       |
       ↓
Container port 8000
```

### `--restart unless-stopped`

If the VM or Docker restarts, Docker can restart the container automatically unless we explicitly stopped it.


# 16. Check the Container

```bash
docker ps
```

You should see:

```text
CONTAINER ID
IMAGE
COMMAND
STATUS
PORTS
NAMES
```

For example:

```text
tfl-api
   |
   ↓
tfl-api:1.0
   |
   ↓
127.0.0.1:8000 → container:8000
```

# 17. Test FastAPI

From the VM:

```bash
curl http://127.0.0.1:8000
```

Expected:

```json
{
  "message": "Hello from Transflower"
}
```

Swagger:

```text
http://127.0.0.1:8000/docs
```

But remember: `127.0.0.1` means the VM itself. The Internet user should use:

```text
http://PUBLIC_IP/
```

because Nginx handles the public request.

# 18. Nginx + Docker

Our Nginx configuration remains approximately:

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

So the complete request journey is:

```text
Browser
   |
   | HTTP :80
   ↓
Internet
   |
   ↓
GCP Firewall
   |
   ↓
GCP VM
   |
   ↓
Nginx :80
   |
   | localhost:8000
   ↓
Docker
   |
   ↓
Container
   |
   ↓
Uvicorn
   |
   ↓
FastAPI
```

This is a real deployment architecture.


# 19. Important Firewall Lesson

Students often make this mistake: "My FastAPI uses port 8000, so I should open TCP 8000 in GCP firewall." Not necessarily. Our architecture is:

```text
Internet
   |
   | 80
   ↓
Nginx
   |
   | 8000
   ↓
Docker
```

Therefore:

```text
Public:
80

Internal:
8000
```

This is much better than:

```text
Internet
   |
   | 8000
   ↓
FastAPI
```

### Security principle

> **Expose only what must be publicly accessible.**

# 20. Docker Commands Every Developer Should Know

### List containers

```bash
docker ps
```

### List all containers

```bash
docker ps -a
```

### View logs

```bash
docker logs tfl-api
```

### Follow logs

```bash
docker logs -f tfl-api
```

### Stop

```bash
docker stop tfl-api
```

### Start

```bash
docker start tfl-api
```

### Restart

```bash
docker restart tfl-api
```

### Remove

```bash
docker rm tfl-api
```

### List images

```bash
docker images
```

### Remove image

```bash
docker rmi tfl-api:1.0
```

# 21. The Beautiful Part of Docker

Suppose our application needs:

```text
Python 3.12
FastAPI
Uvicorn
Libraries
Environment
Startup command
```

Without Docker:

```text
Developer
   |
   +-- Install Python
   +-- Install pip
   +-- Create venv
   +-- Install dependencies
   +-- Configure environment
   +-- Run application
```

With Docker:

```text
Dockerfile
    |
    ↓
Docker Image
    |
    ↓
Container
    |
    ↓
Application
```

We have converted: **"Setup instructions"** into: **"Executable infrastructure."** This is one of the reasons Docker became so important in modern DevOps.



# 22. Image vs Container

This is a common interview question.

### Image

```text
Docker Image = Read-only packaged template
```

Example:

```text
tfl-api:1.0
```

### Container

```text
Container = Running instance of an image
```

Example:

```text
tfl-api
```

Analogy:

```text
Class
  ↓
Object

Docker Image
  ↓
Container
```

Excellent connection for an OOP developer.


# 23. One Image, Multiple Containers

Suppose we have:

```text
tfl-api:1.0
```

We can create:

```text
Container 1
Container 2
Container 3
```

from the same image.

```text
             tfl-api:1.0
                  |
       +----------+----------+
       |          |          |
       ↓          ↓          ↓
    API-1       API-2      API-3
```

This is the foundation for scaling applications.

# 24. What Changes in Our Deployment?

Earlier:

```text
GitHub
   ↓
GCP VM
   ↓
git pull
   ↓
pip install
   ↓
systemctl restart
```

Now:

```text
GitHub
   ↓
Docker Build
   ↓
Docker Image
   ↓
GCP VM
   ↓
Docker Container
   ↓
Nginx
   ↓
User
```

The next logical evolution is:

```text
Developer
    ↓
GitHub
    ↓
GitHub Actions
    ↓
Run Tests
    ↓
Build Docker Image
    ↓
Push Image to Registry
    ↓
GCP VM pulls Image
    ↓
Run Container
    ↓
Nginx
    ↓
FastAPI
```

Now we are entering **real CI/CD with containers**.


# 25. Mentor's Question

Imagine we release:

```text
tfl-api:1.0
```

Today.

Tomorrow we change the application.

Should we overwrite the old image?

Better:

```text
tfl-api:1.0
tfl-api:1.1
tfl-api:1.2
tfl-api:2.0
```

Now deployment becomes versioned.

```text
Production
    |
    ↓
tfl-api:1.2

If something goes wrong
    |
    ↓
Rollback
    |
    ↓
tfl-api:1.1
```

This is where Docker starts connecting with:

```text
CI/CD
Versioning
Deployment
Rollback
Scaling
Cloud
DevOps
```

# 26. Today's Practical Lab

### Step 1

Create:

```text
Dockerfile
```

### Step 2

Create:

```text
.dockerignore
```

### Step 3

Build:

```bash
docker build -t tfl-api:1.0 .
```

### Step 4

Run:

```bash
docker run -d \
  --name tfl-api \
  -p 127.0.0.1:8000:8000 \
  --restart unless-stopped \
  tfl-api:1.0
```

### Step 5

Check:

```bash
docker ps
```

### Step 6

Test:

```bash
curl http://127.0.0.1:8000
```

### Step 7

Check logs:

```bash
docker logs tfl-api
```

### Step 8

Open from your browser:

```text
http://YOUR_PUBLIC_IP/
```

# 27. Transflower Developer Mindset

Don't memorize:

```bash
docker build
docker run
docker ps
docker logs
```

Understand the architecture.

```text
              SOURCE CODE
                   |
                   ↓
              Dockerfile
                   |
                   ↓
              Docker Image
                   |
                   ↓
              Container
                   |
                   ↓
               FastAPI
                   |
                   ↓
                Uvicorn
                   |
                   ↓
                Nginx
                   |
                   ↓
              Public Internet
```

### Remember this sentence:

> **Docker packages the application.
> Nginx exposes the application.
> GCP provides the infrastructure.
> GitHub stores the source.
> GitHub Actions automates the delivery.**

# 28. Where We Go Next

Our journey is now:

```text
                  TRANSFLOWER DEVOPS JOURNEY

Git
 ↓
GitHub
 ↓
CI
 ↓
Testing
 ↓
GCP VM
 ↓
Nginx
 ↓
Docker
 ↓
FastAPI
```

The next problem is very natural: **"Sir, why should I build the Docker image manually on the GCP VM? Can't GitHub Actions build it automatically?"** Absolutely. That takes us to the next session:

```text
GitHub Actions
      ↓
Docker Build
      ↓
Docker Image
      ↓
Container Registry
      ↓
GCP VM
      ↓
Docker Pull
      ↓
New Container
      ↓
Nginx
      ↓
FastAPI
```

**That is the bridge from Docker knowledge to production-style CI/CD.**