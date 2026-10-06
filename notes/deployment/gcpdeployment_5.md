# CI/CD with GitHub Actions → Test → Deploy to GCP VM

> **Mentor Question:**
> We have successfully deployed our FastAPI application to a GCP VM.But every time a developer changes the code, should we manually:
>
> ```text
> SSH → git pull → install → test → restart
> ```
>
> **No.**
>
> Let's automate the journey.


# 1. Where We Are

Our current architecture is:

```text
Developer
    |
    | git push
    v
GitHub
    |
    v
GCP VM
    |
    +-- Linux
    +-- Nginx
    +-- FastAPI
```

We want:

```text
Developer
    |
    | git push
    v
GitHub
    |
    v
CI/CD Pipeline
    |
    +---- Test
    |
    +---- Build
    |
    +---- Deploy
    |
    v
GCP VM
    |
    v
Application
```


# 2. What is CI?

**CI = Continuous Integration**

Developers continuously integrate their changes into a shared repository. When code is pushed:

```text
git push
   |
   v
GitHub
   |
   v
Build
   |
   v
Tests
```

The basic question is: **"Does the new code still work?"**

# 3. What is CD?

CD can mean **Continuous Delivery** or **Continuous Deployment**, depending on the workflow. For our learning example:

```text
Code
  |
  v
Build
  |
  v
Test
  |
  v
Deploy
```

The pipeline answers:

> **"If the code passes validation, can we deliver it to the environment?"**


# 4. The Complete Pipeline

```text
                    DEVELOPER
                        |
                        | git push
                        v
                 +--------------+
                 |   GitHub     |
                 +------+-------+
                        |
                        v
                +---------------+
                | GitHub Actions|
                +-------+-------+
                        |
              +---------+---------+
              |                   |
              v                   v
           Install              Test
              |                   |
              +---------+---------+
                        |
                      PASS?
                     /    \
                   NO      YES
                   |        |
                   v        v
                 STOP     Deploy
                            |
                            v
                       GCP VM
                            |
                            v
                          Nginx
                            |
                            v
                         FastAPI
```

This is the architecture we are going to build.


# 5. Why Automated Testing Before Deployment?

Imagine a developer changes:

```python
@app.get("/api/policies")
```

and accidentally introduces:

```python
@app.get("/api/policys")
```

If we deploy immediately:

```text
Developer
   |
   v
GitHub
   |
   v
Production
   |
   v
BUG
```

Instead:

```text
Developer
   |
   v
GitHub
   |
   v
Automated Test
   |
   X
FAIL
   |
   v
Deployment STOPPED
```

This is the fundamental idea: **Don't deploy code that has failed validation.**


# 6. Our FastAPI Project

Let's use:

```text
tfl-api/
 |
 +-- main.py
 +-- requirements.txt
 +-- test_main.py
 +-- .gitignore
 +-- README.md
```

# 7. Application

`main.py`

```python
from fastapi import FastAPI

app = FastAPI( title="TFL Insurance API", version="1.0")

@app.get("/")
def hello():
    return {
        "message": "Hello from Transflower"
    }

@app.get("/api/policies")
def get_policies():
    return [
        {"id": 1,  "name": "Jeevan Labh", "premium": 15000 },
        {"id": 2,  "name": "Jeevan Arogya", "premium": 12000 }
    ]
```

# 8. Why Test the API?

We can write a simple automated test. Install:

```text
pytest
httpx
```

Our `requirements.txt` becomes:

```text
fastapi
uvicorn
pytest
httpx
```

# 9. Create a Test

`test_main.py`

```python
from fastapi.testclient import TestClient
from main import app

client = TestClient(app)


def test_root():
    response = client.get("/")
    assert response.status_code == 200
    assert response.json()["message"] == "Hello from Transflower"

def test_get_policies():
    response = client.get("/api/policies")
    assert response.status_code == 200
    policies = response.json()
    assert len(policies) >= 1
```

Now we have:

```text
Application
    |
    v
Automated Tests
```

# 10. Run Tests Locally

Before involving GitHub:

```bash
pytest
```

Expected:

```text
2 passed
```

This is important. **CI should automate a test that already works locally.**  Don't write the pipeline first and discover later that your test itself is broken.

# 11. Git Status

Run:

```bash
git status
```

Then:

```bash
git add .
```

Commit:

```bash
git commit -m "Add automated API tests"
```

Push:

```bash
git push origin main
```

Now GitHub receives the new code.


# 12. Enter GitHub Actions

GitHub provides an automation mechanism called:

**GitHub Actions**

