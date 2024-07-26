using Aspnet_mvc.Filters;
using Aspnet_mvc.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Aspnet_mvc.Controllers;

[AuthUserFilter]
public class HomeController : Controller
{
    public IActionResult Index()
    {
        HomeModel home = new()
        {
            Nome = "Serge Gogo",
            Email = "serge@gmail.com"
        };

        return View(home);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
