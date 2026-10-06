# Exploring Google Cloud Platform — From Cloud Basics to Deployment

## 1. The First Question: What is Cloud Computing?

Imagine a software company wants to build an insurance application. Traditionally, the company needs to purchase:

* Servers
* Hard disks
* RAM
* Networking equipment
* Firewalls
* Data-center space
* Backup systems
* Power and cooling

That means huge **capital investment** and significant operational responsibility. Cloud computing changes this model.

```text
Traditional IT

Company
   |
   +---- Server
   +---- Storage
   +---- Network
   +---- Database
   +---- Security
   +---- Data Center
```

With cloud:

```text
             INTERNET
                 |
                 v
        +------------------+
        |   CLOUD PROVIDER |
        |                  |
        | Compute          |
        | Storage          |
        | Database         |
        | Network          |
        | Security         |
        | AI / Analytics   |
        +------------------+
                 |
                 v
          Your Application
```

The fundamental idea is:  **Don't necessarily own the infrastructure. Consume it as a service.**

# 2. What is Google Cloud Platform?

**Google Cloud Platform (GCP)** is Google's public cloud platform. It provides services such as:

```text
GCP
 |
 +-- Compute
 |
 +-- Storage
 |
 +-- Database
 |
 +-- Networking
 |
 +-- Security
 |
 +-- AI
 |
 +-- Analytics
```

Instead of purchasing physical infrastructure, developers can provision computing resources through the internet. The session defined GCP as a public cloud platform providing computing, storage, database, networking, security, AI and analytics services on demand. 


# 3. Google Apps ≠ Google Cloud

This is an important conceptual distinction. Students commonly say:  "I am using Google, so I am using Google Cloud." Not necessarily.

### Google Apps

Examples:

* Gmail
* Google Drive
* Google Meet
* Google Calendar
* Google Chat
* Google Docs
* Google Sheets
* Google Slides
* Google Forms
* Gemini

These are primarily **ready-to-use applications**.

```text
You
 |
 v
Google App
 |
 v
Use the application
```

You don't manage:

* servers
* operating systems
* RAM
* CPUs
* database infrastructure
* networking infrastructure

Google manages those things for you.

# 4. Google Cloud Services

Now consider:

* Compute Engine
* Cloud Run
* Google Kubernetes Engine
* Cloud Storage
* Cloud SQL
* Firestore
* BigQuery

Here, the developer is using cloud infrastructure/platform services to **build and operate applications**.

```text
Developer
    |
    v
Google Cloud
    |
    +-- Compute
    +-- Database
    +-- Storage
    +-- Network
    +-- Security
    +-- Kubernetes
    +-- Analytics
```

The session specifically contrasted Google Apps as SaaS-style end-user applications with Google Cloud services used for building, deploying and managing business applications. 

# 5. A Simple Mentor Analogy

Think about a **hotel**.

### Google Apps = Hotel Room

You simply use the room.

```text
Hotel
 |
 +-- Room
 +-- Bed
 +-- Electricity
 +-- Water
 +-- Security
```

You don't construct the hotel.

Similarly:

```text
Gmail
Drive
Meet
Docs
Sheets
```

You simply **use** them.


### Google Cloud = Construction Site

Now imagine you are a construction engineer. You decide:

```text
How many servers?
How much CPU?
How much RAM?
Which database?
Which network?
Which firewall?
How should the application scale?
```

That is much closer to the cloud-engineering mindset. The session used essentially this hotel-room versus construction-site analogy to distinguish Google Apps from Google Cloud. 


# 6. Important GCP Services

| GCP Service              | Purpose                                 |
| ------------------------ | --------------------------------------- |
| Compute Engine           | Virtual machines                        |
| Cloud Run                | Run containerized applications          |
| Google Kubernetes Engine | Kubernetes-based application deployment |
| Cloud Storage            | Object/file storage                     |
| Cloud SQL                | Managed relational database             |
| Firestore                | NoSQL database                          |
| BigQuery                 | Large-scale analytics                   |

The session highlighted Compute Engine, Cloud Run, GKE, Cloud Storage and Cloud SQL as important building blocks for application deployment.


