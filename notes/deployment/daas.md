# Database as a Service — DBaaS
 
> **Ravi Sir:** "Suppose we are building the TFLInsurance application. Where should our insurance data live?"

Students may answer:

> "Sir, install MySQL on our Linux VM."

Yes. **That is one option.**

But now Ravi Sir asks:

> "Who will maintain that MySQL server?"

And suddenly, the real problem begins.
 

## 1. Traditional Approach — We Manage Everything

Suppose we have a VM:

```text
                    OUR VM
                      |
                      v
              +---------------+
              |     Linux     |
              +---------------+
                      |
                      v
              +---------------+
              |     MySQL     |
              +---------------+
                      |
                      v
              TFLInsuranceDB
```

But installing MySQL is only the beginning. As developers/DBAs, we may have to take care of:

```text
VM
 |
 +-- Linux installation
 |
 +-- MySQL installation
 |
 +-- Configuration
 |
 +-- User management
 |
 +-- Security
 |
 +-- Backup
 |
 +-- Restore
 |
 +-- Monitoring
 |
 +-- Patching
 |
 +-- Replication
 |
 +-- Storage management
 |
 +-- Hardware failure
 |
 +-- High availability
 |
 +-- Disaster recovery
```

### Ravi Sir's question

> **"Are we building an insurance application or are we becoming database infrastructure administrators?"**

That's the important question.
 

# 2. Enter DBaaS

Cloud providers give us another option. Instead of creating and maintaining the database infrastructure ourselves, we can use:

> **Database as a Service — DBaaS**

Conceptually:

```text
                         CLOUD
                           |
                           v
                 +-------------------+
                 |       DBaaS       |
                 |                   |
                 |   Managed MySQL   |
                 +-------------------+
                           |
                           v
                    TFLInsuranceDB
```

We tell the cloud provider:

> "I need a MySQL database."

The provider creates and manages much of the underlying database infrastructure for us.
 

# 3. Traditional Database vs DBaaS

## Traditional Database

```text
              Developer / DBA
                     |
                     v
              +-------------+
              | Buy / Create|
              |   Server    |
              +-------------+
                     |
                     v
              +-------------+
              |    Linux    |
              +-------------+
                     |
                     v
              +-------------+
              |    MySQL    |
              +-------------+
                     |
          +----------+----------+
          |          |          |
        Backup    Patching   Monitoring
          |          |          |
          +----------+----------+
                     |
                     v
               TFLInsuranceDB
```

The responsibility is largely **ours**.

 

# 4. DBaaS Approach

Now look at the cloud model:

```text
                     CLOUD PROVIDER
                           |
             +-------------+-------------+
             |                           |
             v                           v
      Infrastructure             Database Service
             |                           |
       +-----+-----+              +------+------+
       |           |              |             |
     Servers    Storage         MySQL       PostgreSQL
       |           |              |             |
       +-----+-----+              +------+------+
             |                           |
             +-------------+-------------+
                           |
                           v
                    TFLInsuranceDB
```

The cloud provider manages much of the underlying infrastructure. The development team can focus more on:

```text
       APPLICATION
            |
            +
       BUSINESS LOGIC
            |
            +
           DATA
```

rather than:

```text
        HARDWARE
           |
         SERVER
           |
          OS
           |
       DATABASE
           |
       PATCHING
           |
        BACKUP
           |
       HARDWARE
        FAILURE
```
  

# 5. What Does DBaaS Save Us From?

Ravi Sir asks the class:

### Question 1

> **"Do I need to buy a physical database server?"**

**Students:**

> No, not when using a cloud DBaaS offering.

 

### Question 2

> **"Do I need to manually install Linux on the database server?"**

**Students:**

> Usually no.

The provider abstracts much of the underlying infrastructure.
 

### Question 3

> **"Do I need to manually install MySQL?"**

**Students:**

> Usually no.

We select the database engine and configure the service.

```text
Create Database Service
          |
          v
     Select MySQL
          |
          v
     Choose Size
          |
          v
      Configure
          |
          v
      Connect
```

 

### Question 4

> **"What about backups?"**

A managed database service can provide automated backup and recovery capabilities depending on the service configuration and plan.

```text
TFLInsuranceDB
      |
      v
 Automated Backup
      |
      v
   Backup Storage
```

We still need to understand:

* retention
* restore
* recovery
* backup policies
* disaster recovery

**DBaaS does not mean "forget about backups."**

 
### Question 5

