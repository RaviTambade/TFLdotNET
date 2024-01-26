# ASP.NET  First App

## Creating asp.net core app using dotnet CLI command
We can create simple ASP.NET Core application using Visual studio Project template. Even using donet CLI command, asp.net core application project can be created.

Following command creates a new web app.
```
    dotnet new webapp -o aspnetcoreapp
```
The -o aspnetcoreapp parameter creates a directory named aspnetcoreapp with the source files for the app.

## Run the app using dotnet CLI command
```
    cd aspnetcoreapp
    dotnet watch run
```
The ASP.NET Core web applications are self-hosted on the internal web server called  <b>Kestrel </b>. So, you don't need to use IIS Express or other web server. In this way, you can create the default ASP.NET Core MVC application to get started. We can create standard MVC ASP.NET Core Web App using following dotnet CLI command

```
    dotnet new mvc -o aspnetcoreapp
```



Once ASP.NET Core MVC application project get created, we observe following files and folders inside Project.
Let's understand the significanse of each file and folder.
C#  code is normally ogranized in projetcts and solutions..


## Solution File
The top-level solution file (.sln) that can contains information about projects and structure of Software Solution.  Solution file acts like workspace for logically linking more than one projects .
Following code snippet represents one example of .sln file

```
Microsoft Visual Studio Solution File, Format Version 12.00
# Visual Studio Version 17
VisualStudioVersion = 17.0.31903.59
MinimumVisualStudioVersion = 10.0.40219.1
Project("{9A19103F-16F7-4668-BE54-9A1E7A4F7556}") = "EStoreWebApp", "EStoreWebApp\EStoreWebApp.csproj", "{5D345E31-5FB8-448F-82A0-B7ED2C886A81}"
EndProject
Project("{9A19103F-16F7-4668-BE54-9A1E7A4F7556}") = "BOL", "BOL\BOL.csproj", "{901C0B6E-69EA-4FD4-9B78-3C231C8E6F9F}"
EndProject
Project("{9A19103F-16F7-4668-BE54-9A1E7A4F7556}") = "BLL", "BLL\BLL.csproj", "{73791374-AC19-4123-BDEF-84A24ADEC60D}"
EndProject
Project("{9A19103F-16F7-4668-BE54-9A1E7A4F7556}") = "DAL", "DAL\DAL.csproj", "{620A5EC3-6153-4C7F-B912-3F56ACADE157}"
EndProject
Project("{9A19103F-16F7-4668-BE54-9A1E7A4F7556}") = "TestApp", "TestApp\TestApp.csproj", "{9005EC51-6D9A-4CB2-BEC4-6FFCA45E49CB}"
EndProject
Project("{FAE04EC0-301F-11D3-BF4B-00C04F79EFBC}") = "ASPXWebApp", "ASPXWebApp\ASPXWebApp.csproj", "{FB6B757F-8AD7-4674-B54F-E99C763EFC30}"
EndProject
Project("{FAE04EC0-301F-11D3-BF4B-00C04F79EFBC}") = "CatalogAPI", "CatalogAPI\CatalogAPI.csproj", "{B765DE01-58CB-4921-A9F4-1FE0133BDF27}"
EndProject
Project("{9A19103F-16F7-4668-BE54-9A1E7A4F7556}") = "EStoreWebAPI", "EStoreWebAPI\EStoreWebAPI.csproj", "{9FFA9FB6-571C-4D64-BE1B-C37B10185252}"
EndProject
Global
	GlobalSection(SolutionConfigurationPlatforms) = preSolution
		Debug|Any CPU = Debug|Any CPU
		Release|Any CPU = Release|Any CPU
	EndGlobalSection
	GlobalSection(ProjectConfigurationPlatforms) = postSolution
		{5D345E31-5FB8-448F-82A0-B7ED2C886A81}.Debug|Any CPU.ActiveCfg = Debug|Any CPU
		{5D345E31-5FB8-448F-82A0-B7ED2C886A81}.Debug|Any CPU.Build.0 = Debug|Any CPU
		{5D345E31-5FB8-448F-82A0-B7ED2C886A81}.Release|Any CPU.ActiveCfg = Release|Any CPU
		{5D345E31-5FB8-448F-82A0-B7ED2C886A81}.Release|Any CPU.Build.0 = Release|Any CPU
		{901C0B6E-69EA-4FD4-9B78-3C231C8E6F9F}.Debug|Any CPU.ActiveCfg = Debug|Any CPU
		{901C0B6E-69EA-4FD4-9B78-3C231C8E6F9F}.Debug|Any CPU.Build.0 = Debug|Any CPU
		{901C0B6E-69EA-4FD4-9B78-3C231C8E6F9F}.Release|Any CPU.ActiveCfg = Release|Any CPU
		{901C0B6E-69EA-4FD4-9B78-3C231C8E6F9F}.Release|Any CPU.Build.0 = Release|Any CPU
		{73791374-AC19-4123-BDEF-84A24ADEC60D}.Debug|Any CPU.ActiveCfg = Debug|Any CPU
		{73791374-AC19-4123-BDEF-84A24ADEC60D}.Debug|Any CPU.Build.0 = Debug|Any CPU
		{73791374-AC19-4123-BDEF-84A24ADEC60D}.Release|Any CPU.ActiveCfg = Release|Any CPU
		{73791374-AC19-4123-BDEF-84A24ADEC60D}.Release|Any CPU.Build.0 = Release|Any CPU
		{620A5EC3-6153-4C7F-B912-3F56ACADE157}.Debug|Any CPU.ActiveCfg = Debug|Any CPU
		{620A5EC3-6153-4C7F-B912-3F56ACADE157}.Debug|Any CPU.Build.0 = Debug|Any CPU
		{620A5EC3-6153-4C7F-B912-3F56ACADE157}.Release|Any CPU.ActiveCfg = Release|Any CPU
		{620A5EC3-6153-4C7F-B912-3F56ACADE157}.Release|Any CPU.Build.0 = Release|Any CPU
		{9005EC51-6D9A-4CB2-BEC4-6FFCA45E49CB}.Debug|Any CPU.ActiveCfg = Debug|Any CPU
		{9005EC51-6D9A-4CB2-BEC4-6FFCA45E49CB}.Debug|Any CPU.Build.0 = Debug|Any CPU
		{9005EC51-6D9A-4CB2-BEC4-6FFCA45E49CB}.Release|Any CPU.ActiveCfg = Release|Any CPU
		{9005EC51-6D9A-4CB2-BEC4-6FFCA45E49CB}.Release|Any CPU.Build.0 = Release|Any CPU
		{FB6B757F-8AD7-4674-B54F-E99C763EFC30}.Debug|Any CPU.ActiveCfg = Debug|Any CPU
		{FB6B757F-8AD7-4674-B54F-E99C763EFC30}.Debug|Any CPU.Build.0 = Debug|Any CPU
		{FB6B757F-8AD7-4674-B54F-E99C763EFC30}.Release|Any CPU.ActiveCfg = Release|Any CPU
		{FB6B757F-8AD7-4674-B54F-E99C763EFC30}.Release|Any CPU.Build.0 = Release|Any CPU
		{B765DE01-58CB-4921-A9F4-1FE0133BDF27}.Debug|Any CPU.ActiveCfg = Debug|Any CPU
		{B765DE01-58CB-4921-A9F4-1FE0133BDF27}.Debug|Any CPU.Build.0 = Debug|Any CPU
		{B765DE01-58CB-4921-A9F4-1FE0133BDF27}.Release|Any CPU.ActiveCfg = Release|Any CPU
		{B765DE01-58CB-4921-A9F4-1FE0133BDF27}.Release|Any CPU.Build.0 = Release|Any CPU
		{9FFA9FB6-571C-4D64-BE1B-C37B10185252}.Debug|Any CPU.ActiveCfg = Debug|Any CPU
		{9FFA9FB6-571C-4D64-BE1B-C37B10185252}.Debug|Any CPU.Build.0 = Debug|Any CPU
		{9FFA9FB6-571C-4D64-BE1B-C37B10185252}.Release|Any CPU.ActiveCfg = Release|Any CPU
		{9FFA9FB6-571C-4D64-BE1B-C37B10185252}.Release|Any CPU.Build.0 = Release|Any CPU
	EndGlobalSection
	GlobalSection(SolutionProperties) = preSolution
		HideSolutionNode = FALSE
	EndGlobalSection
	GlobalSection(ExtensibilityGlobals) = postSolution
		SolutionGuid = {A91399B6-3E92-4A68-B63B-A85D4AABDE6B}
	EndGlobalSection
EndGlobal
```


