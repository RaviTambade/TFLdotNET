using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using AssessmentPortal.Models;
using System.Text.Json.Nodes;
using System.ComponentModel.Design;
using System.Reflection;
using AssessmentPortal.Models;
using System.Buffers;
using System.Reflection.PortableExecutable;

namespace AssessmentPortal.Controllers;

public class AuthController : Controller
{
    public AuthController()
    {
        
    }
    public IActionResult Login()
    {
        return View();
    }

    public IActionResult Register()
    {
      return View();  
    }

}
