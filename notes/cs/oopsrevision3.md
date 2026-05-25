Excellent discussion students… now we are entering the most fundamental definition in Object-Oriented Programming.

# What is an Object?

In software engineering interviews, books, and projects, you will hear:

> “Everything revolves around objects.”

But what exactly is an object?

Let us understand from project experience instead of textbook memorization.

---

# Definition of Object

An object is:

> A real-world entity having:

* State
* Behavior
* Identity

This is the complete definition.

---

# Object is an Instance of a Class

Suppose we have a class:

```java id="25nlqv"
class Person
{
}
```

Now if we create:

```java id="5pqik0"
Person p1 = new Person();
```

Then:

| Part         | Meaning   |
| ------------ | --------- |
| Person       | Class     |
| p1           | Reference |
| new Person() | Object    |

So:

# Object is an instance of a class.

Meaning:

* class is blueprint
* object is real entity created from blueprint

---

# Real World Analogy

Suppose:

```text id="7szrbo"
Person
```

is a template.

Now:

```text id="h91sfo"
Sachin
Ravi
Tejas
```

are real persons.

Similarly:

```java id="f0y5de"
Person p1
Person p2
```

represent different objects.

---

# Creating Objects

Now students carefully observe this statement:

```java id="7kvwjo"
Person p1 =
 new Person("Sachin","Patil",24,"988173580");
```

This single statement teaches complete OOP.

---

# Understanding the Statement

## 1. Person

This is class/type.

It defines:

* structure
* properties
* methods

---

## 2. p1

This is reference variable.

It stores:

* address of object
* reference of object

---

## 3. new Person(...)

This creates real object inside heap memory.

---

# Objects Have Identity

Every object created in memory has:

* unique address
* unique identity

Example:

```text id="vr3c5d"
p1 → Object at memory location A100
p2 → Object at memory location A250
```

Even if both belong to same class:
they are different objects.

That uniqueness is called:

# Identity

---

# Objects Have State

Now carefully observe:

```java id="t5kzvx"
Person p1 =
 new Person("Sachin","Patil",24,"988173580");
```

What is state of object?

State means:

# Current values stored inside object

---

# State of p1

| Property      | Value     |
| ------------- | --------- |
| firstName     | Sachin    |
| lastName      | Patil     |
| age           | 24        |
| contactNumber | 988173580 |

These values define current state of object.

---

# State Changes Over Time

Suppose:

```java id="5i4b6z"
p1.setAge(98);
```

Now object state changes.

Earlier:

```text id="n7qq7u"
age = 24
```

Now:

```text id="n7p7sv"
age = 98
```

So object state is dynamic.

---

# Real World Example of State

Suppose temperature object exists.

Morning:

```text id="ztj8cr"
temperature = 20°C
```

Afternoon:

```text id="86qk0h"
temperature = 38°C
```

Night:

```text id="r7m9s6"
temperature = 18°C
```

Temperature value changes.

That changing value represents:

# State

---

# Objects Have Behavior

Now students observe:

```java id="j1mq2r"
p1.show();
p1.getAge();
p1.setAge(98);
```

These are not values.

These are:

* actions
* operations
* functionalities

This is called:

# Behavior

---

# Behavior Means What Object Can Do

| Method   | Behavior            |
| -------- | ------------------- |
| show()   | Display information |
| getAge() | Return age          |
| setAge() | Modify age          |

Behavior defines:

* algorithms
* instructions
* actions

---

# State vs Behavior

Students usually confuse this.

Let us separate clearly.

---

# State

Represents:

* values
* data
* properties

Example:

```java id="g02w4u"
p1.firstName
p1.age
p1.contactNumber
```

These hold values.

---

# Behavior

Represents:

* methods
* actions
* functions

Example:

```java id="l4a8v7"
p1.show()
p1.setAge()
p1.getAge()
```

These perform actions.

---

# Another Real Example

Suppose AC object.

## State

```text id="k2rfr0"
temperature = 18
mode = cool
speed = high
```

---

## Behavior

```text id="jyc0gs"
increaseTemperature()
decreaseTemperature()
turnOn()
turnOff()
```

So:

| Concept  | Meaning          |
| -------- | ---------------- |
| State    | What object HAS  |
| Behavior | What object DOES |

---

# Multiple Objects from Same Class

Suppose:

```java id="fnp0zq"
Person p1 =
 new Person("Sachin","Patil",24);

Person p2 =
 new Person("Ravi","Tambade",45);
```

How many objects?

# Two objects

Same class:

* different states
* different identities

---

# Visualization

```text id="zdhpaz"
Person Class
   |
   +----> p1 Object
   |        name = Sachin
   |        age = 24
   |
   +----> p2 Object
            name = Ravi
            age = 45
```

---

# Understanding Dynamic State Change

Suppose initially:

```java id="g1wfpw"
p1.getAge()
```

returns:

```text id="4d1f1m"
24
```

Then:

```java id="sjc5hb"
p1.setAge(98);
```

Now:

```java id="a4dfy9"
p1.getAge()
```

returns:

```text id="c8x6va"
98
```

Why?

Because behavior modified object state.

This is core OOP understanding.

---

# Mentor Perspective

Students…

When you see object:
do not think only syntax.

Visualize:

* real-world entity
* memory allocation
* current state
* actions performed
* state changes happening

That is actual object-oriented thinking.

---

# Final Understanding

An object is:

* A real-world entity
* Created from class
* Stored in heap memory
* Accessed through references
* Having:

  * identity
  * state
  * behavior

And entire enterprise backend applications are nothing but:

# Thousands of objects collaborating together.
