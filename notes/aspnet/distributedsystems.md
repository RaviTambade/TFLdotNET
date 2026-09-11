
# **Distributed Systems**

Most developers learn distributed systems by memorizing technologies:

REST.
RabbitMQ.
Kafka.
Azure Service Bus.
Batch jobs.
Streaming.

But the real question is not: **“Which technology should I use?”** The better question is: **“How should these two systems communicate?”** That decision changes the architecture. Here are 5 communication patterns every software engineer should understand 👇

### 1️⃣ Request–Response → “I need an answer now.”

One system asks another system for something and waits for the response.

```text
Client
   ↓ Request
Order Service
   ↓ Response
Client
```

Examples:

• REST API
• gRPC
• Database queries

Use it when the caller **needs an immediate answer**. But remember: **You are also accepting the dependency on the other service's latency and availability.**

 

### 2️⃣ Message Queue → “Do this later.”

The producer puts work into a queue. The consumer processes it when it is ready.

```text
Producer
   ↓
  Queue
   ↓
Consumer
```

The producer doesn't have to wait for the consumer to finish. Great for:

• Background processing
• Email sending
• Invoice generation
• Order processing
• Traffic bursts

Think: **“I don't need the result right now. I need the work to eventually happen.”**
 

### 3️⃣ Publish–Subscribe → “Something happened. Anyone interested?”

Here the producer publishes an event. Multiple consumers can independently react.

```text
             ┌──→ Email Service
             │
Order ─→ Topic ─→ Analytics
             │
             └──→ Notification Service
```

For example:

**OrderPlaced**

One event can trigger:

→ Payment processing
→ Email notification
→ Inventory update
→ Analytics
→ Loyalty points

The publisher doesn't need to know who consumes the event. That's **decoupling**.

 

### 4️⃣ Streaming → “Keep processing the flow.”

Streaming is about **continuous data**.

```text
Events →→→→→→→→→→→
          ↓
      Consumers
          ↓
   Real-time processing
```

Think about:

• Stock prices
• IoT sensors
• Application logs
• Click streams
• Real-time analytics

A major difference from a simple queue is that streaming platforms commonly retain events, allowing consumers to process them independently and potentially replay historical data.

Think: **“The data never really stops coming.”**
 

### 5️⃣ Batch Processing → “Process this collection together.”

Instead of processing every event immediately, collect data and process it as a group.

```text
Data
 ↓
████████████
Batch
 ↓
Processing
 ↓
Report
```

Examples:

• Nightly ETL
• Payroll processing
• Monthly reports
• Data migration
• Backfills
• Large-scale recomputation

Think: **“Real-time isn't necessary. Efficiency matters more.”**
 
### The mental model I teach at Transflower

Don't memorize five definitions. Remember five questions:

**Request–Response** :👉 Do I need an answer now?
**Message Queue** :👉 Can this work happen asynchronously?
**Pub-Sub** : 👉 Does this event need to notify multiple consumers?
**Streaming** : 👉 Do I need to continuously process a flow of events?
**Batch** : 👉 Can I process this data together later?

That's distributed-systems thinking.

### And here's where architecture becomes interesting...

Real systems rarely choose only one pattern. An e-commerce application might look like:

```text
                    ┌──→ Payment
                    │
Customer
   │                ├──→ Inventory
   ↓                │
Order API ──→ Event ─┼──→ Notification
   │                │
   │                └──→ Analytics
   ↓
Response

Analytics
    ↓
Streaming
    ↓
Data Platform
    ↓
Nightly Batch
    ↓
Business Reports
```

One system.

Multiple communication patterns.

Because **different problems require different forms of communication.**

That is the shift from:

**“I know APIs.”**

to

**“I know how distributed systems communicate.”**

And that is an important step in becoming a software architect.

**Don't start with Kafka, RabbitMQ, REST, or any tool.**

Start with the communication problem.

**Architecture first.
Technology second.**

— Transflower Mentor 🌱

#DistributedSystems #SoftwareArchitecture #SystemDesign #Microservices #EventDrivenArchitecture #SoftwareEngineering #Transflower #Mentoring

If you want, I can also turn this into a **Transflower Mentor “Distributed Systems — 10 communication patterns” series** covering REST, gRPC, queues, pub-sub, Kafka, event sourcing, CQRS, WebSockets, SSE, and batch processing.