## Project File
.csproj file tells dotnet how to build the ASP.NET application. It's one of the most important files in an ASP.NET project. An ASP.NET project can depend on third-party libraries developed by other developers. Usually, these libraries are installed as Nuget packages using Nuget package manager.

Following code snippet represents example of C# Project file.

```
<Project Sdk="Microsoft.NET.Sdk.Web">

  <PropertyGroup>
    <TargetFramework>net8.0</TargetFramework>
    <Nullable>enable</Nullable>
    <ImplicitUsings>enable</ImplicitUsings>
  </PropertyGroup>

  <ItemGroup>
    <PackageReference Include="Microsoft.AspNetCore.Cors" Version="2.2.0" />
    <PackageReference Include="Microsoft.AspNetCore.OpenApi" Version="7.0.1" />
    <PackageReference Include="Swashbuckle.AspNetCore" Version="6.4.0" />
  </ItemGroup>

</Project>

```

## Dependencies

The Dependencies node contains the list of all the dependencies that your project relies on, including NuGet packages, project references, and framework dependencies.
```
<ItemGroup>
    <PackageReference Include="Microsoft.AspNetCore.Cors" Version="2.2.0" />
    <PackageReference Include="Microsoft.AspNetCore.OpenApi" Version="7.0.1" />
    <PackageReference Include="Swashbuckle.AspNetCore" Version="6.4.0" />
  </ItemGroup>

</Project>

```

## wwwroot folder
By default, the wwwroot folder in the ASP.NET Core project is treated as a web root folder. Static files can be stored in any folder under the web root and accessed with a relative path to that root.

All the css, JavaScript, and external library files should be stored here which are being reference in the HTML file.


## Controllers, Models, Views folders
The Controllers, Models, and Views folders include controller classes, model classes and cshtml or vbhtml files respectively for MVC application.

## appsettings.json
The appsettings.json file is a configuration file commonly used in .NET applications, including ASP.NET Core and ASP.NET 5/6, to store application-specific configuration settings and parameters. It allows developers to use JSON format for the configurations instead of code, which makes it easier to add or update settings without modifying the application's source code.

