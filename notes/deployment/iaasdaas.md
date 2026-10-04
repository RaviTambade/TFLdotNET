# 15. IaaS + DBaaS Together

Now Ravi Sir asks the class:

> **"We know how to put our application on a cloud VM. We also know that we can use a managed database. Can we use both together?"**

**Yes!**

This is a very common cloud architecture.

## 15.1 Think Like a Developer

We have two separate requirements:

```text
Requirement 1
-------------
"I need a computer to run my application."

                |
                v
              IaaS
                |
                v
          Virtual Machine


Requirement 2
-------------
"I need a database to store my application data."

                |
                v
             DBaaS
                |
                v
          Managed MySQL
```

So we combine them.

```text
                         PUBLIC CLOUD
                              |
                +-------------+-------------+
                |                           |
               IaaS                       DBaaS
                |                           |
                v                           v
        +---------------+           +---------------+
        | Compute Engine|           | Managed MySQL |
        |      VM       |           |   Database    |
        +---------------+           +---------------+
                |                           |
                v                           v
          Debian Linux              TFLInsuranceDB
                |
                v
             .NET 10
                |
                v
          ASP.NET Core
                |
                +---------------------------+
                                            |
                                            | SQL / DB Connection
                                            v
                                      Managed MySQL
```


# 15.2 Important Point

Look carefully. We have:

```text
Compute Engine VM
       |
       +-- Debian Linux
       |
       +-- .NET 10
       |
       +-- ASP.NET Core
       |
       +-- TFLInsurance API
```

But we **don't necessarily have**:

```text
       +-- MySQL
```

inside the VM.

Instead:

```text
ASP.NET Core
      |
      | Database Connection
      v
Managed MySQL
      |
      v
TFLInsuranceDB
```

This is the power of combining **IaaS + DBaaS**.


# 15.3 Traditional Architecture

Earlier, we might have done everything inside one VM:

```text
                 VM
                  |
       +----------+----------+
       |          |          |
     Debian     .NET       MySQL
       |          |          |
       +----------+----------+
                  |
           TFLInsuranceDB
```

It works. For learning, labs and small applications, this can be perfectly reasonable. But now imagine production. What happens if:

```text
VM crashes?
      |
      v
Application + Database
      |
      v
Both affected
```

Now we have another architecture.



# 15.4 IaaS + DBaaS Architecture

```text
                    PUBLIC CLOUD
                         |
             +-----------+-----------+
             |                       |
            IaaS                    DBaaS
             |                       |
             v                       v
       +-----------+           +-----------+
       | Compute   |           | Managed   |
       | Engine VM |           | MySQL     |
       +-----------+           +-----------+
             |                       |
             v                       v
          Debian              TFLInsuranceDB
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
             |
             +----------------------+
                                    |
                              Database Connection
                                    |
                                    v
                              Managed MySQL
```

Now the responsibilities are separated.

```text
Application Infrastructure       Database Infrastructure
        |                                  |
        v                                  v
      IaaS                                DBaaS
        |                                  |
        v                                  v
  Compute Engine                    Managed MySQL
        |                                  |
        v                                  v
 ASP.NET Core API                  TFLInsuranceDB
```


# 16. Complete Transflower Architecture

Now Ravi Sir says:

> **"Let's connect everything we have learned so far."**

We have:

* Internet
* GCP Cloud
* IaaS
* Compute Engine
* Virtual Machine
* Debian Linux
* .NET
* ASP.NET Core
* TFLInsurance API
* DBaaS
* Managed MySQL
* TFLInsuranceDB
* Browser
* Postman

Put everything together.

```text
                           INTERNET
                              |
                              v
                  +-----------------------+
                  |       GCP CLOUD       |
                  +-----------------------+
                       |             |
                       |             |
                      IaaS          DBaaS
                       |             |
                       v             v
                +-----------+   +-----------+
                | Compute   |   | Managed   |
                | Engine VM |   | MySQL     |
                +-----------+   +-----------+
                       |             |
                       v             v
                    Debian      TFLInsuranceDB
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
              v                 v
           Browser           Postman
```

But there is one important connection missing from the picture. The API needs to communicate with the database.

So let's make it explicit:

