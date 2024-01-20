
# MVC:Views
In an ASP.NET Core MVC application, presentation logic is implemneted in page is known as a view. In ASP.NET MVC application, all incoming browser requests are handled by the controller and these requests are mapped to the controller actions. A controller action might return a view or it might also perform some other type of action such as redirecting to another controller action. With the MVC framework, the most popular method for creating HTML is to use the Razor view engine of ASP.NET MVC.

To use this view engine, a controller action produces a ViewResult object, and a ViewResult can carry the name of the Razor view that we want to use.

- Views by default in a C# ASP.NET project are files that have a *.cshtml extension and the views follow a specific convention. By default, all views live in a Views folder in the project.
- The view location and the view file name will be derived by ASP.NET MVC if you don't give it any additional information.
- If we need to render a view from the Index action of the HomeController, the first place that the MVC framework will look for that view is inside the Views folder.
- It will go into a Home folder and then look for a file called Index.cshtml − the file name starts with Index because we are in the Index action.
- The MVC framework will also look in a Shared folder and views that you place inside the Shared folder, you can use them anywhere in the application.

Let us consider Simple HomeController.

```
using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;

namespace MvcMovie.Controllers;
public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
    public IActionResult Welcome(string name, int numTimes = 1)
    {
        ViewData["Message"] = "Hello " + name;
        ViewData["NumTimes"] = numTimes;
        return View();
    }
}
```


welcome.cshtml file (Welcome View)


```
@{
    ViewData["Title"] = "Welcome";
}

<h2>Welcome</h2>

<ul>
    @for (int i = 0; i < (int)ViewData["NumTimes"]!; i++)
    {
        <li>@ViewData["Message"]</li>
    }
</ul>
```
The ViewData dictionary object contains data that will be passed to the view.

