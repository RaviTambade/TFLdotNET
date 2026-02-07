
## **Lab Assignment: Real-Time Healthcare Communication using WebSockets**

## 1. Lab Overview

**Lab Type:** Mentor-guided, hands-on real-time systems lab
**TLF Stage:** Real-Time Communication & Event-Driven Systems
**Duration:** 2–3 Hours
**Difficulty Level:** Intermediate
**Primary Skill Focus:** Bidirectional communication, stateful connections, real-time updates

## 2. Business Context (Healthcare Domain)

> In a hospital environment, many systems require **real-time updates**:
>
> * Patient vital monitoring
> * Doctor availability status
> * Emergency alerts
> * Live appointment queue

REST APIs are **request–response based** and inefficient for such scenarios.

👉 **WebSockets** enable *persistent, real-time, bidirectional communication*.

## 3. Problem Statement (Given to Students)

A hospital wants to build a **Live Patient Monitoring Dashboard**.

* The **Hospital Server** continuously sends patient status updates.
* Multiple **Client Dashboards** receive updates in real time.
* Communication must be **low-latency** and **continuous**.

🎯 **Your task:**
Implement a **WebSocket-based system** to broadcast patient status updates.


## 4. Learning Objectives (TLF Aligned)

By completing this lab, students will be able to:

* Explain **why WebSockets are required**
* Differentiate **REST vs WebSockets**
* Implement a **WebSocket server**
* Build a **WebSocket client**
* Understand **stateful connections**
* Design **real-time healthcare systems**

## 5. Architecture View

```
+---------------------+      WebSocket       +----------------------+
|  Client Dashboard   | <----------------->  | Hospital WS Server   |
| (Doctor / Nurse UI) |                      | (Vital Broadcaster)  |
+---------------------+                      +----------------------+
```
  
**Mentor Note:**

> REST asks questions. WebSockets **stay connected**.



## 6. Technology Stack

| Layer       | Technology                       |
| ----------- | -------------------------------- |
| Server      | Node.js / ASP.NET Core WebSocket |
| Client      | Browser / Node.js                |
| Protocol    | WebSocket                        |
| Data Format | JSON                             |


## 7. Use Case Definition

### Patient Vital Update Event

```json
{
  "patientId": "P102",
  "heartRate": 82,
  "bp": "120/80",
  "status": "Stable"
}
```

## 8. Lab Tasks (Step-wise Assignment)

### 🔹 Task 1: Understand the Communication Model

Answer before coding:

* Why polling is inefficient?
* Why REST fails for real-time systems?
* What does “persistent connection” mean?


### 🔹 Task 2: Create WebSocket Server

**Responsibilities:**

* Accept WebSocket connections
* Maintain connected clients
* Broadcast patient updates

**Hint (Conceptual):**

```text
onClientConnect → store connection
onVitalUpdate → broadcast to all clients
```


### 🔹 Task 3: Simulate Patient Data

* Generate patient vitals every 3 seconds
* Broadcast updates to all connected clients

**TLF Thinking:**

> Events happen whether clients ask or not.


### 🔹 Task 4: Create WebSocket Client

**Responsibilities:**

* Connect to server
* Listen for updates
* Display patient status

**Expected Output (Console/UI):**

```
Patient P102 | HR: 82 | BP: 120/80 | Status: Stable
```


### 🔹 Task 5: Handle Multiple Clients

* Open multiple clients
* Verify all receive updates simultaneously

**Mentor Insight:**

> This simulates doctors and nurses monitoring the same patient.


## 9. Comparison Section (Mandatory)

| Feature       | REST             | WebSockets    |
| ------------- | ---------------- | ------------- |
| Communication | Request–Response | Bidirectional |
| Connection    | Stateless        | Stateful      |
| Real-time     | ❌                | ✅             |
| Use case      | CRUD             | Live updates  |


## 10. Reflection Questions (Written Submission)

Students must answer:

1. When should WebSockets NOT be used?
2. What happens if a client disconnects?
3. How does this help emergency systems?
4. Can WebSockets replace REST completely?


## 11. Assignments & Extensions

### Level 1 (Easy)

* Add patient temperature

### Level 2 (Medium)

* Send alert when heart rate > 120

### Level 3 (Advanced)

* Add authentication
* Tag messages with doctor ID


## 12. Assessment Criteria (TLF Style)

| Area     | What is Evaluated         |
| -------- | ------------------------- |
| Design   | Correct use of WebSockets |
| Server   | Broadcasting logic        |
| Client   | Real-time handling        |
| Concepts | Explanation ability       |
| Thinking | Use-case justification    |


## 13. Industry Mapping

| Industry System | Lab Mapping            |
| --------------- | ---------------------- |
| ICU Monitoring  | WebSockets             |
| Stock Tickers   | Real-time feeds        |
| Chat Apps       | Persistent connections |


## 14. Mentor Closing Note

> REST builds services.
> WebSockets build **situational awareness**.