```text
                         INTERNET
                            |
                            v
                 +-------------------+
                 |     GCP CLOUD     |
                 +-------------------+
                     |           |
                    IaaS        DBaaS
                     |           |
                     v           v
              +-----------+  +-----------+
              | Compute   |  | Managed   |
              | Engine VM |  | MySQL     |
              +-----------+  +-----------+
                     |           |
                     v           v
                  Debian    TFLInsuranceDB
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
          +----------+----------+
          |                     |
          v                     v
      Browser                Postman
          
                     |
                     | SQL / DB Connection
                     |
                     +-------------------->
                              Managed MySQL
```

A cleaner application-flow view is:

```text
Browser / Postman
       |
       | HTTP / HTTPS
       v
TFLInsurance API
       |
       | SQL / Database Driver
       v
Managed MySQL
       |
       v
TFLInsuranceDB
```
 

# 16.1 What Is Running Where?

This is a very important cloud concept.

### On the Compute Engine VM

```text
Compute Engine VM
       |
       +-- Debian Linux
       |
       +-- .NET 10
       |
       +-- ASP.NET Core
       |
       +-- TFLInsurance API
```

### On the Managed Database Service

```text
DBaaS
 |
 +-- MySQL
 |
 +-- Storage
 |
 +-- Backup capabilities
 |
 +-- Database infrastructure
 |
 +-- Managed operations
 |
 +-- TFLInsuranceDB
```

So:

> **Application and database don't have to live on the same VM.**

 

# 16.2 The Developer's View

Suppose we have this API:

```text
GET /api/policies
POST /api/policies
PUT /api/policies/101
DELETE /api/policies/101
```

The request flow becomes:

```text
             Client
          Browser/Postman
                |
                | HTTP
                v
       +------------------+
       | TFLInsurance API |
       +------------------+
                |
                | SQL
                v
       +------------------+
       |   Managed MySQL  |
       +------------------+
                |
                v
          TFLInsuranceDB
                |
                v
             policies
```

The developer doesn't need to think:  "Where is the physical hard disk?" Instead, the developer thinks:  "What data does my application need and how should I access it?" That is a major shift toward **cloud thinking**.



# 16.3 Why This Architecture Is Useful

### Separation of concerns

```text
Application
     |
     v
Compute / IaaS

Database
     |
     v
DBaaS
```

### Independent management

The application VM and database service can be managed separately.

```text
Application
    |
    +-- Scale / restart / deploy
    |
    v
   IaaS


Database
    |
    +-- Backup / storage / HA configuration
    |
    v
  DBaaS
```

### Better operational model

Instead of:

```text
Developer
   |
   +-- Linux
   +-- MySQL
   +-- Backup
   +-- Patching
   +-- Hardware
   +-- Application
   +-- Business Logic
```

we move toward:

```text
Cloud Provider
   |
   +-- Infrastructure
   +-- Managed Database Infrastructure


Developer
   |
   +-- Application
   +-- API
   +-- Business Logic
   +-- Data Model
```

---

# 16.4 Ravi Sir's Classroom Question

> **"If my ASP.NET Core application is running inside a GCP VM, does my MySQL database also have to run inside that same VM?"**

### Answer:

**No.**

The application can run on:

```text
GCP Compute Engine
```

while the database can run on:

```text
GCP Managed Database Service
```

and they communicate over the cloud network.

```text
       IaaS                         DBaaS

+----------------+             +----------------+
| Compute Engine |             | Managed MySQL  |
|                |             |                |
| Debian         |             | TFLInsuranceDB |
| .NET 10        |             |                |
| ASP.NET Core   |------------>|                |
+----------------+    Network  +----------------+
```

# 16.5 The Transflower Mental Model

Remember this simple picture:

```text
                CLOUD
                  |
       +----------+----------+
       |                     |
      IaaS                  DBaaS
       |                     |
       v                     v
   "Run my app"        "Store my data"
       |                     |
       v                     v
   Compute VM            Managed DB
       |                     |
       v                     v
 ASP.NET Core          MySQL
       |                     |
       +----------+----------+
                  |
                  v
          TFLInsurance
```

### Final mentor message

> **IaaS gives us a computer to run our application.**

> **DBaaS gives us a managed database to store our data.**

> **Together, they allow us to build a cloud application without putting the entire infrastructure-management burden on the development team.**

And the real developer mindset is:

```text
Don't ask only:

"Where can I install my software?"

Start asking:

"What should I manage myself,
and what should I consume as a cloud service?"
```

That is the beginning of **cloud architecture thinking**.