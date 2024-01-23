# State Management
In an ASP NET application, state management in ASP NET is an object and preserves type state control. This is because ASP NET applications are basically stateless. In ASP NET, the information of users is stored and maintained till the user session ends. Each time the page is posted on the server, a new instance of the Web page class is created. Whenever the user enters information, this information might get lost in the round trip from the browser 

## Understanding state management
State management is the technique of maintaining the state of an application over time, i.e., for the duration of a user session or across all of the HTTP requests and responses that constitute the session. Thus it is one of the most important cross-cutting concerns of any web application.
State management is how you keep track of the data moving in and out of your application and how you ensure it’s available when needed. State management allows a smoother user experience by enabling users to pick up where they left off without re-entering their information. Without state management, users would have to enter their information every time they visited or reloaded a new page.
You can manage the state in several ways in an ASP.NET Core MVC application. We’ll examine six ways to handle state in the sections below: cookies, session state, hidden fields, the TempData property, query strings, and caching.

### Using cookies in ASP.NET Core MVC
Cookies store data in the user’s browser. Browsers send cookies with every request and hence their size should be kept to a minimum.
Ideally, we should only store an identifier in the cookie and we should store the corresponding data using the application. Most browsers restrict cookie size to 4096 bytes and only a limited number of cookies are available for each domain.

Users can easily tamper with or delete a cookie. Cookies can also expire on their own. 
Hence we should not use them to store sensitive information and their values should not be blindly trusted or used without proper validation.

We often use cookies to personalize the content for a known user especially when we just identify a user without authentication. 
We can use the cookie to store some basic information like the user’s name. 
Then we can use the cookie to access the user’s personalized settings, such as their preferred color theme.

A cookie is a piece of data that resides on the user’s computer that helps identify the user. In most web browsers, each cookie is saved in a separate file (the exception is Firefox, which saves all cookies in the same file). Cookies are represented as key-value pairs, and the keys can be used to read, write, or remove cookies. ASP.NET Core MVC uses cookies to preserve session state; the cookie with the session ID is transmitted to the client.

```
CookieOptions options = new CookieOptions();
options.Expires = DateTime.Now.AddSeconds(10);
```


### Using  session state in ASP.NET Core MVC
Session state is a mechanism for storing user data on the server side in an ASP.NET Core MVC web application. A user’s browser sends the server a request containing information about the user’s session every time the user visits a website. The server then creates a new session and stores the user’s data in that session.
The user’s session and all the user’s data are destroyed when they leave the website. Session state is useful for storing small amounts of data that need to be persisted across multiple requests from a single user. For example, you might use session state to store a user’s shopping cart items or preferences.

The following code snippet illustrates how you can store a key-value pair in the session state in an action method.

```
public IActionResult Index()
{
   HttpContext.Session.SetString("MyKey", "MyValue");
   return View();
}
```

### Using  hidden fields in ASP.NET Core MVC
When working on ASP.NET Core MVC applications, we may need to preserve data on the client side instead of presenting it on the page. For example, we might need to send data to the server when the user takes a certain action, without showing the data in the user interface. This is a typical problem in many applications, and hidden fields offer an excellent solution. We can store information in hidden form fields and return it in the following request.

The following code snippet illustrates how you can store the user ID of a logged in user and assign the value 1.
```
@Html.HiddenFor(x => x.UserId, new { Value = "1" })
```

### Use TempData  in ASP.NET Core MVC
You can use the TempData property in ASP.NET Core to store data until your application reads it. We can examine the data without deleting it using the Keep() and Peek() functions. TempData is extremely helpful when we need data belonging to more than one request. We can get to them using controllers and views.

TempData is used to transmit data from one request to the next, i.e., to redirect data from one page to another. It has a minimal life and only exists until the target view is entirely loaded. However, you may save data in TempData by using the Keep() function. TempData is accessible only during a user’s session. It survives until we read it and then it’s cleared after an HTTP request.

The following code snippet illustrates how you can use TempData in your ASP.NET Core MVC controller.

```
public class CustomerController : Controller
{
    public IActionResult TempDataDemo()
    {
        var customerId = TempData["CustomerId"] ?? null;       
        return View();
    }
}
```
It is meant to be a very short-lived instance, and you should only use it during the current and the subsequent requests only! Since TempData works this way, you need to know for sure what the next request will be, and redirecting to another view is the only time you can guarantee this. Therefore, the only scenario where using TempData will reliably work is when you are redirecting. This is because a redirect kills the current request (and sends HTTP status code 302 Object Moved to the client), then creates a new request on the server to serve the redirected view. Looking back at the previous HomeController code sample means that the TempData object could yield results differently than expected because the next request origin can't be guaranteed. For example, the next request can originate from a completely different machine and browser instance.


### query strings  in ASP.NET Core MVC
You can take advantage of query strings to transmit a small amount of data from one request to another. Note that because query strings are publicly exposed, you should never use them to pass sensitive data. Additionally, using query strings could make your application vulnerable to cross-site request forgery (CSRF) attacks.
The following code snippet illustrates how you can use query strings in ASP.NET Core MVC.

```
http://localhost:5655/api/customer?region=abc
```
And, the code snippet below shows how you can read the query string data in your action method.

```
string region = HttpContext.Request.Query["region"].ToString();
```

### Caching in ASP.NET Core MVC
Caching is yet another way to store state information between requests. You can leverage a cache to store stale data, i.e., data that changes infrequently in your application. ASP.NET Core MVC provides support for three different types of caching, namely in-memory caching, distributed caching, and response caching. The following code snippet shows how you can turn on in-memory caching in your ASP.NET Core MVC applications.

```
builder.Services.AddMemoryCache();
```
If you would like to store and retrieve instances of complex types in the session state, you can serialize or deserialize your data as appropriate. And if you’d like to send data from your controller to the view, you can take advantage of ViewData.

## ViewBag and ViewData 

ViewData and ViewBag are objects that can be used to pass data from the Controller to the View in an ASP.NET Core MVC application.

### ViewData
ViewData is a dictionary object that you put data into, which then becomes available to the view. ViewData is a derivative of the ViewDataDictionary class, so you can access by the familiar "key/value" syntax.

### ViewBag
The ViewBag object is a wrapper around the ViewData object that allows you to create dynamic properties for the ViewBag.
