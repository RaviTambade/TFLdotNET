# 🏦 MaxNewYorkInsurance – Complete Database DDL

```sql id="full_insurance_schema"
CREATE DATABASE IF NOT EXISTS MaxNewYorkInsurance;

USE MaxNewYorkInsurance;

---------------------------------------------------
-- 1. CUSTOMERS
---------------------------------------------------
CREATE TABLE Customers
(
    CustomerId INT AUTO_INCREMENT PRIMARY KEY,
    CustomerCode VARCHAR(20) NOT NULL UNIQUE,

    FirstName VARCHAR(50) NOT NULL,
    LastName VARCHAR(50) NOT NULL,
    DateOfBirth DATE,
    Gender VARCHAR(20),

    Email VARCHAR(100) NOT NULL UNIQUE,
    MobileNumber VARCHAR(15) NOT NULL,

    AddressLine1 VARCHAR(200),
    AddressLine2 VARCHAR(200),
    City VARCHAR(100),
    State VARCHAR(100),
    PostalCode VARCHAR(20),
    Country VARCHAR(100),

    PanNumber VARCHAR(20) UNIQUE,
    AadhaarNumber VARCHAR(20) UNIQUE,

    Occupation VARCHAR(100),
    AnnualIncome DECIMAL(15,2) DEFAULT 0.00,

    NomineeName VARCHAR(100),
    NomineeRelationship VARCHAR(50),
    NomineeContactNumber VARCHAR(15),

    RegistrationDate DATETIME DEFAULT CURRENT_TIMESTAMP,
    IsActive BOOLEAN DEFAULT TRUE,
    TotalPoliciesPurchased INT DEFAULT 0
);

---------------------------------------------------
-- 2. POLICIES
---------------------------------------------------
CREATE TABLE Policies
(
    PolicyId INT AUTO_INCREMENT PRIMARY KEY,
    PolicyNumber VARCHAR(30) NOT NULL UNIQUE,

    CustomerId INT NOT NULL,

    PolicyType VARCHAR(50) NOT NULL,
    PolicyAmount DECIMAL(15,2) NOT NULL,
    IsRenewed BOOLEAN DEFAULT FALSE,

    FOREIGN KEY (CustomerId)
        REFERENCES Customers(CustomerId)
        ON DELETE CASCADE
);

---------------------------------------------------
-- 3. PREMIUMS
---------------------------------------------------
CREATE TABLE Premiums
(
    PremiumId INT AUTO_INCREMENT PRIMARY KEY,

    PolicyId INT NOT NULL,
    CustomerId INT NOT NULL,

    AmountPaid DECIMAL(12,2) NOT NULL,
    PaymentDate DATETIME NOT NULL,

    PaymentMode VARCHAR(30) NOT NULL,
    TransactionId VARCHAR(100) NOT NULL UNIQUE,

    PaymentFrequency VARCHAR(20) NOT NULL,
    PaymentStatus VARCHAR(20) DEFAULT 'Success',

    Remarks VARCHAR(500),

    FOREIGN KEY (PolicyId)
        REFERENCES Policies(PolicyId),

    FOREIGN KEY (CustomerId)
        REFERENCES Customers(CustomerId)
);

---------------------------------------------------
-- 4. CLAIMS
---------------------------------------------------
CREATE TABLE Claims
(
    ClaimId INT AUTO_INCREMENT PRIMARY KEY,

    PolicyNumber VARCHAR(30) NOT NULL,
    CustomerId INT NOT NULL,

    ClaimDate DATETIME NOT NULL,
    ClaimType VARCHAR(50) NOT NULL,
    Reason VARCHAR(500),

    ClaimAmount DECIMAL(15,2) NOT NULL,
    ApprovedAmount DECIMAL(15,2) DEFAULT 0.00,

    Status VARCHAR(30) DEFAULT 'Registered',
    Remarks VARCHAR(500),

    SettlementDate DATETIME NULL,

    FOREIGN KEY (CustomerId)
        REFERENCES Customers(CustomerId)
);

---------------------------------------------------
-- 5. EMPLOYEES
---------------------------------------------------
CREATE TABLE Employees
(
    EmployeeId INT AUTO_INCREMENT PRIMARY KEY,

    EmployeeCode VARCHAR(20) NOT NULL UNIQUE,
    FirstName VARCHAR(50) NOT NULL,
    LastName VARCHAR(50) NOT NULL,

    Email VARCHAR(100) NOT NULL UNIQUE,
    MobileNumber VARCHAR(15) NOT NULL,

    Department VARCHAR(50) NOT NULL,
    Designation VARCHAR(50) NOT NULL,

    DateOfJoining DATE NOT NULL,
    Salary DECIMAL(15,2) DEFAULT 0.00,
    IsActive BOOLEAN DEFAULT TRUE,

    Address VARCHAR(300)
);

---------------------------------------------------
-- 6. AGENTS
---------------------------------------------------
CREATE TABLE Agents
(
    AgentId INT AUTO_INCREMENT PRIMARY KEY,

    AgentCode VARCHAR(20) NOT NULL UNIQUE,
    FullName VARCHAR(100) NOT NULL,

    Email VARCHAR(100) NOT NULL UNIQUE,
    MobileNumber VARCHAR(15) NOT NULL,

    LicenseNumber VARCHAR(50) NOT NULL UNIQUE,
    Branch VARCHAR(100),
    Designation VARCHAR(50),

    CommissionRate DECIMAL(5,4) DEFAULT 0.0000,
    TotalCommissionEarned DECIMAL(15,2) DEFAULT 0.00,

    DateOfJoining DATE NOT NULL,
    IsActive BOOLEAN DEFAULT TRUE
);
```

