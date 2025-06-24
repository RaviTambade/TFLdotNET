# The Rise of Microservices — From Kingdoms to City-States

*"Let me take you back to the time when we built big monolithic applications — like giant kingdoms."*
We had one big **codebase**, one **deployment**, one **database**. Everything was tightly packed.

And when something went wrong? Well, the whole kingdom collapsed.

> “We had one controller fail… and the entire app was down. Even the login page was affected by a reporting issue!”

That’s when the evolution began.

## 🧩 Microservices Architecture — Divide and Rule (Efficiently)

> "Instead of building an empire, we started building **city-states** — independent, self-sufficient services."

Each **microservice** is like its **own city**:

* Has its **own rules (business logic)**
* Its **own food supply (database)**
* And a **passport system (API)** to talk to others


### 🏗 1. **Building a Microservice with .NET Core**

I tell my mentees:

> "Start with ASP.NET Core Web API. It's lightweight, blazing fast, and perfect for microservices."

In our online shopping system, we built:

* **ProductService**: Manages products
* **OrderService**: Handles orders and transactions
* **InventoryService**: Updates stock
* **UserService**: Takes care of profiles and authentication

Each of them:

* Is an independent `.NET Core` web API
* Has its own `DbContext` with **Entity Framework Core**
* Registers services using **Dependency Injection**


### 🐳 2. **Containerize with Docker – Your Services, Your Ships**

> “Imagine shipping your services to clients without worrying about what OS or configuration they use.”

We used **Docker** to package each microservice:

* It includes the app, dependencies, and configuration
* It can run anywhere: dev machine, cloud, server

```bash
docker build -t product-service .
docker run -p 5000:80 product-service
```

Now our Product Service is live — in a container — **independent** of the host environment.


### ⚙️ 3. **Orchestrate with Kubernetes – The Harbor Master**

Now, imagine you have **20 microservices** running in containers. How do you:

* Keep them alive if one crashes?
* Scale them when traffic spikes?
* Connect them together?

> That’s where **Kubernetes** (or **K8s**) shines.

With Kubernetes, we define:

* **Pods** (the smallest deployable unit)
* **Deployments** (update strategy)
* **Services** (access, discovery, load balancing)

```yaml
apiVersion: apps/v1
kind: Deployment
metadata:
  name: product-deployment
spec:
  replicas: 3
  selector:
    matchLabels:
      app: product-service
  template:
    metadata:
      labels:
        app: product-service
    spec:
      containers:
      - name: product-service
        image: product-service:latest
        ports:
        - containerPort: 80
```

We deployed our services to **Azure Kubernetes Service (AKS)** — and boom 💥 — automatic scaling, self-healing, and load balancing in production!

### 🛰 4. **Service-to-Service Communication**

In the real world, services need to talk.

* For lightweight needs, we used **HTTP/REST** via `HttpClientFactory`.
* For high-performance needs like real-time stock updates, we used **gRPC**.
* For decoupled communication (events), we used **RabbitMQ**.

Example: When an order is placed:

* **OrderService** processes it
* Then **sends a message** to a queue (`OrderPlaced`)
* **InventoryService** picks it up and updates stock

> “Loose coupling = less friction, more flexibility.”


### 🕵️ 5. **Service Discovery + API Gateway**

With dynamic scaling and multiple replicas, how do services find each other?

We use:

* **Consul** or **Kubernetes DNS** for **service discovery**
* **YARP** or **Ocelot** as an **API Gateway** to expose unified endpoints to the outside world

```json
{
  "Routes": [
    {
      "RouteId": "product",
      "ClusterId": "productCluster",
      "Match": { "Path": "/product/{**catch-all}" },
      "Cluster": { "Destinations": { "product": { "Address": "http://product-service" } } }
    }
  ]
}
```

> “Now users access one URL, and the gateway handles the traffic smartly behind the scenes.”

### 📈 6. **Observability – Know What’s Going On**

In a microservices world, it’s not enough to log — we need **metrics, tracing, and dashboards**.

* **Serilog** for structured logging
* **Prometheus + Grafana** for metrics and alerts
* **OpenTelemetry** for tracing across services

> “When a customer says 'something is slow', you don’t want to guess. You want to trace the request end-to-end.”


### 🧠 Final Wisdom from the Mentor:

**Microservices are powerful, but complex.**

Don’t split your app just for the trend. Start by identifying bounded contexts. Ask:

* “Is this business function independent enough?”
* “Do we need to deploy it separately?”
* “Is it managed by a different team?”

> “Build microservices only when you’re solving micro problems. Don’t create micro-chaos.”


