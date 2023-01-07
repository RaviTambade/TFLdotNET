## MVC


MVC (Model-View-Controller) is a pattern in software design commonly used to implement user interfaces, data, and controlling logic. It emphasizes a separation between the software's business logic and display. This "separation of concerns" provides for a better division of labor and improved maintenance. Some other design patterns are based on MVC, such as MVVM (Model-View-Viewmodel), MVP (Model-View-Presenter), and MVW (Model-View-Whatever).

The three parts of the MVC software-design pattern can be described as follows:

- Model: Manages data and business logic.
The model manages the behavior and data of the application domain, responds to requests for information about its state (usually from the view), and responds to instructions to change state (usually from the controller). It maintains the state and notified observers/subscribers of change in information.

- View: Handles layout and display.
The view manages the display of information and also facilitates interaction with user. However, it does not act on user interaction (i.e., events) - that is the job of the controller

- Controller: Routes commands to the model and view parts.
The controller interprets the mouse and keyboard inputs from the user, informing the model and/or the view to change as appropriate.




### Model View Controller example

Imagine a simple shopping list app. All we want is a list of the name, quantity and price of each item we need to buy this week. Below we'll describe how we could implement some of this functionality using MVC.

- The Model
The model defines what data the app should contain. If the state of this data changes, then the model will usually notify the view (so the display can change as needed) and sometimes the controller (if different logic is needed to control the updated view).

Going back to our shopping list app, the model would specify what data the list items should contain — item, price, etc. — and what list items are already present.

- The View
The view defines how the app's data should be displayed.

In our shopping list app, the view would define how the list is presented to the user, and receive the data to display from the model.

- The Controller
The controller contains logic that updates the model and/or view in response to input from the users of the app.

So for example, our shopping list could have input forms and buttons that allow us to add or delete items. These actions require the model to be updated, so the input is sent to the controller, which then manipulates the model as appropriate, which then sends updated data to the view.

You might however also want to just update the view to display the data in a different format, e.g., change the item order to alphabetical, or lowest to highest price. In this case the controller could handle this directly without needing to update the model.


From a high-level perspective, the Controller is the brains of your application, handling the business logic, the Model handles the slice of the data that is relevant at-the-moment, and the View encapsulates the user interface (UI) or output of the system.


#### 1. HOW: Separation of Concerns
How do we delineate which methods to place in our Controller v.s. Model v.s. View? That’s where the importance of the separation of concerns comes into play.

#### The Model

The Model represents and handles the data your application needs to run, especially as an in-memory, partial and local representation of the data that lives in your database(s) / data store(s). All the methods you need to create, read, update, and delete (i.e. CRUD) this data should be in your Model, thereby allowing your application access to those methods.

Some also place API methods, especially those that interface with your database(s) here as well. Doing so makes sense, but it’s not the only way to go about it. I actually prefer to conceptualize and incorporate these in the Controller, for reasons I’ll explain later.

The bottom line is that the Model captures and contains the data your application currently needs, in memory, to be able to display what it needs to output. It does not contain your entire database, only as much as it needs right now.

The Model receives and contains the data it needs from your database’s file storage (i.e. an SSD) to local memory (i.e. RAM) in order to rapidly render these on the page.

When you’re scrolling down your feed on Facebook, for example, the Model is being updated with more data from the database, probably several steps ahead of your scroll, to ensure a seamless transition. But each scroll most likely makes yet again another API call to the database, to add one (or more) tidbits of data to the Model, ready to be displayed on your next scroll.

#### The View
The View is the part of the application that your user sees and interacts with. For front-end applications, it’s the DOM. For an API or other server-side microservice, we might think of the View as the output of the system, i.e. the response from the server.

The common theme is that the View mainly represents the output from the system. For a fullstack system, where the user also triggers UI events and enters data, however, the View also includes those interfaces that capture inputs or events from the user.

These might include and incorporate, for example, <input/> and <button></button> elements from a <form></form>, or generally elements that can be clicked on.

#### The Controller
Finally, the Controller is the brains of the application, where most of the business logic lives. The Controller is the middleman that pulls data from the Model, and sends it to the View to be rendered on the page.

In the other direction, the Controller receives UI events from the View, processes them, and sends data to the Model if necessary (e.g. to add data from the user input to the Model).

In our increasingly cloud-based and microservices-oriented applications, we also need to understand where to place asynchronous API calls. In my mental model, I prefer to think of the Model as exclusively representing the state of the application in-the-moment. This is especially useful when thinking of front-end libraries like React, where each component’s state represents the data as it exists in-the-moment. This state can easily and quickly change, thereby necessitating a re-render of the View.

That’s part of the reason why it conceptually and usually makes more sense to incorporate API calls in the Controller, especially because their asynchronous nature means it will be at least a while before we receive the response and can update both the Model and the View. There are definitely applications, however, where encapsulating API calls in the Model is a better way to go.

The bottom line is that these aren’t always rigid demarcation lines, and the details of your MVC implementation might vary depending on the needs of your application, and even perhaps on the structure of your organization and teams responsible for the maintenance of your application.

#### 2. WHY: Simplicity, Flexibility, and Reusability
What are some of the main advantages of the MVC pattern?

- Simplicity - First of all, having those three separate concepts adds a certain level of simplicity to your application, clearly delineating where each method should be placed, according to the separation of concerns principle. The pattern does away with complex inheritance structures that might involve six or more classes and / or otherwise relies on multiple and varied relationships between all these different classes.
- Flexibility - The MVC pattern is also flexible and can accommodate any feature that might need to be added later on. It has room for growth within its basic structure.
- Reusability - Finally, because it is a familiar pattern that many developers are already used to, and because of its flexibility, it’s a pattern that can be used over and over again, so it can be applied to any application or business need. It also means applications that follow this pattern are easier to maintain in the long run.

#### 3. SO WHAT?
Design patterns are helpful because they provide us with a blueprint for an architectural framework that has worked before, and would probably also work again. But each pattern’s usefulness and applicability of course varies and depends on your system’s particular use case and business needs.

Although MVC was originally designed in the 1970s as a desktop computing framework for the Smalltalk language, it has evolved to be incorporated in many web frameworks for Java,.NET, Python, and Ruby.