# 7. Application Deployment Example

Let's take our familiar **TFL Insurance application**. Suppose we have:

```text
React
Frontend
   |
   | HTTP/JSON
   v
FastAPI
Backend
   |
   | SQL
   v
Cloud SQL
Database
```

Now place the application inside Google Cloud.

```text
                    INTERNET
                        |
                        v
                 +-------------+
                 | Load Balancer|
                 +-------------+
                        |
                        v
              +-------------------+
              | Compute Engine VM |
              |                   |
              | FastAPI           |
              | Insurance API     |
              +-------------------+
                        |
                        | SQL
                        v
                 +-------------+
                 |  Cloud SQL  |
                 |             |
                 | customers   |
                 | policies    |
                 | products    |
                 | claims      |
                 +-------------+
```

This is the important transition:

```text
Local Development
       |
       v
Cloud Deployment
```

The session used an insurance application example involving FastAPI/ASP.NET, virtual machines, load balancing and Cloud SQL to explain this architecture. 


# 8. Where Does the Developer Fit?

A developer should not think only about:

```text
Python
C#
Java
JavaScript
```

A real application also needs:

```text
Application
     |
     +-- Code
     +-- Database
     +-- Operating System
     +-- Network
     +-- Security
     +-- Deployment
     +-- Monitoring
     +-- Scaling
```

This is where **DevOps knowledge** becomes important.


# 9. Why DevOps?

Earlier, organizations commonly had separate teams.

```text
Developer Team
      |
      | "Application is ready."
      v
Operations Team
      |
      | "It doesn't work in production."
      v
Developer Team
```

This created communication gaps. Then came the idea:

```text
Development + Operations
          |
          v
        DevOps
```

The session described DevOps as an important bridge between development and operations, particularly because production failures can have significant business and customer impact. 


# 10. Modern Software Engineer

Today, particularly in smaller organizations and startups, one engineer may need to understand multiple areas.

```text
             Software Engineer
                    |
       +------------+------------+
       |            |            |
       v            v            v
   Development    Testing    Deployment
                                  |
                                  v
                               Cloud
                                  |
                                  v
                              Operations
```

So don't think:  "I am only a Python developer." 
Think:

> **"I am a software engineer who can build, test and deploy software."**

The session emphasized the growing importance of engineers who can work across development, testing, deployment and cloud responsibilities.

# 11. Cloud Engineer = Linux + Programming + Cloud

One important mentor message from the session:

```text
              Cloud Engineer
                    |
        +-----------+-----------+
        |           |           |
        v           v           v
     Linux     Programming     Cloud
```

Knowing only a cloud portal is not enough. You should understand the operating system underneath. 

For example:

```bash
uname -a
lscpu
free -h
df -h
ip addr
ps aux
```

These commands help you understand the machine running your application. The session specifically emphasized Linux administration as a foundational skill for becoming competent in cloud engineering. 


# 12. Why Learn Linux?

Suppose your FastAPI application is running on a VM. Something goes wrong. You need to investigate:

```text
Is the machine running?
Is there enough RAM?
Is disk full?
Is the network working?
Is the process running?
Which port is listening?

```

That requires operating-system knowledge. Therefore:

```text
Python Developer
       |
       v
Linux
       |
       v
Cloud
       |
       v
DevOps
       |
       v
Production Engineer
```


# 13. GCP Free Tier — Learning Environment

The session began with exploration of the GCP free-tier account and a guided account-creation exercise. The session material described a 90-day trial with free credits and walked through account verification and activation. 

### Mentor lesson

Cloud should not be learned only theoretically. Students should:

```text
Read
 |
 v
Create Account
 |
 v
Create Resource
 |
 v
Run Application
 |
 v
Observe
 |
 v
Destroy Resource
```

That is **learning by doing**.


# 14. Cloud Learning Mindset

Don't simply memorize:

```text
What is Compute Engine?
What is Cloud SQL?
What is Cloud Run?
```

Instead ask:

> **"When would I use it?"**

For example:

### Need a VM?

```text
Compute Engine
```

### Need a managed relational database?

