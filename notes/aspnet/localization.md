
# Localization

Making websites multi-lingual allows users from around the globe to use our applications in their native language. ASP.NET Core provides us with the ability to enable localization to support different languages and cultures. 


Globalization is the process of designing applications to support different languages and cultures.

Localization, on the other hand, is the process of adjusting an application for a specific language/culture


### Resource Files
Resource files are a specific file format (.resx) that allows us to define translations for any plaintext content we want to display in our application, in a key-value pair format. They allow us to separate the localized content from our code, meaning we can easily swap out different resource files for different languages without having to change our code.

### File Naming Convention
By convention, resource files should be separated into a separate folder called Resources. When naming these files, they should follow the name of the class that will consume them, as well as include the language they represent.

We can either organize the files into separate folders, such as 
```
Resources/Controllers.HomeController.es.resx
Resources/Controllers.HomeController.en.resx
```


## Configure Supported Cultures

Before we can test out the localization of the application, we need to configure our application to register the required services. In the Program class, let’s add localization to our application:

```
builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");

```

We need to set the ResourcesPath property to our Resources folder, so ASP.NET Core knows where to look for our resource files.

Also, we can configure the supported cultures for our application:

```
    const string defaultCulture = "en-GB";

    var supportedCultures = new[]
    {
        new CultureInfo(defaultCulture),
        new CultureInfo("es")
    };

    builder.Services.Configure<RequestLocalizationOptions>(options => {
        options.DefaultRequestCulture = new RequestCulture(defaultCulture);
        options.SupportedCultures = supportedCultures;
        options.SupportedUICultures = supportedCultures;
    });

```

Here, we define our default culture (en-GB) as well as another supported culture (es).

Finally, we need to tell our application to use these supported cultures:

```
app.UseRequestLocalization(app.Services.GetRequiredService<IOptions<RequestLocalizationOptions>>().Value);

```


### IStringLocalizer Interface for Localization
ASP.NET Core provides us with an easy-to-use interface for making our applications localized, IStringLocalizer<T>. This interface uses two classes, ResourceReader and ResourceManager which provides access to culture-specific resources at run-time. We can use dependency injection to gain access to this interface to make the localization of our applications much more straightforward.


### Controller Localization
Now that we have our resource files created, let’s create the accompanying controller:

```
public class HomeController : Controller
{
    private readonly IStringLocalizer<HomeController> _localizer;

    public LocalizationController(IStringLocalizer<HomeController> localizer)
    {
        _localizer = localizer;
    }

    public IActionResult Index()
    {
        ViewData["Greeting"] = _localizer["Greeting"];
        return View();
    }
}


```

We start by injecting the IStringLocalizer<HomeController> interface, which will give us access to our English and Spanish resource files.

In the Index() method, we set the ViewData dictionary value for Greeting from our resource file, which will be determined based on the culture of the user. 

In the Views folder, let’s create a new folder called Localization for our new controller, and create our Index view:

```
    @{
        ViewData["Title"] = "IStringLocalizer";
    }

    <div class="text-center">
        <p>@ViewData["Greeting"]</p>
    </div>
```

Here, we simply access the Greeting key of the ViewData dictionary.

If we run our application and navigate to /Home, given that our culture is set to English, we will see the English variation of our Greeting message.

#### Request Culture with Query String
We can manually specify the culture we wish to use by using the culture query string parameter. By default in ASP.NET Core, the QueryStringRequestCultureProvider is registered, allowing us to use this query string parameter in our application.

Let’s try it out. If we navigate to the Localization controller and pass the culture query parameter with the value set to es, we will see the Spanish variation of our greeting:

```
    http://localhost:5286/?culture=es

```

Localization is a powerful tool that enables us to reach a wider audience with our applications, providing each culture with a personalized experience suited to them.