---

# 🧠 Mentor Architecture Insight

## 🔗 Final Relationships

```
Customers
   │
   ├── Policies
   │      ├── Premiums
   │      └── Claims
   │
Employees (Internal Staff)
Agents (External Sales Force)
```

---

# 🚀 What You Now Have

You just designed a **real Insurance ERP database** with:

- ✔ Customer Management
- ✔ Policy Lifecycle
- ✔ Premium Billing System
- ✔ Claim Processing System
- ✔ Employee Management
- ✔ Agent Commission System
- ✔ Fully normalized relational schema

 
# 📥 Sample INSERT Script – MaxNewYorkInsurance

```sql id="seed_max_insurance"
USE MaxNewYorkInsurance;

---------------------------------------------------
-- 1. CUSTOMERS
---------------------------------------------------
INSERT INTO Customers
(CustomerCode, FirstName, LastName, DateOfBirth, Gender, Email, MobileNumber,
 City, State, PostalCode, Country, PanNumber, AadhaarNumber,
 Occupation, AnnualIncome, NomineeName, NomineeRelationship, NomineeContactNumber,
 RegistrationDate, IsActive, TotalPoliciesPurchased)
VALUES
('CUST1001','Ravi','Tambade','1990-05-15','Male','ravi@gmail.com','9876543210',
'Pune','Maharashtra','411001','India','ABCDE1234F','123412341234',
'Engineer',1200000,'Anita Tambade','Spouse','9876543222',NOW(),TRUE,2),

('CUST1002','Amit','Sharma','1988-11-20','Male','amit@gmail.com','9876500000',
'Mumbai','Maharashtra','400001','India','PQRST5678K','987654321012',
'Doctor',2000000,'Neha Sharma','Spouse','9876501111',NOW(),TRUE,1),

('CUST1003','Sneha','Patil','1995-03-10','Female','sneha@gmail.com','9123456780',
'Nagpur','Maharashtra','440001','India','LMNOP9876Z','567890123456',
'Teacher',600000,'Rahul Patil','Husband','9123400000',NOW(),TRUE,3);

---------------------------------------------------
-- 2. POLICIES
---------------------------------------------------
INSERT INTO Policies
(PolicyNumber, CustomerId, PolicyType, PolicyAmount, IsRenewed)
VALUES
('POL1001',1,'Health',500000.00,FALSE),
('POL1002',1,'Life',1000000.00,TRUE),
('POL1003',2,'Motor',300000.00,FALSE),
('POL1004',3,'Travel',200000.00,TRUE),
('POL1005',3,'Health',750000.00,FALSE);

---------------------------------------------------
-- 3. PREMIUMS
---------------------------------------------------
INSERT INTO Premiums
(PolicyId, CustomerId, AmountPaid, PaymentDate,
 PaymentMode, TransactionId, PaymentFrequency, PaymentStatus, Remarks)
VALUES
(1,1,12000.00,NOW(),'UPI','TXN10001','Annual','Success','Paid full premium'),
(2,1,25000.00,NOW(),'Credit Card','TXN10002','Annual','Success','Renewed policy'),
(3,2,8000.00,NOW(),'Net Banking','TXN10003','Quarterly','Success','Installment paid'),
(4,3,5000.00,NOW(),'UPI','TXN10004','Monthly','Pending','Awaiting confirmation'),
(5,3,15000.00,NOW(),'Cash','TXN10005','Annual','Success','Cash payment received');

---------------------------------------------------
-- 4. CLAIMS
---------------------------------------------------
INSERT INTO Claims
(PolicyNumber, CustomerId, ClaimDate, ClaimType, Reason,
 ClaimAmount, ApprovedAmount, Status, Remarks, SettlementDate)
VALUES
('POL1001',1,NOW(),'Health','Hospitalization due to surgery',
50000.00,45000.00,'Approved','Verified documents',DATE_ADD(NOW(), INTERVAL 3 DAY)),

('POL1003',2,NOW(),'Motor','Accident damage repair',
120000.00,0.00,'Under Review','Surveyor assigned',NULL),

('POL1005',3,NOW(),'Health','Medical emergency',
30000.00,30000.00,'Settled','Payment completed',NOW());

---------------------------------------------------
-- 5. EMPLOYEES
---------------------------------------------------
INSERT INTO Employees
(EmployeeCode, FirstName, LastName, Email, MobileNumber,
 Department, Designation, DateOfJoining, Salary, Address, IsActive)
VALUES
('EMP1001','Anita','Patil','anita@company.com','9876543210',
'Claims','Officer','2024-07-01',55000,'Pune',TRUE),

('EMP1002','Rahul','Sharma','rahul@company.com','9876500000',
'Sales','Executive','2023-11-15',45000,'Mumbai',TRUE),

('EMP1003','Sneha','Deshmukh','sneha@company.com','9123456780',
'Accounts','Manager','2022-06-10',75000,'Nagpur',TRUE);

---------------------------------------------------
-- 6. AGENTS
---------------------------------------------------
INSERT INTO Agents
(AgentCode, FullName, Email, MobileNumber,
 LicenseNumber, Branch, Designation,
 CommissionRate, TotalCommissionEarned,
 DateOfJoining, IsActive)
VALUES
('AGT1001','Ramesh Sharma','ramesh@gmail.com','9876543210',
'LIC1001','Pune','Senior Advisor',0.10,250000,'2023-05-15',TRUE),

('AGT1002','Priya Deshmukh','priya@gmail.com','9123456780',
'LIC1002','Mumbai','Advisor',0.08,180000,'2022-08-10',TRUE),

('AGT1003','Suresh Patil','suresh@gmail.com','9988776655',
'LIC1003','Nagpur','Manager',0.12,500000,'2021-01-20',TRUE);
```

 

# 🧠 Mentor Insight (Important)

You now have a **fully working enterprise dataset**:

### 🔗 Relationship Flow

```
Customer
   ├── Policies
   │      ├── Premiums (Payments)
   │      └── Claims
   │
Employees (Internal Operations)
Agents (Sales & Commission)
```

 