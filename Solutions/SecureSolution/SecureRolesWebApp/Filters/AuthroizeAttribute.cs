using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using SecureWebApp.Entities;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class AuthorizeAttribute : Attribute, IAuthorizationFilter
{
    public string Role { get; set; }

    public void OnAuthorization(AuthorizationFilterContext context)
    {

        

        var user = (User)context.HttpContext.Items["User"];
        var roleToken = (string)context.HttpContext.Items["Role"];

        if (user == null)
        {
            // not logged in

            context.Result = new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
        }
        else
        {
            if (Role != roleToken)
            {
                // not logged in

                context.Result = new JsonResult(new { message = "Unauthorized Access :Role "+roleToken }) { StatusCode = StatusCodes.Status401Unauthorized };
            }


        }
    }
}