<hr/>

## **Lab Assignment: Real-Time Healthcare Communication using WebSockets**


## 1. Lab Overview

**Lab Type:** Mentor-guided, hands-on real-time systems lab
**TLF Stage:** Real-Time Communication & Event-Driven Systems
**Duration:** 2–3 Hours
**Difficulty Level:** Intermediate
**Primary Skill Focus:** Bidirectional communication, stateful connections, real-time updates


## 2. Business Context (Healthcare Domain)

> In a hospital environment, many systems require **real-time updates**:
>
> * Patient vital monitoring
> * Doctor availability status
> * Emergency alerts
> * Live appointment queue

REST APIs are **request–response based** and inefficient for such scenarios.

👉 **WebSockets** enable *persistent, real-time, bidirectional communication*.


## 3. Problem Statement (Given to Students)

A hospital wants to build a **Live Patient Monitoring Dashboard**.

* The **Hospital Server** continuously sends patient status updates.
* Multiple **Client Dashboards** receive updates in real time.
* Communication must be **low-latency** and **continuous**.

🎯 **Your task:**
Implement a **WebSocket-based system** to broadcast patient status updates.

---

## 4. Learning Objectives (TLF Aligned)

By completing this lab, students will be able to:

* Explain **why WebSockets are required**
* Differentiate **REST vs WebSockets**
* Implement a **WebSocket server**
* Build a **WebSocket client**
* Understand **stateful connections**
* Design **real-time healthcare systems**


## 5. Architecture View

```
+---------------------+      WebSocket      +-----------------------+
|  Client Dashboard   | <-----------------> | Hospital WS Server    |
| (Doctor / Nurse UI) |                     | (Vital Broadcaster)   |
+---------------------+                     +-----------------------+
```

**Mentor Note:**

> REST asks questions. WebSockets **stay connected**.


## 6. Technology Stack

| Layer       | Technology                       |
| ----------- | -------------------------------- |
| Server      | Node.js / ASP.NET Core WebSocket |
| Client      | Browser / Node.js                |
| Protocol    | WebSocket                        |
| Data Format | JSON                             |


## 7. Use Case Definition

### Patient Vital Update Event

```json
{
  "patientId": "P102",
  "heartRate": 82,
  "bp": "120/80",
  "status": "Stable"
}
```


## 8. Lab Tasks (Step-wise Assignment)

### 🔹 Task 1: Understand the Communication Model

Answer before coding:

* Why polling is inefficient?
* Why REST fails for real-time systems?
* What does “persistent connection” mean?


### 🔹 Task 2: Create WebSocket Server

**Responsibilities:**

* Accept WebSocket connections
* Maintain connected clients
* Broadcast patient updates

**Hint (Conceptual):**

```text
onClientConnect → store connection
onVitalUpdate → broadcast to all clients
```


### 🔹 Task 3: Simulate Patient Data

* Generate patient vitals every 3 seconds
* Broadcast updates to all connected clients

**TLF Thinking:**

> Events happen whether clients ask or not.


### 🔹 Task 4: Create WebSocket Client

**Responsibilities:**

* Connect to server
* Listen for updates
* Display patient status

**Expected Output (Console/UI):**

```
Patient P102 | HR: 82 | BP: 120/80 | Status: Stable
```

### 🔹 Task 5: Handle Multiple Clients

* Open multiple clients
* Verify all receive updates simultaneously

**Mentor Insight:**

> This simulates doctors and nurses monitoring the same patient.



## 9. Comparison Section (Mandatory)

| Feature       | REST             | WebSockets    |
| ------------- | ---------------- | ------------- |
| Communication | Request–Response | Bidirectional |
| Connection    | Stateless        | Stateful      |
| Real-time     | ❌                ✅            |
| Use case      | CRUD             | Live updates  |


## 10. Reflection Questions (Written Submission)

Students must answer:

1. When should WebSockets NOT be used?
2. What happens if a client disconnects?
3. How does this help emergency systems?
4. Can WebSockets replace REST completely?


## 11. Assignments & Extensions

### Level 1 (Easy)

* Add patient temperature

### Level 2 (Medium)

* Send alert when heart rate > 120

### Level 3 (Advanced)

* Add authentication
* Tag messages with doctor ID


## 12. Assessment Criteria (TLF Style)

| Area     | What is Evaluated         |
| -------- | ------------------------- |
| Design   | Correct use of WebSockets |
| Server   | Broadcasting logic        |
| Client   | Real-time handling        |
| Concepts | Explanation ability       |
| Thinking | Use-case justification    |


## 13. Industry Mapping

| Industry System | Lab Mapping            |
| --------------- | ---------------------- |
| ICU Monitoring  | WebSockets             |
| Stock Tickers   | Real-time feeds        |
| Chat Apps       | Persistent connections |


## 14. Mentor Closing Note

> REST builds services.
> WebSockets build **situational awareness**.

