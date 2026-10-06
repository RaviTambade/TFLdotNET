# Git → GitHub → Clone Project → Configure → Run Application on GCP VM

> **Mentor Question:**
> We have a GCP VM.  We have Linux.  We have Nginx.  We have FastAPI.
>
> But there is one problem:
>
> **Where does our application code come from?**

The answer is:

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
GCP VM
    |
    v
Application
```


# 1. Yesterday's Architecture

We reached this:

```text
                       INTERNET
                           |
                           v
                       Public IP
                           |
                           v
                    GCP Firewall
                           |
                           v
                     Nginx :80
                           |
                           |
                    proxy_pass
                           |
                           v
                   FastAPI :8000
                           |
                           v
                       Database
```

Today we introduce **source-code management and deployment**.

```text
Developer
    |
    v
Git Repository
    |
    v
GitHub
    |
    v
GCP VM
    |
    v
Application
```



# 2. Why Git?

Imagine 5 developers working on one project.

```text
Developer A
Developer B
Developer C
Developer D
Developer E
```

Everyone modifies the same source code. Without version control:

```text
final.py
final_new.py
final_latest.py
final_latest2.py
final_really_latest.py
```

This becomes a mess. Git solves this problem.


# 3. Git is Version Control

Git helps us track:

```text
Who changed the code?
What changed?
When did it change?
Why did it change?
Can we go back?
Can multiple developers work together?
```

Basic model:

```text
Working Directory
       |
       | git add
       v
Staging Area
       |
       | git commit
       v
Local Repository
       |
       | git push
       v
GitHub
```

# 4. GitHub

GitHub is a hosted platform where Git repositories can be stored and collaborated on. Think:

```text
Git
 |
 +-- Local version control
 |
 +-- Branches
 |
 +-- Commits
 |
 +-- History
```

GitHub adds:

```text
GitHub
 |
 +-- Remote repository
 +-- Collaboration
 +-- Pull Requests
 +-- Issues
 +-- Actions / CI/CD
```

# 5. Developer-to-Cloud Journey

Our deployment journey becomes:

```text
Developer Laptop
       |
       | git push
       v
    GitHub
       |
       | git clone / pull
       v
    GCP VM
       |
       v
    Linux
       |
       v
 Application
```

This is the simplest manual deployment workflow.

# 6. Prepare a Simple Project

Let's use our FastAPI application.

Project:

```text
tfl-api/
 |
 +-- main.py
 +-- requirements.txt
 +-- README.md
```

`main.py`:

```python
from fastapi import FastAPI

app = FastAPI(title="TFL Insurance API", version="1.0")

@app.get("/")
def hello():
    return {
        "message": "Hello from Transflower"
    }

@app.get("/api/policies")
def get_policies():
    return [
        {"id": 1,"name": "Jeevan Labh","premium": 15000},
        { "id": 2,"name": "Jeevan Arogya", "premium": 12000}
    ]
```

# 7. requirements.txt

Create:

```text
fastapi
uvicorn
```

The purpose is important. Instead of telling another developer:

> "Install FastAPI and Uvicorn."

we provide:

```text
requirements.txt
```

Then the developer can run:

```bash
pip install -r requirements.txt
```

This is **dependency management**.


# 8. Initialize Git

From the project directory:

```bash
git init
```

Git creates:

```text
.git/
```

Now:

```text
tfl-api/
 |
 +-- .git/
 +-- main.py
 +-- requirements.txt
 +-- README.md
```

`.git` contains Git's repository information.

# 9. Check Git Status

Run:

```bash
git status
```

You may see:

```text
Untracked files:

main.py
requirements.txt
README.md
```

Git knows these files exist, but they haven't been added to the next commit.

# 10. Stage Files

Run:

```bash
git add .
```

Then:

```bash
git status
```

Now Git shows files ready to be committed. Think:

```text
Working Directory
       |
       | git add
       v
Staging Area
```

# 11. Commit

Run:

```bash
git commit -m "Initial FastAPI application"
```

Now we have a version.

```text
Commit
  |
  v
Application Version 1
```

Later:

```text
Commit 1
Commit 2
Commit 3
Commit 4
```

Git gives us a history of the project.


# 12. Create GitHub Repository

Create a repository on GitHub, for example:

```text
tfl-api
```

You will have a remote repository.

Conceptually:

```text
LOCAL

tfl-api
   |
   v
Git


REMOTE

GitHub
   |
   v
tfl-api
```

# 13. Connect Local Git to GitHub

Git needs to know where the remote repository is. For example:

```bash
git remote add origin <YOUR-GITHUB-REPOSITORY>
```

Check:

```bash
git remote -v
```

You should see the remote repository.



# 14. Push Code

For a new repository:

```bash
git branch -M main
```

Then:

```bash
git push -u origin main
```

Now:

```text
Developer Laptop
       |
       | git push
       v
     GitHub
       |
       v
   tfl-api
