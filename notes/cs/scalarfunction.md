# ** Scalar Functions in MS SQL Server**

## 🎯 Learning Objective

By the end of this session, every aspiring Software Engineer should be able to:

* Understand what a Scalar Function is.
* Know why SQL Server provides Scalar Functions.
* Use built-in Scalar Functions in real-world database applications.
* Write cleaner and more efficient SQL queries.

# Think Like a Software Engineer

Imagine you are developing an **E-Commerce Application**.

Your **Products** table contains:

| ProductId | ProductName |    Price | CreatedDate |
| --------- | ----------- | -------: | ----------- |
| 101       | Laptop      | 65000.50 | 2026-01-10  |
| 102       | Mouse       |   850.25 | 2026-03-12  |
| 103       | Keyboard    |  1200.75 | 2026-05-15  |

Your customer wants:

* Product names in **UPPERCASE**
* Rounded prices
* Current date
* Number of products
* Difference between today's date and product creation date

Should you write C# code for every calculation?

**No.**

SQL Server already provides built-in functions.

These are called **Scalar Functions**.


# What is a Scalar Function?

A **Scalar Function** accepts one or more values as input and returns **exactly one value**.

```
Input  ---------> Scalar Function ---------> Single Output
```

Examples

```
UPPER('transflower')
Returns

TRANSFLOWER
```

```
LEN('Database')

Returns

8
```

Only **one value** is returned.

# Why are Scalar Functions Important?

As Software Engineers, we frequently need to

* Format data
* Validate data
* Convert data types
* Perform calculations
* Handle dates
* Manipulate strings

Instead of writing application logic repeatedly,

SQL Server performs these operations efficiently.

# Categories of Scalar Functions

## 1. String Functions

Used to manipulate text.

Common functions

```
UPPER()
LOWER()
LEN()
LEFT()
RIGHT()
SUBSTRING()
LTRIM()
RTRIM()
REPLACE()
CONCAT()
```

Example

```sql
SELECT UPPER('transflower');
```

Output

```
TRANSFLOWER
```

---

Example

```sql
SELECT LEN('Software Engineer');
```

Output

```
17
```

---

Example

```sql
SELECT LEFT('Microsoft',5);
```

Output

```
Micro
```

Example

```sql
SELECT RIGHT('Database',4);
```

Output

```
base
```

# Real Project Example

Customer table

| CustomerName |
| ------------ |
| Ravi Tambade |

Display names in uppercase.

```sql
SELECT UPPER(CustomerName)
FROM Customers;
```

# 2. Numeric Functions

Used for mathematical calculations.

Common functions

```
ABS()
ROUND()
POWER()
SQRT()
CEILING()
FLOOR()
RAND()
```

Example

```sql
SELECT ABS(-25);
```

Output

```
25
```

Example

```sql
SELECT ROUND(123.5678,2);
```

Output

```
123.57
```

Example

```sql
SELECT CEILING(125.10);
```

Output

```
126
```

Example

```sql
SELECT FLOOR(125.99);
```

Output

```
125
```

# Real Project Example

Round product prices.

```sql
SELECT ProductName,
       ROUND(Price,2)
FROM Products;
```

# 3. Date Functions

Used to work with dates and time.

Common functions

```
GETDATE()
YEAR()
MONTH()
DAY()
DATEADD()
DATEDIFF()
```

Example

```sql
SELECT GETDATE();
```

Output

Current system date and time.

Example

```sql
SELECT YEAR(GETDATE());
```

Output

```
2026
```

Example

```sql
SELECT MONTH(GETDATE());
```

Output

```
7
```


Example

```sql
SELECT DATEDIFF(DAY,'2026-01-01',GETDATE());
```

Returns number of days.


# Real Project Example

Insurance Premium Due

```sql
SELECT PolicyNo,
       DATEDIFF(DAY,IssueDate,GETDATE()) AS PolicyAge
FROM Policies;
```

# 4. Conversion Functions

Convert one datatype into another.

Common functions

```
CAST()
CONVERT()
TRY_CAST()
TRY_CONVERT()
```

Example

```sql
SELECT CAST(125.50 AS INT);
```

Output

```
125
```

Example

```sql
SELECT CONVERT(VARCHAR,GETDATE(),103);
```

Output

```
31/07/2026
```

---

# 5. System Functions

Provide information about SQL Server.

Examples

```sql
SELECT DB_NAME();
```

Returns

Current database name.

---

```sql
SELECT USER_NAME();
```

Returns

Current logged-in user.

---

```sql
SELECT HOST_NAME();
```

Returns

Computer name.

---

# Real-Life Banking Example

Balance

```
4500.756
```

Display

```
4500.76
```

```sql
SELECT ROUND(Balance,2)
FROM Accounts;
```

---

# Real-Life HR Example

Employee Name

```
ravi tambade
```

Display

```
RAVI TAMBADE
```

```sql
SELECT UPPER(EmployeeName)
FROM Employees;
```

---

# Real-Life Insurance Example

Policy Age

```sql
SELECT
DATEDIFF(YEAR,
IssueDate,
GETDATE())
FROM Policies;
```

---

# Interview Questions

### Q1 What is a Scalar Function?

A function that returns **a single value**.

---

### Q2 Difference between Scalar and Aggregate Function?

| Scalar Function                | Aggregate Function                    |
| ------------------------------ | ------------------------------------- |
| Returns one value for each row | Returns one value for a group of rows |
| Works row by row               | Works on multiple rows                |
| Example: UPPER()               | Example: SUM()                        |

---

### Q3 Can Scalar Functions be nested?

Yes.

```sql
SELECT UPPER(LEFT('transflower',5));
```

Output

```
TRANS
```

---

### Q4 Can Scalar Functions be used inside WHERE clause?

Yes.

```sql
SELECT *
FROM Customers
WHERE UPPER(City)='PUNE';
```

---

### Q5 Name five Scalar Functions.

* UPPER()
* LOWER()
* LEN()
* GETDATE()
* ROUND()

---

# Best Practices

✅ Use built-in Scalar Functions whenever possible.

✅ Avoid unnecessary nested functions in large queries.

✅ Use `TRY_CAST()` instead of `CAST()` when conversion failures are possible.

✅ Apply functions thoughtfully on indexed columns, as wrapping a column in a function (for example, `UPPER(CustomerName)`) can prevent efficient index usage.

---

# Transflower Mentor's Take

> **"A Software Engineer should never think of SQL Server as just a place to store data. Think of it as a powerful data processing engine. Scalar Functions allow us to transform, validate, calculate, and format information close to the data, reducing application complexity and improving maintainability."**

---

# Remember This

```
Functions in SQL Server

                Functions
                     |
     --------------------------------
     |              |              |
 Scalar       Aggregate        Window
     |
-------------------------------
|       |        |      |      |
String Numeric Date Conversion System
```

### Key Takeaways

* A **Scalar Function returns exactly one value**.
* Scalar Functions are executed **for each row** in a query.
* They simplify string manipulation, mathematical calculations, date operations, type conversions, and system information retrieval.
* Common real-world uses include formatting names, rounding prices, calculating policy age, and converting data types.
* Mastering Scalar Functions is essential for SQL Server development and frequently tested in .NET and SQL interview rounds.
