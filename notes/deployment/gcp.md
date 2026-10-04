# What is GCP?

**GCP = Google Cloud Platform.**

GCP is Google's **public cloud computing platform**. It provides computing, storage, databases, networking, security, AI, analytics, and many other services over the internet.

Think of it this way:

```text
Traditional IT Infrastructure
        |
        +-- Buy Physical Server
        +-- Buy Storage
        +-- Setup Network
        +-- Install OS
        +-- Install Database
        +-- Maintain Hardware
        |
        v
     Your Data Center
```

With GCP:

```text
                    GOOGLE CLOUD
                         |
       +-----------------+-----------------+
       |                 |                 |
    Compute           Storage           Database
       |                 |                 |
   VM / Server        Cloud Storage      Cloud SQL
       |
   Compute Engine
       |
     Linux
       |
     .NET
       |
 ASP.NET Core App
```

### Simple definition

> **GCP is Google's collection of cloud services that allows organizations and developers to use computing infrastructure and software services on demand, without owning the underlying physical data-center infrastructure.**

## Important GCP Services

| Requirement          | GCP Service                        | Purpose                                           |
| -------------------- | ---------------------------------- | ------------------------------------------------- |
| Virtual Machine      | **Compute Engine**                 | Run Windows/Linux VMs                             |
| Application platform | **App Engine**                     | Deploy applications without managing VMs directly |
| Containers           | **Cloud Run**                      | Run containerized applications                    |
| Kubernetes           | **Google Kubernetes Engine (GKE)** | Manage containerized applications                 |
| Object storage       | **Cloud Storage**                  | Store files, images, videos, backups              |
| Relational database  | **Cloud SQL**                      | Managed MySQL/PostgreSQL/SQL Server               |
| NoSQL database       | **Firestore**                      | Document-oriented database                        |
| Data warehouse       | **BigQuery**                       | Large-scale analytics                             |
| Networking           | **VPC**                            | Private cloud networking                          |
| Identity             | **IAM**                            | Access and permissions                            |
| Monitoring           | **Cloud Monitoring**               | Monitor applications and infrastructure           |

## GCP in Our Transflower Example

We recently created this environment:

```text
                       GCP
                        |
                 Compute Engine
                        |
                  Virtual Machine
                        |
                  Debian Linux 13
                        |
                     .NET 10
                        |
                  ASP.NET Core
                        |
                  TFLInsurance API
                        |
                   Cloud Database
```

So when we say:

> "Our ASP.NET Core application is running on GCP"

we really mean that the application is running on **GCP infrastructure**, in our case a **Compute Engine virtual machine**.

## One Important Concept

GCP itself is not just a "server." It is a **cloud platform containing many services**.

```text
                    GCP
                     |
     +---------------+---------------+
     |               |               |
   Compute         Storage         Database
     |               |               |
 Compute Engine   Cloud Storage    Cloud SQL
     |
     v
    VM
     |
     v
   Linux
     |
     v
  .NET 10
     |
     v
ASP.NET Core
```

### Mentor's One-Line Explanation

> **"GCP is Google's data-center infrastructure exposed to developers as cloud services."**

And from a developer's perspective:

> **We don't need to own the data center; we consume the infrastructure and services provided by Google Cloud.**

The exact environment:

```text
GCP Compute Engine
        |
        v
Debian GNU/Linux 13 (trixie)
        |
        v
Debian 13.7
```

## 1. Update your GCP VM

SSH into the VM and run:

```bash
sudo apt update
sudo apt upgrade -y
```

Check architecture:

```bash
uname -m
```

Normally a GCP VM will return:

```text
x86_64
```

## 2. Install prerequisites

```bash
sudo apt install -y wget ca-certificates
```

## 3. Add Microsoft's Debian 13 repository

Download Microsoft's repository package:

```bash
wget https://packages.microsoft.com/config/debian/13/packages-microsoft-prod.deb
```

Install it:

```bash
sudo dpkg -i packages-microsoft-prod.deb
```

Remove the downloaded file:

```bash
rm packages-microsoft-prod.deb
```

Now update the package list:

```bash
sudo apt update
```

## 4. Check whether .NET 10 is available

Before installing, verify:

```bash
apt-cache search dotnet-sdk-10
```

You should see something similar to:

```text
dotnet-sdk-10.0
```

You can also check:

```bash
apt-cache policy dotnet-sdk-10.0
```

## 5. Install .NET 10 SDK

```bash
sudo apt install -y dotnet-sdk-10.0
```

This installs the **SDK**, which includes the runtime and the `dotnet` CLI.

## 6. Verify

Run:

```bash
dotnet --version
```

Then:

```bash
dotnet --info
```

And:

```bash
dotnet --list-sdks
```

You should see a 10.0 SDK, for example:

```text
10.0.xxx [/usr/share/dotnet/sdk]
```

Also check runtimes:

```bash
dotnet --list-runtimes
```

# 7. Complete installation script

For your **GCP + Debian 13** VM, you can save this as:

