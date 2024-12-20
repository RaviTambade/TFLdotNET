
## Middleware Pipeline

When you create a new project using one of the built-in templates, your project is already supplied with a few calls to add/configure middleware services and then use them. This is accomplished by adding the calls to the Program.cs file.

Request Received at Web Application level. 
It is  goes throgh Middleware Layer.

```
	1.Exception Handling
	2.HSTS Protocol
	3.HTTP Redirect
	4.Static Files
	5.Cookie Policy
	6.Auth
	7.Session
	8.MVC
	Reverse
Response is sent back 

```

## HTTP Pipeline

When an HTTP request comes in, the first request delegate handles that request. It can either pass the request down to the next in line or short-circuit the pipeline by preventing the request from propagating further. This is use very useful across multiple scenarios, e.g. serving static files without the need for authentication, handling exceptions before anything else, etc.
The returned response travels back in the reverse direction back through the pipeline. This allows each component to run code both times: when the request arrives and also when the response is on its way out.


## Built-In Middleware

The information below explains how the built-in middleware works, and why the order is important. 
The UseXYZ() methods are merely extension methods that are prefixed with the word “Use” as a useful convention, 
making it easy to discover Middleware components when typing code. 
 
1. Exception Handling:
2. HSTS & HTTPS Redirection:
3. Static Files:
4. Cookie Policy:
5. Authentication, Authorization & Sessions:
6. MVC & Routing:

### 1.Exception Handling:

- <b>UseDeveloperExceptionPage()</b> & <b>UseDatabaseErrorPage()</b>: used in development to catch run-time exceptions
<b>UseExceptionHandler()</b>: used in production for run-time exceptions
Calling these methods first ensures that exceptions are caught in any of the middleware components that follow. 

### 2.HSTS & HTTPS Redirection:

- <b>UseHsts() </b>: used in production to enable HSTS (HTTP Strict Transport Security Protocol) and enforce HTTPS.
UseHttpsRedirection(): forces HTTP calls to automatically redirect to equivalent HTTPS addresses.
Calling these methods next ensure that HTTPS can be enforced before resources are served from a web browser.

### 3.Static Files:
- <b>UseStaticFiles()</b>:used to enable static files, such as HTML, JavaScript, CSS and graphics files. 
			  	Called early on to avoid the need for authentication, session or MVC middleware.
Calling this before authentication ensures that static files can be served quickly without unnecessarily triggering authentication middleware. 

### 4.Cookie Policy:

- <b>UseCookiePolicy()</b>: used to enforce cookie policy and display GDPR-friendly messaging
Calling this before the next set of middleware ensures that the calls that follow can make use of cookies if consented. 


### 5.Authentication, Authorization & Sessions:

- <b>UseAuthentication()</b>: used to enable authentication and then subsequently allow authorization.
- <b>UseSession()</b>: manually added to the Startup file to enable the Session middleware.
Calling these after cookie authentication (but before the MVC middleware) ensures that cookies can be issued as necessary and 
that the user can be authenticated before the MVC engine kicks in. 

### 6.MVC & Routing:
- <b>UseMvc()</b>:enables the use of MVC in your web application, with the ability to customize routes for your MVC application 
		and set other options.
- <b>routes.MapRoute()</b>: 
	set the default route and any custom routes when using MVC.

## Routing

Routing is a pattern matching system that monitors the incoming request and figures out what to do with that request.
Typically, it is a way to serve the user request.
When a user request URLs from the server then URLs are handled by the routing system. 
The Routing system try to find out the matching route pattern of requeted Url with already registered routes which are map to controller, actions, files, or other items.
If there is a matching route entry, then it process the request i.e. serve the resource, otherwise it returns 404 error.

## Types of Routing

There are two main ways to define routes in ASP.NET Core:
1. Convention-based Routing
2. Attribute Routing

## 1.Convention-based Routing
It creates routes based on a series of conventions which represent all the possible routes in your system.
Convention-based are defined in the Startup.cs file.

## 2.Attribute Routing
It creates routes based on attributes placed on controller or action level. 
Attribute routing provides us more control over the URLs generation patterns which helps us in SEO.

#### Attribute Routing Tokens
One of the cool thing about ASP.NET Core routing is it's more flexible as compared to ASP.NET MVC5 routing 
since it provides tokens for [area], [controller], and [action]. These tokens get replaced by their values in the route table.

### Mixed Routing
You can use Convention-based Routing and Attribute routing together. Even you should use both together
since it's not possible to define attribute route for each and every action or controller. 
In that case, Convention-based Routing will help you.

### Route Constraints
Route Constraints are used to restrict the type of passed value to an action.
For example, if you expect an argument id as an integer type, 
then you have to restrict it to an integer type by using datatype in the curly brackets as {id:int}.

#### Optional Parameters
You can define your route parameter as optional in routes by adding a question mark (?) to the parameter's constraint as given below:

```
app.UseMvc(routes =>
{
 routes.MapRoute(
 template: "{controller}/{action}/{id:int?}");
});
```

### Default Values
In addition to route constraints and optional parameters, 
you can also specify the default values for your route parameters 
which will be used if values are not provided.

```
app.UseMvc(routes =>
{
 routes.MapRoute(
 template: "{controller=Home}/{action=Index}/{id:int?}");
});
```