Think of it as:

```text
"When something happens in my repository,
execute these steps."
```

For example:

```text
WHEN:
    code is pushed

DO:
    install Python
    install dependencies
    run tests
```


# 13. Workflow File

GitHub Actions workflows normally live under:

```text
.github/
   |
   +-- workflows/
          |
          +-- ci.yml
```

So create:

```text
.github/workflows/ci.yml
```


# 14. Basic CI Workflow

```yaml
name: TFL API CI

on:
  push:
    branches:
      - main

  pull_request:
    branches:
      - main

jobs:

  test:

    runs-on: ubuntu-latest

    steps:

      - name: Checkout source code
        uses: actions/checkout@v4

      - name: Setup Python
        uses: actions/setup-python@v5
        with:
          python-version: "3.12"

      - name: Install dependencies
        run: |
          python -m pip install --upgrade pip
          pip install -r requirements.txt

      - name: Run tests
        run: |
          pytest
```

# 15. Understand the YAML

Don't just copy it. Let's understand it.

### Workflow name

```yaml
name: TFL API CI
```

Human-readable name.

### Trigger

```yaml
on:
  push:
    branches:
      - main
```

Meaning:  When somebody pushes code to `main`, run the workflow.

### Job

```yaml
jobs:
  test:
```

We have a job called:

```text
test
```

### Runner

```yaml
runs-on: ubuntu-latest
```

GitHub provides a temporary Ubuntu environment to execute the job.

Think:

```text
GitHub
   |
   +-- Temporary Linux Machine
          |
          +-- Python
          +-- Dependencies
          +-- Tests
```

# 16. Checkout

```yaml
- name: Checkout source code
  uses: actions/checkout@v4
```

This downloads our repository into the GitHub Actions runner. Conceptually:

```text
GitHub Repository
       |
       | checkout
       v
CI Runner
```

# 17. Setup Python

```yaml
- name: Setup Python
  uses: actions/setup-python@v5
```

The runner gets the required Python environment.

# 18. Install Dependencies

```yaml
- name: Install dependencies
  run: |
    pip install -r requirements.txt
```

Now:

```text
requirements.txt
       |
       v
FastAPI
Uvicorn
Pytest
HTTPX
```

# 19. Run Tests

```yaml
- name: Run tests
  run: |
    pytest
```

If:

```text
2 passed
```

the job succeeds.

If:

```text
1 failed
```

the job fails.

# 20. What Happens After `git push`?

Developer:

```bash
git push origin main
```

GitHub:

```text
Repository
    |
    v
GitHub Actions
    |
    v
Checkout
    |
    v
Install Python
    |
    v
Install dependencies
    |
    v
pytest
```

If successful:

```text
✓ CI PASSED
```

If unsuccessful:

```text
✗ CI FAILED
```

# 21. This Is Continuous Integration

The developer doesn't need to manually say:  "Let me test the project."  GitHub does it.

```text
Developer
    |
    v
git push
    |
    v
Automatic Test
```

That's the **CI** part.

# 22. Now We Need Deployment

CI currently ends here:

```text
GitHub
   |
   v
Test
   |
   v
PASS
```

We want:

```text
GitHub
   |
   v
Test
   |
   v
PASS
   |
   v
Deploy
   |
   v
GCP VM
```

This is the **CD** part.


# 23. How Can GitHub Reach the VM?

We need a secure mechanism. One common approach is:

```text
GitHub Actions
      |
      | SSH
      v
GCP VM
```

But SSH requires authentication. We should **not** put passwords in the workflow. Instead, use an SSH key and store sensitive values as GitHub Secrets.

# 24. Secrets

Suppose we need:

```text
VM_HOST
VM_USER
SSH_PRIVATE_KEY
```

We don't write them directly into:

```yaml
VM_HOST: "34.xx.xx.xx"
```

or:

```yaml
SSH_PRIVATE_KEY: "-----BEGIN PRIVATE KEY-----..."
```

in the repository.

Instead:

```text
GitHub Secrets
     |
     +-- VM_HOST
     +-- VM_USER
     +-- SSH_PRIVATE_KEY
```

GitHub Actions can access them during the workflow.


# 25. Security Principle

> **Code can be public. Secrets should not be.**

Examples of secrets:

```text
Database password
API key
SSH private key
JWT signing secret
Cloud credentials
```

Never commit them into Git.

# 26. Deployment Flow

Our desired pipeline:

