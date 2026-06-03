## Insurance Policy Management System using Events

### Background

An insurance company sells various policies to customers. Whenever an important action occurs, multiple departments need to be informed.

For example:

* When a new policy is purchased, the Sales Team should be notified.
* When a premium is paid, the Accounts Team should be notified.
* When a claim is registered, the Claim Department should be notified.
* When a policy expires, the Renewal Team should be notified.

Instead of tightly coupling these operations, use **Events and Event Handlers** to notify interested departments.

---

# Functional Requirements

Create a Console Application that displays the following menu:

```text
====================================
 Insurance Management System
====================================

1. Purchase Policy
2. Pay Premium
3. Register Claim
4. Renew Policy
5. Exit

Enter Choice:
```

---

# Event Requirements

## Event 1: Policy Purchased

When a customer purchases a policy:

```csharp
policyManager.PurchasePolicy();
```

Raise:

```csharp
PolicyPurchased
```

Subscribers:

```text
Sales Department
Customer Notification Service
```

Expected Output:

```text
Policy Purchased Successfully

[Sales Team]
New policy sold.

[Notification Service]
Welcome email sent to customer.
```

---

## Event 2: Premium Paid

When a customer pays premium:

```csharp
policyManager.PayPremium();
```

Raise:

```csharp
PremiumPaid
```

Subscribers:

```text
Accounts Department
SMS Notification Service
```

Expected Output:

```text
Premium Received

[Accounts Department]
Payment recorded successfully.

[SMS Service]
Premium payment confirmation sent.
```

---

## Event 3: Claim Registered

When a customer registers claim:

```csharp
policyManager.RegisterClaim();
```

Raise:

```csharp
ClaimRegistered
```

Subscribers:

```text
Claim Department
Surveyor Team
```

Expected Output:

```text
Claim Registered

[Claim Department]
Claim case created.

[Surveyor Team]
Survey request initiated.
```

---

## Event 4: Policy Renewed

When a policy is renewed:

```csharp
policyManager.RenewPolicy();
```

Raise:

```csharp
PolicyRenewed
```

Subscribers:

```text
Renewal Department
Email Notification Service
```

Expected Output:

```text
Policy Renewed Successfully

[Renewal Team]
Policy renewal processed.

[Email Service]
Renewal confirmation email sent.
```

---

# Suggested Class Design

## InsurancePolicyManager

```csharp
class InsurancePolicyManager
{
    public event Action PolicyPurchased;
    public event Action PremiumPaid;
    public event Action ClaimRegistered;
    public event Action PolicyRenewed;
}
```

---

## Subscriber Classes

### SalesDepartment

```csharp
class SalesDepartment
{
    public void OnPolicyPurchased()
    {
        Console.WriteLine("Sales team notified.");
    }
}
```

### AccountsDepartment

```csharp
class AccountsDepartment
{
    public void OnPremiumPaid()
    {
        Console.WriteLine("Payment recorded.");
    }
}
```

### ClaimDepartment

```csharp
class ClaimDepartment
{
    public void OnClaimRegistered()
    {
        Console.WriteLine("Claim created.");
    }
}
```

### RenewalDepartment

```csharp
class RenewalDepartment
{
    public void OnPolicyRenewed()
    {
        Console.WriteLine("Renewal processed.");
    }
}
```

---

# Learning Objectives

After completing this assignment, students should understand:

### C# Events

```csharp
public event Action PolicyPurchased;
```

### Event Subscription

```csharp
manager.PolicyPurchased += salesDepartment.OnPolicyPurchased;
```

### Event Invocation

```csharp
PolicyPurchased?.Invoke();
```

### Publisher-Subscriber Pattern

```text
InsurancePolicyManager
         |
         | Raises Event
         v
---------------------------------
| Sales Team                  |
| Accounts Team               |
| Notification Service        |
| Claim Department            |
---------------------------------
```


# Bonus Challenge

Enhance the project by:

1. Using custom `EventArgs`
2. Passing Policy Number
3. Passing Customer Name
4. Passing Premium Amount
5. Logging all events into a file
6. Maintaining policy records using `List<Policy>`

This assignment closely resembles real-world enterprise applications where events are used for notifications, auditing, messaging, workflow automation, and microservice communication.
