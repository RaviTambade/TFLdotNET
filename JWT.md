# Securing ASP.NET Web API using JWT Token based Authentication

Authentication is a crucial aspect of web application security. It ensures that users are who they claim to be before granting them access to specific resources or functionalities. One popular method of authentication in modern web development is JSON Web Tokens (JWT). 

## JSON Web Token (JWT)

Imagine being at a big music fetival with lot of stages and fun things to do. As soon as you get there, they give you a special wristband. This wristband is like your key to everything at the festival- Security guards at each stage simply look at your wristband to know if you can enter.They don't  need to call the ticket office or check a list.
This is similar to how JSON Web Token (JWT) function in the world of web programming.J

JWTs are like digital wristbands for online services.In the digital world, when you log into a website or app, it needs a way to remember  that you are authenticate (Like having a ticket to the festival);Without JWT, you would have to log in again every time your switch pages or request data.That would be like going to the ticket boot every time you want to enter a new stage at the festival-not practical.

JWT is a URL-safe, compack string for tranferring claims between two parties, made of three dot-sparated parts:
1. Header (token type and encrytption method).
2. Payload (user data and info)
3. Signature (verifies token integrity).

## Why JWT Authentication ?
JWT is a compact, URL-safe means of representing claims to be transferred between two parties. These claims can be digitally signed, making it a secure way to authenticate and transmit information between the client and server. JWT tokens are often used for:

1. <b>Stateless Authentication</b>: JWTs are self-contained and can store user information, reducing the need for server-side storage or session management.
2. <b>Cross-Origin Authentication</b>: They can be easily used in single-page applications (SPAs) and mobile apps due to their compact format.
3. <b>Scalability</b>: JWTs can be easily distributed and validated across multiple servers, making them ideal for microservices architectures.


## Steps for implementing ASP.NET Core Web API
When a user logsin, the server issures a JWT.
This token is stored by the users browser  and sent back with each request to the server. Like the festival wirstbandm it quickly proves the users identity and access rights , elimninating the need for repeated logins.



#### Step 1: Create a .NET Core 6 Web API Project

```
    using Microsoft.AspNetCore.Mvc;

    namespace SecureWebApp.Controllers
    {
        [Route("api/[controller]")]
        [ApiController]
        public class HelloWorldController : ControllerBase
        {
            [HttpGet]
            public IActionResult Get() 
            {
                return Ok("Hello World");
            }
        }
    }
```
After running the application we can hit the endpoint using postman and we see 200 response.

#### Step 2: Install Required NuGet Packages

You’ll need some NuGet packages to handle JWT authentication. Open your project’s .csproj file and add the following package references:
```
    <PackageReference Include="Microsoft.AspNetCore.Authentication.JwtBearer" Version="6.0.0" />
    <PackageReference Include="System.IdentityModel.Tokens.Jwt" Version="6.15.0" />
```

Save the changes and restore the packages.
You can also install these packages using Nuget UI or dotnet CLI.

#### Step 3: Configure JWT Authentication

In appsettings.json add JWT token details.

 ```
    "Jwt": {
        "Key": "TFLNonDisclouserKey",
        "Issuer": "transflower.in"
    }
```

In your Program.cs file, configure JWT authentication

```

    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.IdentityModel.Tokens;
    using System.Text;

    var builder = WebApplication.CreateBuilder(args);

    //Jwt configuration starts here
    var jwtIssuer = builder.Configuration.GetSection("Jwt:Issuer").Get<string>();
    var jwtKey = builder.Configuration.GetSection("Jwt:Key").Get<string>();

    builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtIssuer,
            ValidAudience = jwtIssuer,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
        };
    });

    //Jwt configuration ends here

    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    var app = builder.Build();

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();

    app.UseAuthentication();
    app.UseAuthorization();

    app.MapControllers();
    app.Run();
```

Step 4: Generate JWT Tokens
We will create JWT tokens when a user logs in.
We are creating another controller for login activities

```
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.IdentityModel.Tokens;
    using System.IdentityModel.Tokens.Jwt;
    using System.Text;

    namespace SecureWebApp.Controllers
    {
        [Route("api/[controller]")]
        [ApiController]
        public class LoginController : ControllerBase
        {
            private IConfiguration _config;
            public LoginController(IConfiguration config) 
            {
                _config = config;
            }

            [HttpPost]
            public IActionResult Post([FromBody] LoginRequest loginRequest)
            {
                //your logic for login process
                //If login usrename and password are correct then proceed to generate token

                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                var Sectoken = new JwtSecurityToken(_config["Jwt:Issuer"],
                _config["Jwt:Issuer"],
                null,
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: credentials);
                var token =  new JwtSecurityTokenHandler().WriteToken(Sectoken);
                return Ok(token);
            }
        }
    }
```


### Step 5: Protect API Endpoints
Now that you have configured JWT authentication, you can protect your API endpoints by applying the [Authorize] attribute to controllers or actions that require authentication.

```
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class HelloWorldController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get() 
        {
            return Ok("Hello World");
        }
    }

```

Since now we have added [Authorize] in the controller, when we call HelloWorld endpoint we get an 401 response. It means we are not authorized to access this endpoint without valid token. So lets generate a new token. Now we can call login API to get the token which we can use for authentication
<img src ="~/images/"/>


Implementing JWT token authentication in a .NET Core  application is a powerful way to secure your APIs. It allows you to create stateless, scalable, and cross-origin authentication systems, making it ideal for modern web development.
