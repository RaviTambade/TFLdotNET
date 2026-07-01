# Insurance Rest API App

## Problem Statement

Modern insurance operations depend on a reliable backend that can support policy sales, renewals, premium collection, customer onboarding, claims handling, and notifications. This app demonstrates a lightweight insurance REST API built using .NET 10, with a clear responsibility split across managers, departments, services, and repositories.

The main problem this app solves is: how to coordinate multiple insurance workflows through event-driven domain managers while keeping business responsibilities separated and exposing them through REST endpoints.

## Solution Overview

This project implements a simplified insurance domain with these capabilities:

- Policy purchase and cancellation
- Policy renewal and renewal reminders
- Premium payment processing
- Customer registration
- Claim registration, approval, rejection, and settlement
- Policy document generation
- Notification triggering via SMS and email services

The app uses a minimal ASP.NET Core API style in `Program.cs` and domain managers to raise events that are handled by department classes and notification services.

## Key Concepts

- `Managers`: encapsulate domain events and workflow triggers (e.g. `SalesManager`, `ClaimsManager`, `PremiumManager`, `RenewalManager`, `CustomerManager`, `PolicyAdminManager`).
- `Departments`: simulate business teams responsible for operating on events (e.g. `SalesDepartment`, `AccountsDepartment`, `ClaimsDepartment`, `RenewalDepartment`, `CustomerServiceDepartment`).
- `Services`: represent external notification channels (`SMSNotificationService`, `EmailNotificationService`).
- `Repositories`: provide read access to stored resources (`PolicyRepository`, `CustomerRepository`, `ClaimsRepository`, `PremiumRepository`).
- `Models`: represent insurance entities such as `Policy`, `Customer`, `Claim`, and `Premium`.

## Architecture Diagram

```txt
                +----------------+
                |  HTTP Clients  |
                | (Postman/curl) |
                +--------+-------+
                         |
                         v
                +----------------+
                |   Program.cs   |
                |  Minimal API   |
                +--------+-------+
                         |
         +---------------+---------------+
         |               |               |
         v               v               v
  +--------------+  +--------------+  +--------------+
  | SalesManager |  | RenewalManager|  | PremiumManager|
  +------+-------+  +-------+------+  +-------+------+
         |                  |                 |
         |                  |                 |
         v                  v                 v
+----------------+   +----------------+   +----------------+
|SalesDepartment |   |RenewalDepartment|   |AccountsDepartment|
+----------------+   +----------------+   +----------------+
         |                  |                 |
         |                  |                 |
         v                  v                 v
  +--------------+  +--------------+   +----------------+
  |   PolicyRepo |  |   PolicyRepo |   | PremiumRepo    |
  +--------------+  +--------------+   +----------------+

         +----------------------------------------------+
         |                 ClaimsManager               |
         +----------------+-----------------------------+
                          |
                          v
                   +--------------+
                   |ClaimsDepartment|
                   +-------+------+ 
                           |
                           v
                      +-----------+
                      | ClaimRepo |
                      +-----------+

                  +-----------------------+
                  | Notification Services |
                  | - SMSNotificationSvc  |
                  | - EmailNotificationSvc|
                  +----------+------------+
                             |
                             v
                       +-------------+
                       | Customers / |
                       |  Policies   |
                       +-------------+
```

## API Endpoints

### Policy Operations

- `POST /api/policies/purchase`
  - Request body: `Policy`
  - Description: Purchase a new policy.

- `POST /api/policies/renew/{policyno}`
  - Description: Renew an existing policy.

- `POST /api/policies/paypremium`
  - Request body: `Premium`
  - Description: Pay a premium for a policy.

- `POST /api/policies/cancel/{policyNumber}`
  - Description: Cancel an existing policy.

- `POST /api/policies/reminder/{policyNumber}`
  - Description: Send a renewal reminder for a policy.

- `POST /api/policies/document/{policyNumber}`
  - Description: Generate and send a policy document.

- `GET /api/policies`
  - Description: Retrieve all policies.

### Customer Operations

- `POST /api/customers/register`
  - Request body: `Customer`
  - Description: Register a new customer.

### Claim Operations

- `POST /api/policies/registerclaim`
  - Request body: `Claim`
  - Description: Register a new claim against a policy.

- `POST /api/claims/approve/`
  - Request body: `Claim`
  - Description: Approve a claim.

- `POST /api/claims/settle`
  - Request body: `Claim`
  - Description: Settle an approved claim.

- `POST /api/claims/reject`
  - Request body: `Claim`
  - Description: Reject a claim.

## Models

### Policy

Properties:
- `PolicyNumber`
- `CustomerName`
- `PolicyType`
- `PolicyAmount`
- `IsRenewed`
- `Status`

### Customer

Properties include identity, contact, address, government ID, employment, nominee info, registration date, and policy activity.

### Claim

Properties include claim ID, policy number, customer details, claim date, type, reason, amount, approved amount, status, remarks, and settlement date.

### Premium

Properties include premium ID, policy reference, customer reference, amount paid, payment date, payment mode, transaction ID, frequency, status, and remarks.

## Running the App

Requirements:
- .NET 10 SDK

Steps:
1. Open the solution directory in a terminal.
2. Run `dotnet run`.
3. The application will start and expose endpoints for HTTP requests.
4. Use a tool like Postman, curl, or the built-in Swagger/OpenAPI UI to invoke the API.

## Notes

- This is a sample application designed to illustrate domain-driven event handling in an insurance workflow.
- The implementation uses in-memory domain behavior and simplified event wiring; it is not production-ready as a complete insurance system.
- Add persistence, validation, authentication, and error handling for a real-world deployment.

## Contact

For enhancements or debugging, inspect `Program.cs` and the domain folders:
- `Managers/`
- `Departments/`
- `Services/`
- `Repositories/`
- `models/`
