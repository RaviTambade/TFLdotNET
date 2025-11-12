Bank Loan Processing — Problem Statement (C# / Object-Oriented)

Below is a ready-to-use, self-contained problem statement you can give to students or use as a project spec. It emphasizes object-oriented design (encapsulation, inheritance, polymorphism, interfaces), clear use cases, sample data, acceptance tests, and extension (bonus) tasks.


---

1 — Project Title

LoanX — A console / desktop / simple web application for bank loan processing using object-oriented C#.


---

2 — Goal

Design and implement a bank loan processing system that models customers, accounts, loan products, loan applications, approvals, disbursal, and repayment schedules using OOP principles in C#. The system should simulate real workflow steps and validate business rules.


---

3 — Core Functional Requirements

1. Customer management

Create, update, view customers.

Customer fields: CustomerId, FullName, DOB, PAN/SSN, Email, Phone, Address, CreditScore.



2. Account management

Each customer may have one or more bank accounts.

Account fields: AccountId, Customer, AccountType (Savings/Checking), Balance.



3. Loan products

Support multiple loan types with shared base behavior and type-specific details: PersonalLoan, HomeLoan, AutoLoan.

Product configuration: LoanType, InterestRate (annual), MaxAmount, MinCreditScore, MaxTenureMonths.



4. Loan application

Customer applies for a loan: LoanApplication with RequestedAmount, RequestedTenureMonths, Collateral (optional), Status (Draft → Submitted → UnderReview → Approved/Rejected → Disbursed → Closed).

Validate requested amount vs product limits, credit score checks, and simple debt-to-income check.



5. Approval workflow

A LoanOfficer reviews applications. Approval rules combine automated checks and officer decision.

Implement an interface ICreditCheckService that returns credit score (allow mock for tests).



6. EMI calculation & repayment schedule

Generate amortization schedule using standard EMI formula for equal monthly installments.

Track installments: due date, amount, principal, interest, status (Pending, Paid, Late).



7. Disbursal & repayments

On approval, disburse to selected account; update account balance.

Allow payments (full or partial) and update outstanding principal and schedule.



8. Notifications / logging

Notify customer on important events (submission, approval, disbursal, overdue). Implement INotificationService interface (console/mock).



9. Reporting

Basic reports: active loans, overdue loans, portfolio by loan type.



10. Persistence (optional)

For initial project, in-memory collections are fine. Optionally use file-based (JSON) or database.





---

4 — Non-Functional Requirements

Use C# and object-oriented design principles.

Clean separation of concerns: domain classes, services, data access (repository), UI.

Unit tests for core logic (EMI calculation, approval rules, schedule generation).

Clear error handling and validation.

Code should be modular and easy to extend (open/closed principle).



---

5 — Suggested Domain Model (Classes & Interfaces)

Core classes

Customer

Properties: Id, Name, DOB, PAN, Email, Phone, Address, CreditScore

Methods: UpdateContact().


Account

Properties: Id, CustomerId, AccountType, Balance

Methods: Credit(amount), Debit(amount) (with insufficient funds handling).


LoanProduct (abstract/base)

Properties: ProductId, Name, InterestRate, MaxAmount, MinCreditScore, MaxTenureMonths

Methods: ValidateApplication(LoanApplication) (virtual).


LoanApplication

Properties: ApplicationId, Applicant (Customer), Product (LoanProduct), RequestedAmount, RequestedTenureMonths, Collateral, Status, CreatedDate, DecisionBy, DecisionDate

Methods: Submit(), Withdraw().


Loan (represents active loan)

Properties: LoanId, Application, Principal, InterestRate, TenureMonths, StartDate, OutstandingPrincipal, Schedule (list of Installment), Status

Methods: MakePayment(amount, date), CalculateEMI(), GenerateSchedule().


Installment

Properties: InstallmentNumber, DueDate, EMIAmount, PrincipalComponent, InterestComponent, PaidAmount, Status (Pending/Paid/Overdue)

Methods: ApplyPayment(amount, date).



Actors / Services

LoanOfficer

Methods: ReviewApplication(application, decision, remarks)


ICreditCheckService (interface)

Method: GetCreditScore(customer) — allow mock implementation.


INotificationService (interface)

Methods: NotifyCustomer(customer, message)


ILoanRepository, ICustomerRepository, IAccountRepository (interfaces for persistence)


Example inheritance/polymorphism

LoanProduct → PersonalLoan, HomeLoan, AutoLoan override validation rules and extra fields (e.g., HomeLoan requires property valuation).



---

6 — Example Sequence (Happy Path)

1. Customer registers / exists.


2. Customer opens or selects account for disbursal.


3. Customer selects loan product and creates LoanApplication.


4. System runs automatic validation (credit score, limits).


5. LoanApplication status → Submitted.


6. LoanOfficer reviews and approves.


7. On approval:

Create Loan object.

Generate EMI schedule.

Disburse principal to selected account (account.Credit(amount)).

Notify customer.



8. Customer makes monthly payments; Loan.MakePayment() updates installments and outstanding principal.




---

7 — Business Rules (examples)

Minimum credit score per product must be met.

Requested amount ≤ product.MaxAmount.

Tenure ≤ product.MaxTenureMonths.

If collateral required (HomeLoan), collateral value must be ≥ 1.2 × requested amount.

EMI calculated monthly using annual interest rate converted to monthly.


EMI formula (use in code):

r = monthlyRate = annualRate / 12 / 100
n = months
EMI = P * r * (1 + r)^n / ((1 + r)^n - 1)


---

8 — Sample Data (for testing)

Customers

C001 — Ravi Tambade — CreditScore 720

C002 — Priya Joshi — CreditScore 640


LoanProducts

PersonalLoan — Interest 12% p.a, MaxAmount ₹5,00,000, MinCreditScore 650, MaxTenure 60 months

HomeLoan — Interest 8.2% p.a, MaxAmount ₹1,00,00,000, MinCreditScore 700, MaxTenure 360 months (collateral required)

AutoLoan — Interest 9.5% p.a, MaxAmount ₹30,00,000, MinCreditScore 660, MaxTenure 84 months



---

9 — Acceptance Tests / Example Scenarios

1. Apply & Approve

Customer with credit score 720 applies for PersonalLoan ₹2,00,000 for 36 months.

System validates and officer approves.

EMI generated and first disbursal credited.



2. Reject due to low score

Customer with credit score 620 requests PersonalLoan (min 650) → automatic rejection with proper message.



3. Collateral check

HomeLoan application with collateral valuation less than required → validation fails.



4. Repayment

Pay EMI partially and verify remaining installment and outstanding principal are correct.



5. Overdue detection

Simulate missed payments; loan marked overdue; notification created.





---

10 — Suggested Project Structure (C#)

/LoanX
  /LoanX.Domain
    Customer.cs
    Account.cs
    LoanProduct.cs
    PersonalLoan.cs
    HomeLoan.cs
    LoanApplication.cs
    Loan.cs
    Installment.cs
  /LoanX.Services
    CreditCheckServiceMock.cs
    NotificationServiceConsole.cs
    LoanProcessingService.cs
  /LoanX.Repo
    InMemoryRepositories.cs
  /LoanX.UI
    Program.cs (console UI) or web controllers
  /LoanX.Tests
    EmICalculationTests.cs
    LoanApprovalTests.cs
    ScheduleGenerationTests.cs


---

11 — Evaluation / Grading Rubric (if used as assignment)

Domain modeling & OOP usage (30%)

Clear classes, appropriate encapsulation, use of inheritance & interfaces, SRP.


Business logic correctness (30%)

EMI formula, schedule accuracy, validation rules, approval flow.


Code quality & design (15%)

Separation of concerns, repository/services, error handling, naming.


Tests & sample data (15%)

Unit tests for EMI, approval rules, schedule; demonstration scenarios.


Extras / Bonus (10%)

Persistence (JSON/DB), UI (simple web), concurrency, extended reports, logging.




---

12 — Stretch / Bonus Enhancements

Integrate a mock external credit bureau (simulate delays and async calls).

Implement role-based access (Officer, Manager) with approval escalation.

Add refinancing requests, prepayment handling, and penalty calculation.

Add persistence with EF Core and a SQLite DB.

Build a small web front-end using ASP.NET Core MVC minimal APIs for interactions.



---

13 — Starter Skeleton (pseudo-code snippet)

public abstract class LoanProduct {
    public string Name { get; set; }
    public decimal AnnualInterestRate { get; set; }
    public decimal MaxAmount { get; set; }
    public int MaxTenureMonths { get; set; }
    public int MinCreditScore { get; set; }

    public virtual bool Validate(LoanApplication app) {
        return app.RequestedAmount <= MaxAmount && app.RequestedTenureMonths <= MaxTenureMonths;
    }
}

public class PersonalLoan : LoanProduct {
    // override validations if needed
}

public class Loan {
    public decimal Principal { get; private set; }
    public decimal AnnualInterestRate { get; private set; }
    public int TenureMonths { get; private set; }
    public List<Installment> Schedule { get; private set; }

    public decimal CalculateEmi() {
        var r = AnnualInterestRate / 12 / 100;
        var n = TenureMonths;
        if (r == 0) return Principal / n;
        var factor = (decimal)Math.Pow((double)(1 + r), n);
        return Principal * r * factor / (factor - 1);
    }
}


---

If you want, I can:

Provide a ready-to-run console app skeleton with the classes above and some sample data + unit tests, or

Create a detailed UML diagram (text or simple ASCII) or a starter GitHub-ready project layout.


Which of those would help you next?