```

Your source code is now available remotely.


# 15. Important Question

> **Why don't we simply copy files from our laptop to the VM?**

We could. For example:

```text
Laptop
   |
   | SCP
   v
VM
```

But Git gives us:

```text
Version history
Branches
Commits
Collaboration
Rollback
Code review
CI/CD integration
```

Therefore:

> **Git becomes the bridge between development and deployment.**

# 16. Now Go to GCP VM

SSH into your VM.

```text
GCP Console
   |
   v
Compute Engine
   |
   v
VM
   |
   v
SSH
```

Check Git:

```bash
git --version
```

If Git isn't installed:

```bash
sudo apt update
sudo apt install git -y
```

Verify:

```bash
git --version
```

# 17. Clone the Repository

Create an application directory:

```bash
mkdir -p ~/apps
cd ~/apps
```

Clone:

```bash
git clone <YOUR-GITHUB-REPOSITORY>
```

Now:

```text
~/apps/
 |
 +-- tfl-api/
       |
       +-- main.py
       +-- requirements.txt
       +-- README.md
```


# 18. This is a Big Moment

Think about what just happened.

Your laptop had:

```text
main.py
```

You pushed it to:

```text
GitHub
```

The GCP VM downloaded it:

```text
GitHub
   |
   | git clone
   v
GCP VM
```

Now the application source code exists inside a cloud computer.

# 19. Create Python Virtual Environment

Move into the project:

```bash
cd ~/apps/tfl-api
```

Create virtual environment:

```bash
python3 -m venv venv
```

Activate:

```bash
source venv/bin/activate
```

Install dependencies:

```bash
pip install -r requirements.txt
```

Now:

```text
VM
 |
 +-- Python
 |
 +-- Virtual Environment
       |
       +-- FastAPI
       +-- Uvicorn
```

# 20. Run the Application

For the first test:

```bash
uvicorn main:app --host 127.0.0.1 --port 8000
```

Open another SSH terminal.

Test:

```bash
curl http://127.0.0.1:8000
```

Expected:

```json
{
    "message": "Hello from Transflower"
}
```

Test:

```bash
curl http://127.0.0.1:8000/api/policies
```

Expected:

```json
[
    {   "id": 1, "name": "Jeevan Labh", "premium": 15000 },
    {   "id": 2, "name": "Jeevan Arogya", "premium": 12000 }
]
```


# 21. Connect Nginx

We already configured:

```text
Nginx :80
       |
       v
127.0.0.1:8000
```

Therefore:

```text
Browser
   |
   | http://PUBLIC-IP
   v
Nginx :80
   |
   v
FastAPI :8000
```

Open:

```text
http://YOUR_PUBLIC_IP
```

Then:

```text
http://YOUR_PUBLIC_IP/api/policies
```

# 22. What Have We Achieved?

We started with:

```text
Python Code
```

and reached:

```text
Internet
    |
    v
GCP Public IP
    |
    v
Firewall
    |
    v
Nginx
    |
    v
FastAPI
    |
    v
Python Code
```

But the source code journey was:

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
git clone
    |
    v
GCP VM
    |
    v
Python Environment
    |
    v
FastAPI
```

This is the essence of deployment.


# 23. But There Is a Problem

Suppose tomorrow you change:

```python
"Hello from Transflower"
```

to:

```python
"Hello from Transflower GCP"
```

What should you do?

Old-fashioned approach:

```text
SSH
   |
Edit file manually
   |
Restart application
```

Not ideal.

Instead:

```text
Developer
   |
   v
git commit
   |
   v
git push
   |
   v
GitHub
   |
   v
GCP VM
   |
   v
git pull
   |
   v
Restart
```

This is better.

# 24. `git pull`

On the VM:

```bash
git pull origin main
```

Git downloads the latest changes.

Then:

```bash
pip install -r requirements.txt
```

if dependencies changed.

Then restart the application.

This is **manual deployment**.


# 25. Application Process Problem

There is another issue. If you run:

```bash
uvicorn main:app --host 127.0.0.1 --port 8000
```

inside SSH and close the terminal, the process may stop. We need a proper process manager/service. This leads us to:

```text
systemd
```

or another process supervisor.

# 26. Production-Like Architecture

Instead of manually running:

```bash
uvicorn main:app ...
```

we can configure:

```text
systemd
   |
   v
Uvicorn
   |
   v
FastAPI
```

Architecture:

```text
Linux
 |
 +-- Nginx
 |
 +-- systemd
       |
       +-- FastAPI service
```

systemd can help with:

* Starting the application
* Stopping the application
* Restarting the application
* Starting after VM reboot
* Managing service state

# 27. Service-Oriented Thinking

Instead of:

```text
"I ran Python."
```

we want:

```text
"FastAPI is running as a Linux service."
```

That is a significant step toward production operations.

# 28. Environment Variables

Now imagine our application connects to MySQL.

We need:

```text
DATABASE_HOST
DATABASE_NAME
DATABASE_USER
DATABASE_PASSWORD
```

Should we write the password directly into:

```python
database_password = "mypassword"
```