```text
Developer
    |
    | git push
    v
GitHub
    |
    v
CI
    |
    +---- Install
    +---- Test
    |
    v
PASS
    |
    v
SSH
    |
    v
GCP VM
    |
    +---- git pull
    +---- install/update dependencies
    +---- restart application
    |
    v
Nginx
    |
    v
FastAPI
```

# 27. Manual Deployment First

Before automating deployment, understand what the pipeline will do manually. 

SSH into VM:

```bash
cd ~/apps/tfl-api
```

Pull:

```bash
git pull origin main
```

Install:

```bash
source venv/bin/activate
pip install -r requirements.txt
```

Then restart the application.

This is the process we will automate.


# 28. Why systemd?

Earlier we started FastAPI manually:

```bash
uvicorn main:app ...
```

That's not ideal for production.

Instead, we create a Linux service.

For example:

```text
systemd
   |
   v
tfl-api.service
   |
   v
Uvicorn
   |
   v
FastAPI
```

Then:

```bash
sudo systemctl restart tfl-api
```

becomes our deployment restart command.


# 29. Example systemd Service

Create:

```bash
sudo nano /etc/systemd/system/tfl-api.service
```

Example:

```ini
[Unit]
Description=TFL FastAPI Application
After=network.target

[Service]
User=YOUR_VM_USER
WorkingDirectory=/home/YOUR_VM_USER/apps/tfl-api
Environment="PATH=/home/YOUR_VM_USER/apps/tfl-api/venv/bin"
ExecStart=/home/YOUR_VM_USER/apps/tfl-api/venv/bin/uvicorn main:app --host 127.0.0.1 --port 8000

Restart=always

[Install]
WantedBy=multi-user.target
```

Replace:

```text
YOUR_VM_USER
```

with the actual Linux username on your VM.


# 30. Enable the Service

```bash
sudo systemctl daemon-reload
```

Enable it:

```bash
sudo systemctl enable tfl-api
```

Start it:

```bash
sudo systemctl start tfl-api
```

Check:

```bash
sudo systemctl status tfl-api
```

Now FastAPI behaves like a proper Linux service.


# 31. Deployment Becomes Simple

Instead of:

```bash
uvicorn main:app ...
```

we can use:

```bash
sudo systemctl restart tfl-api
```

Our deployment sequence becomes:

```text
git pull
   |
   v
pip install
   |
   v
systemctl restart tfl-api
```

That's much easier to automate.


# 32. CI/CD Pipeline

Now visualize:

```text
                  git push
                     |
                     v
              +-------------+
              |   GitHub    |
              +------+------+
                     |
                     v
             +---------------+
             | GitHub Action |
             +-------+-------+
                     |
              +------+------+
              |             |
              v             v
           Install        Test
              |             |
              +------+------+
                     |
                   PASS
                     |
                     v
                    SSH
                     |
                     v
              +-------------+
              |   GCP VM    |
              +------+------+
                     |
                  git pull
                     |
               pip install
                     |
             systemctl restart
                     |
                     v
                  FastAPI
```

# 33. Important Production Question

Should we deploy directly from every developer's branch? Usually no. A better flow:

```text
Developer
    |
    v
feature branch
    |
    v
Pull Request
    |
    v
CI Tests
    |
    v
Code Review
    |
    v
main
    |
    v
Deployment
```

This gives us:

```text
Quality
+
Review
+
Automation
```

# 34. Pull Request Architecture

```text
Developer
    |
    v
feature/login
    |
    v
Pull Request
    |
    v
GitHub Actions
    |
    v
Automated Tests
    |
    +---- FAIL → Fix
    |
    +---- PASS
            |
            v
       Code Review
            |
            v
          Merge
            |
            v
           main
            |
            v
        Deployment
```

This is much closer to professional software development.


# 35. The Four Environments

As projects grow, we often separate:

```text
Development
     |
     v
Testing
     |
     v
Staging
     |
     v
Production
```

Example:

```text
Developer
   |
   v
DEV
   |
   v
CI Tests
   |
   v
STAGING
   |
   v
Approval
   |
   v
PRODUCTION
```

Don't let the first code change immediately destroy production.



# 36. Deployment Safety

Imagine the new application version is broken. We don't want:

```text
Bad Code
   |
   v
Production
   |
   v
Users
   |
   v
ERROR
```

Instead:

```text
Code
 |
 v
Test
 |
 v
Validate
 |
 v
Deploy
 |
 v
Health Check
 |
 +---- PASS → Continue
 |
 +---- FAIL → Rollback
```

This introduces another important DevOps concept:

# **Rollback**


# 37. Rollback

Git gives us versions:

```text
v1
 |
 v
v2
 |
 v
v3
```

