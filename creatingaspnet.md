#ASP.NET  First App

##Creating asp.net core app using dotnet CLI command

```
    dotnet new webapp -o aspnetcoreapp
```
The above  command creates a new web app.

The -o aspnetcoreapp parameter creates a directory named aspnetcoreapp with the source files for the app.

## Run the app using dotnet CLI command
```
    cd aspnetcoreapp
    dotnet watch run
```

The ASP.NET Core web applications are self-hosted on the internal web server called "Kestrel". So, you don't need to use IIS Express or other web server.

In this way, you can create the default ASP.NET Core MVC application to get started.


We can create standard MVC  ASP.NET Core Web App using following dotnet CLI command

```
    dotnet new mvc -o aspnetcoreapp
```

ASP.NET Core project files and folders are synchronized with physical files and folders.