**Absolutely not.**

Why? Because the source code goes to GitHub. Then:

```text
GitHub
   |
   v
Public / Team Repository
   |
   v
Password exposed
```

This is a security problem.

# 29. Configuration vs Code

Application code:

```text
main.py
```

Configuration:

```text
DATABASE_HOST
DATABASE_USER
DATABASE_PASSWORD
```

These should be separated.

Think:

```text
Application
     |
     +---- Code
     |
     +---- Configuration
     |
     +---- Secrets
```

# 30. Environment Variables

Linux provides environment variables. For example:

```bash
export APP_ENV=production
```

Check:

```bash
echo $APP_ENV
```

Output:

```text
production
```

Python can read it:

```python
import os

environment = os.getenv("APP_ENV")
```

Now:

```text
Code
 |
 | reads
 v
Environment
 |
 v
Configuration
```

# 31. Mentor Rule: Never Commit Secrets

Never put:

```text
password
API key
private key
database secret
JWT secret
cloud credentials
```

into Git.

Use:

```text
Environment Variables
Secret Manager
Deployment Configuration
```

instead.


# 32. `.gitignore`

Create:

```text
.gitignore
```

Example:

```text
venv/
__pycache__/
.env
*.log
```

Why? Because these files should generally not be committed. Check:

```bash
git status
```

The virtual environment should not become part of your source repository.


# 33. Developer Mindset

A beginner thinks: "My code works on my laptop."

A professional asks:"Can I reproduce this environment somewhere else?"

A DevOps-minded developer asks:"Can I build, test and deploy it automatically?"

This is the progression:

```text
Works on My Machine
        |
        v
Reproducible
        |
        v
Deployable
        |
        v
Automated
        |
        v
Observable
        |
        v
Scalable
```

# 34. Complete Journey So Far

```text
                 DEVELOPER
                     |
                     v
                  Git
                     |
                     v
                 GitHub
                     |
                     | clone
                     v
              +-------------+
              |   GCP VM    |
              |             |
              | Debian      |
              |             |
              | Python      |
              | FastAPI     |
              |             |
              | Nginx       |
              +------+------+
                     |
                     v
                 Firewall
                     |
                     v
                  Internet
                     |
                     v
                  USER
```


# 35. The Next Level

We now have:

```text
Git
GitHub
GCP
VM
Linux
SSH
Firewall
Nginx
FastAPI
Environment Variables
```

But deployment is still manual.

We currently do:

```text
git push
   |
SSH
   |
git pull
   |
install dependencies
   |
restart application
```

Imagine doing that **20 times every day**. That is where CI/CD comes in.


# 36. CI/CD

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
    +---- Build
    |
    +---- Test
    |
    +---- Package
    |
    +---- Deploy
    |
    v
GCP VM
    |
    v
Application
```

Instead of: "Ravi Sir, please SSH into the server and deploy my code." the system says: "Code was merged. Pipeline passed.  Application deployed." That is automation.


# 37. Transflower Mentor Story

Imagine a restaurant.

### Manual deployment

Every time the customer orders:

```text
Customer
   |
   v
Chef
   |
   v
Chef buys vegetables
   |
   v
Chef prepares kitchen
   |
   v
Chef cooks
```

Very slow.

### Automated kitchen

```text
Order
  |
  v
Kitchen System
  |
  +---- Ingredients
  +---- Preparation
  +---- Cooking
  +---- Quality Check
  |
  v
Food
```

CI/CD is the **automated kitchen for software delivery**.

# 38. Today's Lab Checklist

Students should complete:

```text
[ ] Create FastAPI project
[ ] Create requirements.txt
[ ] Initialize Git
[ ] Commit code
[ ] Create GitHub repository
[ ] Push code
[ ] SSH into GCP VM
[ ] Install Git
[ ] Clone repository
[ ] Create Python virtual environment
[ ] Install dependencies
[ ] Run FastAPI
[ ] Configure Nginx
[ ] Access API through public IP
[ ] Create .gitignore
[ ] Understand environment variables
```

# 39. Final Architecture

```text
                       DEVELOPER
                           |
                           | git push
                           v
                       +--------+
                       | GitHub |
                       +---+----+
                           |
                           |
                     Future CI/CD
                           |
                           v
                 +-------------------+
                 |    GCP VM         |
                 |                   |
                 |    Linux          |
                 |       |           |
                 |     Nginx :80     |
                 |       |           |
                 |       v           |
                 |   FastAPI :8000   |
                 |       |           |
                 |       v           |
                 |    Database       |
                 +---------+---------+
                           |
                           v
                       INTERNET
                           |
                           v
                          USER
```

## Mentor's Closing Thought

> **"Cloud deployment is not simply copying your code to a server. It is creating a repeatable path from source code to running software."**

And remember the journey:

```text
CODE
  ↓
GIT
  ↓
GITHUB
  ↓
CI/CD
  ↓
GCP
  ↓
VM
  ↓
NGINX
  ↓
APPLICATION
  ↓
USER
```