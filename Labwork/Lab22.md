


---

# 🎯 Mock Interview Questions

Below is a **Project-Based Mock Interview Question Set** designed **specifically around Clean Architecture HR Management API Capstone**.



## **Clean Architecture HR API – Project Based**

---

## 🧩 SECTION 1: Project Understanding (Warm-Up)

### 1️⃣ Explain your HR Management API in 2 minutes.

**Follow-ups interviewer may ask:**

* What problem does it solve?
* Who are the users?
* Why did you choose Web API instead of MVC?

✅ *Expected focus:* clarity, domain understanding, API purpose

---

### 2️⃣ What are the major layers in your project?

**Follow-ups:**

* Which layer depends on which?
* Can Infrastructure depend on Application?

✅ *Expected answer:* Domain → Application → Infrastructure → API

---

### 3️⃣ Why did you introduce DTOs?

**Follow-ups:**

* Why not return Entity directly?
* What security risk does DTO solve?

✅ *Expected answer:* encapsulation, security, versioning

---

## 🧱 SECTION 2: Clean Architecture (Core Evaluation)

### 4️⃣ What is Clean Architecture and how is it applied in your project?

**Follow-ups:**

* What rule did you strictly follow?
* Which layer has zero dependencies?

🧠 *Look for:* dependency rule, inward dependency

---

### 5️⃣ Why is your Domain project free from EF, Web, and JWT?

**Follow-ups:**

* What happens if you add `[Authorize]` in Domain?

✅ *Expected:* domain purity, testability

---

### 6️⃣ What would break if I removed Infrastructure project?

**Follow-ups:**

* Can Application still compile?
* Why is that powerful?

---

## 🧩 SECTION 3: Repository & Service Pattern

### 7️⃣ Why do you need both Repository and Service layers?

**Follow-ups:**

* Can Controller call Repository directly?
* What business logic exists in Service?

---

### 8️⃣ Where do you apply LINQ in your project?

**Follow-ups:**

* Why not LINQ in Controller?
* Where does mapping happen?

---

### 9️⃣ How would you replace in-memory repository with EF Core?

**Follow-ups:**

* Which project changes?
* Which project remains untouched?

💡 *Key insight:* Infrastructure replacement

---

## 🔄 SECTION 4: Async / Await

### 🔟 Why is async used everywhere?

**Follow-ups:**

* What problem does async solve?
* What happens if you block threads?

---

### 1️⃣1️⃣ What would happen if you forget `await`?

**Follow-ups:**

* What does Task represent?
* How does ASP.NET handle it?

---

### 1️⃣2️⃣ Is async always faster?

**Follow-ups:**

* CPU-bound vs IO-bound?
* Example from your project?

---

## 🔐 SECTION 5: JWT Security (High-Weight)

### 1️⃣3️⃣ Explain JWT flow in your project end-to-end.

**Flow expected:**

```
Login → Token → Header → Middleware → Controller
```

---

### 1️⃣4️⃣ Where is authentication handled?

**Follow-ups:**

* Controller or middleware?
* What happens before controller executes?

---

### 1️⃣5️⃣ Difference between Authentication & Authorization?

**Follow-ups:**

* Which one uses Roles?
* Which one checks identity?

---

### 1️⃣6️⃣ Why is JWT stateless?

**Follow-ups:**

* How does scaling benefit?
* Any drawback?

---

### 1️⃣7️⃣ Where are Claims used?

**Follow-ups:**

* How is role extracted?
* Who validates claims?

---

## 🧠 SECTION 6: SOLID Principles (Mapping)

### 1️⃣8️⃣ Show where SRP is applied in your project.

**Expected mapping:**

* Controller
* Service
* Repository
* Token service

---

### 1️⃣9️⃣ Where is Dependency Inversion used?

**Follow-ups:**

* Which interfaces?
* Why constructor injection?

---

### 2️⃣0️⃣ How does Open-Closed principle help your design?

**Follow-ups:**

* Adding new role
* Adding new DB

---

## 🧪 SECTION 7: Error Handling & Stability

### 2️⃣1️⃣ How would you handle global exceptions?

**Follow-ups:**

* Where will middleware live?
* Why not try-catch everywhere?

---

### 2️⃣2️⃣ What happens if JWT token expires?

**Follow-ups:**

* HTTP status code?
* Frontend responsibility?

---

### 2️⃣3️⃣ How would you add logging?

**Follow-ups:**

* Which layer?
* Why not Domain?

---

## 🧠 SECTION 8: Performance & Scalability

### 2️⃣4️⃣ How does your architecture support scalability?

**Follow-ups:**

* Stateless services
* Load balancer

---

### 2️⃣5️⃣ What changes are needed to convert this into Microservices?

**Follow-ups:**

* Which services?
* Shared vs independent DB?

---

## 🎯 SECTION 9: Design Thinking (Scenario-Based)

### 2️⃣6️⃣ HR wants salary visible only to Managers. How will you implement?

**Expected:**

* Policy-based authorization
* DTO change
* Role check

---

### 2️⃣7️⃣ HR wants audit logs for employee creation.

**Expected:**

* Middleware or service decorator
* Logging infrastructure

---

### 2️⃣8️⃣ HR wants API versioning.

**Expected:**

* DTO versioning
* Controller routing

---

## 🧠 SECTION 10: Final Judgment Questions

### 2️⃣9️⃣ If given 2 weeks, how would you improve this project?

**Look for:**

* Validation
* EF Core
* Refresh tokens
* CI/CD

---

### 3️⃣0️⃣ Why should we hire you as a backend developer?

**Expected:**

* Architecture thinking
* Clean code
* Security awareness
* Async understanding

---

## 🏁 Evaluation Rubric (Quick)

| Area                       | Weight |
| -------------------------- | ------ |
| Architecture Understanding | ⭐⭐⭐⭐   |
| Security (JWT)             | ⭐⭐⭐⭐   |
| Async & Performance        | ⭐⭐⭐    |
| SOLID Principles           | ⭐⭐⭐    |
| Communication              | ⭐⭐⭐⭐   |

---

## 🧠 Mentor Tip for Students

> ❌ Don’t memorize
> ✅ **Explain using your own project**

Interviewers **hire clarity, not code quantity**.

---

### 🔜 Want Next?

I can provide:
📄 **Model Answers (HR-style explanation)**
🎤 **Mock Interview Script (Interviewer ↔ Candidate)**
📊 **Scoring Sheet for Faculty**
🎓 **Placement Readiness Certificate Criteria**

Just say 👍