```text
Cloud SQL
```

### Need object/file storage?

```text
Cloud Storage
```

### Need to run a container without managing servers directly?

```text
Cloud Run
```

### Need Kubernetes?

```text
Google Kubernetes Engine
```


# 15. Enterprise vs Startup Mindset

The session also discussed an important career observation. In a large enterprise:

```text
Developer
   |
   | specialized responsibility
   v
Specific Team
```

You may work mainly on one area. In a startup:

```text
             Engineer
                |
     +----------+----------+
     |          |          |
     v          v          v
   Code       Test       Deploy
                           |
                           v
                         Cloud
```

You may need to wear multiple hats. The session encouraged students to prepare for startup environments by strengthening fundamentals, asking questions and experimenting with GCP. 


# 16. From Local Machine to Cloud

This is the journey students should understand.

### Stage 1 — Local development

```text
Laptop
 |
 +-- Python
 +-- FastAPI
 +-- MySQL
```

### Stage 2 — Virtual Machine

```text
GCP
 |
 +-- VM
      |
      +-- Linux
      +-- Python
      +-- FastAPI
```

### Stage 3 — Database as a Service

```text
GCP
 |
 +-- Compute Engine
 |      |
 |      +-- FastAPI
 |
 +-- Cloud SQL
        |
        +-- MySQL
```

### Stage 4 — Production Architecture

```text
                 Internet
                    |
                    v
             Load Balancer
                    |
                    v
            Application Layer
                    |
             +------+------+
             |             |
             v             v
           API 1         API 2
             |             |
             +------+------+
                    |
                    v
                Cloud SQL
```


# 17. The Bigger Picture

Students often learn technology like this:

```text
Python
FastAPI
React
MySQL
Git
```

But industry asks a bigger question:  **Can you build and run a software system?** Therefore the learning pyramid should be:

```text
                  Production
                     /   \
                    /     \
                   /Cloud  \
                  / DevOps  \
                 / Deployment\
                /------------ \
               / Architecture  \
              /-----------------\
             / Programming       \
            /---------------------\
           / Computer Fundamentals \
          /_________________________\
```

# 18. Transflower Mentor Message

> **Don't learn cloud as a collection of services.**  Learn how a software system lives inside the cloud.

Understand:

```text
Application
    |
    v
API
    |
    v
Operating System
    |
    v
Virtual Machine
    |
    v
Network
    |
    v
Cloud Infrastructure
    |
    v
Production
```

And remember:

> **Coding makes the application.
> Architecture organizes it.
> Linux runs it.
> Cloud hosts it.
> DevOps delivers it.**


# 19. Today's Learning Assignment

### Theory

Students should be able to explain:

1. What is Cloud Computing?
2. What is GCP?
3. Google Apps vs Google Cloud
4. What is Compute Engine?
5. What is Cloud SQL?
6. What is Cloud Run?
7. What is GKE?
8. Why is Linux important for cloud engineers?
9. Why do we need DevOps?
10. Enterprise developer vs startup engineer

### Practical

```text
Task 1
Create / activate GCP account
        ↓
Task 2
Explore GCP Console
        ↓
Task 3
Create Virtual Machine
        ↓
Task 4
Connect using SSH
        ↓
Task 5
Explore Linux
        ↓
Task 6
Deploy FastAPI
        ↓
Task 7
Configure HTTP access
        ↓
Task 8
Test API from outside
```

The session concluded by directing students to study the deployment material, VM creation material and related cloud-computing documentation, with an assessment announced for the session. 

## Final Board Note

```text
                 TRANSFLOWER
              LEARNING JOURNEY

        Programming Fundamentals
                    |
                    v
              Application
                    |
                    v
               Web / API
                    |
                    v
                 Linux
                    |
                    v
             Virtual Machine
                    |
                    v
               GCP Cloud
                    |
                    v
                DevOps
                    |
                    v
             Production System
```

### **Mentor Thought**

> **"Don't just learn how to write code. Learn where your code lives, how it communicates, how it is deployed, and how it behaves in production."**

That is the beginning of becoming an **industry-ready software engineer**.