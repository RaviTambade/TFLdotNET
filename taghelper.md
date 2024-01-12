
# ASP.NET Core Tag Helpers


In the year 2008, Microsoft released the first version of MVC i.e. Model, View, and Controller for the web programming framework. It was one of the biggest revolutionary steps by Microsoft in the recent past. Because, before this era, web developers mainly developed by using web forms which are mainly maintained by the control of the HTML templates including CSS and Scripting languages as required.

The concept of web forms is very simple and easy for web developers, especially for beginners. But, in the case of MVC, the concept is a little harder. Because of this technology, web developers need to take full responsibility related to all web content in their applications.
In MVC, developers normally do not use any web content for their applications. Because, Microsoft introduced three helper objects (HtmlHelper, UrlHelper, and AjaxHelper) for generating web control in the application. These helper objects simply shorten the work of the developer for designing any application of web interface. In the MVC pattern, all the code of Razor views (including server-side) starts with the @ sign. In this way, the MVC framework always has a clear separation between the server-side code and client-side code.


## Why Tag Helpers?
Microsoft introduced a new feature in the MVC Razor engine with the release of ASP.NET Core which is known as Tag Helpers. This feature helps web developers use their old conventional HTML tags in their web applications for designing presentation layers.

So, with the help of Tag Helpers, developers can replace the Razor cryptic syntax with the @ symbol, a more natural-looking HTML-like syntax. So, the first question always arises “Why do we need Tag Helpers?”. The simple answer is that Tag Helpers reduce the coding amount in HTML which we need to write and also create an abstracted layer between our UI and server-side code. We can extend the existing HTML tag elements or create custom elements just like HTML elements with the help of Tag Helpers.


We can write server-side code in the Razor files to create new elements or render HTML elements. So, we can define the customs elements name, attribute, or parent name just like the HTML elements by using Tag Helpers. But, we need to remember that Tag Helpers does not replace the HTML helpers, so we can use both of them side by side as per our requirement. In the below example, we can see the difference between the two helper methods
```

// HTML Helpers
@Html.ActionLink("Click", "Controller1", "CheckData", { @class="my-css-classname", data_my_attr="my-attribute"}) 

//Tag Helpers
<a asp-controller="Controller1" asp-action="CheckData" class="my-css-classname" my-attr="my-attribute">Click</a>


```


## Characteristics of Tag Helpers in ASP.NET Core MVC
- <b>HTML-like Syntax</b>: Tag Helpers use an HTML-like syntax that is familiar to web developers, making it easier to understand and work with.
- <b>Separation of Concerns</b>: Tag Helpers help separate UI logic from markup, promoting a cleaner and more organized codebase.
- <b>Reusability</b>: Custom Tag Helpers can encapsulate complex UI components and behaviors, making them reusable across multiple views.
- <b>Intuitive Integration</b>: Tag Helpers naturally integrate server-side logic into your markup, making the code more readable and coherent.
- <b>Encapsulation of Complexity</b>: Complex server-side logic and conditional rendering can be encapsulated within Tag Helpers, making your views cleaner and more maintainable.
- <b>IDE Support</b>: Tag Helpers provide better IntelliSense and tooling support in modern development environments.


## Types of Tag Helpers
ASP.NET Core MVC has two main types of Tag Helpers: Built-in Tag Helpers and Custom Tag Helpers. Both types of Tag Helpers contribute to simplifying the process of generating HTML elements in views, but they serve different purposes.

Microsoft provides many built-in Tag Helpers objects to boost our development. In the below table, there is a list of available built-it Tag Helpers in ASP.NET Core.

The Anchor Tag Helpers extend the standard HTML anchor tag (<a>..</a>) which provides some new attributes. By standard convention, the attribute name must start with asp-. This helper object contains the following attributes –
- <b>asp-controller</b> - This attribute assigns the controller name which is used for generating the URL.
- <b>asp-action</b> - This attribute is used to specify the controller action method name. If no value is assigned against this attribute name, then the default asp-action value in the controller will execute to render the view.
- <b>asp-route-{value}</b> - This attribute enables a wildcard-based route prefix. Any value specified in the {value} placeholder is interpreted as a route parameter.

1. <b>Anchor Tag Helpers</b>
- <b>asp-route</b> - This attribute is used for creating a direct URL linking to an actual route value.
- <b>asp-all-route-data</b> - It supports to creation of a dictionary of key-value pairs. The key is the parameter name and the value is the parameter value.
- <b>asp-fragment</b> - This attribute specifies a URL fragment section to append the URL.
- <b>asp-area</b> - This attribute name sets the area name used in the actual route.
- <b>asp-protocol</b> - This attribute is used to specify the protocol value like https in the URL.
- <b>asp-host</b> - It is used to specify the hostname in the URL.
- <b>asp-page</b> - This attribute is needed to be used with the Razor Pages.

```
<a asp-controller="Student" asp-action="Index" 
asp-route-id="@Model.Id"> StudentId: @Model.StudentId </a> >Student List</a>
```

2. <b>Cache Tag Helpers</b>

This Tag Helper object provides the ability to catch the application content within the ASP.NET core cache provider. These helper objects increase the performance of the application. The default cached date time is set to twenty minutes by the Razor engine. The attributes of this Tag Helpers –

- <b>enabled</b> - This attribute decides whether the content within the Cache Tag Helper is cached or not. The default value is true.
- <b>expires-on</b> - This attribute is used to set the absolute expiration date for the cached data.
- <b>expires-after</b> - This attribute sets the length of time from the first request to the cached content.
- <b>expires-sliding</b> - This attribute sets the time of the cached entity so that after that time that entity can be deleted if not accessed.

```
 <cache enabled="true">
 Last Cached Time: @DateTime.Now
</cache>
```

3. <b>Form Tag Helpers</b>
- This Tag Helpers method helps us to work with Form and HTML elements within a Form. The Form Tag Helpers can do -
- It can generate the same form action attribute that we normally use for the MVC Controller to provide an action name.
Generate a hidden token to present cross-origin forgery.
```
 <form asp-controller="Demo" asp-action="Save " method="post">

</form>
```
4. <b>Input Tag Helpers</b>
his Tag helper is used for the HTML input element to display model data in our Razor view. These helper objects do the following –
- asp-for attribute normally populate id and name of the specified HTML attribute for the display expression name.
- It can assign attribute value based on the model data to the HTML attribute
- It supports HTML 5-based validation attributes related to the model data annotations

```
 @model Login
<form asp-controller="Demo" asp-action="Register" method="post">
Provide Email: <input asp-for="Email" /> 
Provide Password: <input asp-for="Password" />
<button type="submit">SignUp</button>
</form>

```

5. <b>Label Tag Helpers</b>
 
This Tag Helper extends the label tag of the HTML. It populates the caption of the label and attributes against the expression name. 
This Helper Object provides advantages like -
- We automatically retrieve the label description from the Display attribute.
- It reduced the code volume of the markup.
- It also can be used as a strong type for the model property.

```
 <form asp-controller="Demo" asp-action="Register" method="post">
<label asp-for="Email">Email Address</label>
<input asp-for="Email" /> 
</form>
```


6. <b>Select Tag Helpers</b>
This Tag Helpers populate <select> tag of HTML and also associated option elements for the properties of the error. The asp-for attribute of this tag helps is used to mention the model property name of the select element. Similarly, asp-items specify the option element.

```
 <select asp-for="Country" asp-items="Model.Countries"></select>
```

7. <b> Validate Tag Helpers</b>
It is used to show the validation message in our view model.
```
<span asp-validation-for="Username" class="text-danger"></span>
```