```bash
nano install-dotnet10.sh
```

Paste:

```bash
#!/bin/bash

set -e

echo "======================================"
echo " GCP Debian 13 - .NET 10 Installation"
echo "======================================"

echo
echo "Operating System:"
cat /etc/os-release

echo
echo "Architecture:"
uname -m

echo
echo "1. Updating Debian..."
sudo apt update
sudo apt upgrade -y

echo
echo "2. Installing prerequisites..."
sudo apt install -y wget ca-certificates

echo
echo "3. Downloading Microsoft package repository..."
wget -q https://packages.microsoft.com/config/debian/13/packages-microsoft-prod.deb

echo
echo "4. Installing Microsoft repository..."
sudo dpkg -i packages-microsoft-prod.deb

echo
echo "5. Removing repository installer..."
rm packages-microsoft-prod.deb

echo
echo "6. Updating package lists..."
sudo apt update

echo
echo "7. Installing .NET 10 SDK..."
sudo apt install -y dotnet-sdk-10.0

echo
echo "======================================"
echo " .NET VERSION"
echo "======================================"

dotnet --version

echo
echo "======================================"
echo " INSTALLED SDKs"
echo "======================================"

dotnet --list-sdks

echo
echo "======================================"
echo " INSTALLED RUNTIMES"
echo "======================================"

dotnet --list-runtimes

echo
echo "======================================"
echo " .NET INFORMATION"
echo "======================================"

dotnet --info

echo
echo "======================================"
echo " .NET 10 installation completed!"
echo "======================================"
```

Make it executable:

```bash
chmod +x install-dotnet10.sh
```

Run:

```bash
./install-dotnet10.sh
```

# 8. Create your first .NET application

After installation:

```bash
mkdir -p ~/projects
cd ~/projects
```

Create a console application:

```bash
dotnet new console -n HelloDotNet
```

```bash
cd HelloDotNet
```

Run it:

```bash
dotnet run
```

Expected:

```text
Hello, World!
```

# 9. Create ASP.NET Core Web API

Go back:

```bash
cd ~/projects
```

Create the API:

```bash
dotnet new webapi -n TFLInsuranceAPI
```

```bash
cd TFLInsuranceAPI
```

Run:

```bash
dotnet run --urls "http://0.0.0.0:5000"
```

You should get something similar to:

```text
Now listening on: http://0.0.0.0:5000
```

# 10. Test inside the VM

Open another SSH terminal and run:

```bash
curl http://localhost:5000
```

For the generated Web API, you can test its available endpoint, commonly:

```bash
curl http://localhost:5000/weatherforecast
```


# 11. Access it from your computer

Now we move from **Linux/.NET** to **GCP networking**.

```text
Your Laptop
     |
     | HTTP
     v
Public IP of GCP VM
     |
     | TCP :5000
     v
GCP Firewall
     |
     v
Debian 13 VM
     |
     v
ASP.NET Core
     |
     v
.NET 10
```

You need a GCP firewall rule allowing TCP 5000.

From **Google Cloud Shell**:

```bash
gcloud compute firewall-rules create allow-dotnet-5000 \
    --allow tcp:5000 \
    --source-ranges 0.0.0.0/0
```

For a temporary classroom/demo VM this is convenient. For production, restrict the source range or put the API behind a proper HTTPS load balancer/reverse proxy.


# 12. Find your GCP VM public IP

Inside the VM:

```bash
curl ifconfig.me
```

Or in GCP Console:

```text
Compute Engine
     |
     +-- VM instances
             |
             +-- External IP
```

Suppose the external IP is:

```text
34.xxx.xxx.xxx
```

Then from your laptop:

```text
http://34.xxx.xxx.xxx:5000
```

And with Postman:

```text
GET http://34.xxx.xxx.xxx:5000/weatherforecast
```

## Your complete stack

This is actually a very good **IaaS classroom lab**:

```text
                    GCP
                     |
             Compute Engine
                     |
              Virtual Machine
                     |
              Debian Linux 13
                     |
                .NET SDK 10
                     |
              ASP.NET Core
                     |
                 Web API
                     |
                TCP :5000
                     |
          +----------+----------+
          |                     |
       Browser               Postman
```

### The 10 commands students should remember

```bash
sudo apt update

sudo apt upgrade -y

sudo apt install -y wget ca-certificates

wget https://packages.microsoft.com/config/debian/13/packages-microsoft-prod.deb

sudo dpkg -i packages-microsoft-prod.deb

rm packages-microsoft-prod.deb

sudo apt update

sudo apt install -y dotnet-sdk-10.0

dotnet --version

dotnet --info
```

Then:

```bash
dotnet new webapi -n TFLInsuranceAPI
cd TFLInsuranceAPI
dotnet run --urls "http://0.0.0.0:5000"
```

**One distinction to teach students:** installing .NET is a **Linux/OS task**; making the API reachable from the internet is a **GCP networking/firewall task**. Both are required when deploying an ASP.NET Core application on an IaaS VM.