Suppose v3 is broken.

We can return to a known-good version.

```text
v1 → v2 → v3
             X
             |
             v
           Rollback
             |
             v
            v2
```

This is one of the biggest advantages of version control.

# 38. Mentor Analogy

Imagine a bank transaction. Before committing:

```text
Check
  |
  v
Validate
  |
  v
Commit
```

If validation fails:

```text
STOP
```

CI/CD is similar:

```text
Code
  |
  v
Validate
  |
  v
Test
  |
  v
Deploy
```

If testing fails:

```text
STOP
```

Don't push bad software forward.


# 39. CI/CD Is Not Magic

Students often think:  "GitHub Actions deploys my application."  No. GitHub Actions is an **automation engine**. We tell it:

```text
WHEN?
WHAT?
WHERE?
HOW?
```

For example:

```text
WHEN:
    push to main

WHAT:
    run tests

WHERE:
    GitHub runner

IF PASS:
    connect to server

HOW:
    SSH

THEN:
    pull code
    install dependencies
    restart service
```

That's automation.

# 40. Complete Transflower Architecture

```text
                         DEVELOPER
                             |
                             |
                         git push
                             |
                             v
                      +-------------+
                      |   GitHub    |
                      +------+------+
                             |
                             v
                    +----------------+
                    | GitHub Actions |
                    +-------+--------+
                            |
             +--------------+--------------+
             |                             |
             v                             v
       Build / Install                 Automated Test
             |                             |
             +--------------+--------------+
                            |
                           PASS
                            |
                            v
                      Deployment
                            |
                            | SSH
                            v
                    +---------------+
                    |    GCP VM     |
                    |               |
                    | Debian Linux  |
                    |               |
                    | Nginx :80     |
                    |      |        |
                    |      v        |
                    | FastAPI :8000 |
                    +-------+-------+
                            |
                            v
                         Database
```

# 41. Mentor's Developer Journey

Look at how far we have travelled:

```text
Python
   ↓
FastAPI
   ↓
REST API
   ↓
Linux
   ↓
Virtual Machine
   ↓
GCP
   ↓
Firewall
   ↓
SSH
   ↓
Nginx
   ↓
Git
   ↓
GitHub
   ↓
Testing
   ↓
CI
   ↓
CD
```

This is not about memorizing 100 commands. It is about understanding the **software delivery system**.



# 42. Today's Practical Lab

Complete these stages.

### Stage 1 — Local

```text
[ ] FastAPI application
[ ] requirements.txt
[ ] pytest tests
[ ] .gitignore
```

### Stage 2 — GitHub

```text
[ ] git init
[ ] git add
[ ] git commit
[ ] git push
```

### Stage 3 — CI

Create:

```text
.github/workflows/ci.yml
```

and verify:

```text
[ ] GitHub Actions starts
[ ] Dependencies install
[ ] pytest runs
[ ] CI passes
```

### Stage 4 — GCP

Verify:

```text
[ ] VM running
[ ] SSH working
[ ] Git installed
[ ] Repository cloned
[ ] FastAPI running
[ ] systemd service running
[ ] Nginx running
```

### Stage 5 — Deployment

Understand:

```text
git push
   ↓
CI
   ↓
Test
   ↓
Deploy
   ↓
GCP VM
   ↓
Restart service
```

# 43. Final Mental Model

Don't remember GitHub Actions syntax first. Remember this:

```text
                  SOURCE
                    |
                    v
                  GIT
                    |
                    v
                 GITHUB
                    |
                    v
                    CI
                    |
              +-----+-----+
              |           |
              v           v
            BUILD       TEST
              |           |
              +-----+-----+
                    |
                  PASS
                    |
                    v
                    CD
                    |
                    v
                 DEPLOY
                    |
                    v
                  GCP
                    |
                    v
                   VM
                    |
                    v
                 NGINX
                    |
                    v
               APPLICATION
                    |
                    v
                  USER
```

## Mentor Closing Thought

> **"DevOps is not a collection of tools. Git, GitHub Actions, Linux, Docker, GCP, Nginx and Kubernetes are tools. The real skill is understanding how these tools work together to deliver reliable software."**

And our next question naturally becomes:

> **"Why do we need Docker if our application is already running successfully on the GCP VM?"**

That leads to the next session:

```text
Docker
   ↓
Dockerfile
   ↓
Docker Image
   ↓
Container
   ↓
GCP VM
   ↓
Nginx
   ↓
FastAPI
```

**Next: Dockerizing the FastAPI application and running the container on the GCP VM.**