> **"What about patching?"**

The cloud provider handles much of the infrastructure and database-service maintenance. But the exact responsibility depends on the provider and service configuration.

 

# 6. The Big Idea

Ravi Sir writes this on the board:

```text
              TRADITIONAL

       WE MANAGE INFRASTRUCTURE
                  |
                  v
       Server + OS + Database
                  |
                  v
              Application
```

Then:

```text
                  DBaaS

          CLOUD PROVIDER
                  |
                  v
       Database Infrastructure
                  |
                  v
             DB SERVICE
                  |
                  v
              Application
```

### Remember this sentence

> **DBaaS moves much of the database infrastructure management from the customer to the cloud provider.**


# 7. But Don't Misunderstand DBaaS

A common student misconception is:

> **"Sir, if I use DBaaS, I don't have to know databases anymore."**

**Ravi Sir:**

> "No! Absolutely not."

DBaaS removes a lot of **infrastructure work**. It does **not** remove the need to understand databases. A developer still needs to understand:

```text
Database Fundamentals
       |
       +-- Tables
       +-- Keys
       +-- Relationships
       +-- SQL
       +-- Indexes
       +-- Transactions
       +-- Constraints
       +-- Normalization
       +-- Query Performance
       +-- Security
```

And an application developer still needs to understand:

```text
ASP.NET Core / Node.js / FastAPI
              |
              v
           Service
              |
              v
         Repository
              |
              v
        DBaaS Database
```
 
# 8. TFLInsurance Example

Suppose we deploy our application on the cloud.

```text
              INTERNET
                  |
                  v
        +-------------------+
        | TFLInsurance API  |
        | ASP.NET / Node /  |
        | FastAPI           |
        +-------------------+
                  |
                  | SQL / DB Driver
                  v
        +-------------------+
        |       DBaaS       |
        |   Managed MySQL   |
        +-------------------+
                  |
                  v
           TFLInsuranceDB
```

The application team concentrates on:

```text
Policy
Premium
Customer
Agent
Claim
Payment
Nominee
```

while the cloud platform takes responsibility for much of the underlying database infrastructure.

 

# 9. DBaaS Is a Responsibility Shift

This is perhaps the most important concept. It is **not**:

```text
DBaaS = No Responsibility
```

It is:

```text
DBaaS
  |
  v
Less Infrastructure Responsibility
  |
  v
More Focus on Application + Data
```

The exact division of responsibility depends on the cloud provider and service. For example:

```text
+--------------------------------------+
|          Cloud Provider              |
+--------------------------------------+
| Infrastructure                       |
| Managed database service             |
| Much of patching/maintenance         |
| Hardware availability                |
| Service-level operations             |
+--------------------------------------+
                  |
                  v
+--------------------------------------+
|          Customer / Developer        |
+--------------------------------------+
| Schema                                |
| Tables                                |
| Queries                               |
| Application code                      |
| Data modeling                         |
| Access control/configuration          |
| Application security                  |
| Performance decisions                 |
| Backup/recovery configuration         |
+--------------------------------------+
```

 

# 10. Mentor's Final Analogy

Imagine you want electricity for your office.

### Option 1 — Build your own power plant

```text
Buy land
   |
Build plant
   |
Buy equipment
   |
Maintain equipment
   |
Manage failures
   |
Generate electricity
```

### Option 2 — Use an electricity service

```text
POWER COMPANY
      |
      v
Electricity
      |
      v
Your Office
```

You don't stop caring about electricity.

You simply stop managing the **power-generation infrastructure**.

DBaaS is similar.

```text
Traditional DB
     |
     v
"I manage the database infrastructure."

DBaaS
     |
     v
"Cloud provider manages much of the
 database infrastructure; I consume
 the database as a service."
```

## One-line takeaway

> **DBaaS = Database without having to own and manage most of the underlying database infrastructure.**

And that leads naturally to our next cloud question:

```text
              CLOUD SERVICES

        +-----------------------+
        |       SaaS            |
        |  Complete Software    |
        +-----------------------+
                  |
        +-----------------------+
        |       PaaS            |
        | Application Platform  |
        +-----------------------+
                  |
        +-----------------------+
        |       DBaaS           |
        | Managed Database      |
        +-----------------------+
                  |
        +-----------------------+
        |       IaaS            |
        | VM / Network / Disk   |
        +-----------------------+
```

> **The more managed the service, the less infrastructure we manage—and the more we can concentrate on delivering business value.**
