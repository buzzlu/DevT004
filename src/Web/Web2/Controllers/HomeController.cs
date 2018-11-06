using Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using Web.Models;

namespace Web.Controllers
{

    [Authorize]
    public class HomeController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ILogger<HomeController> _logger;

        public HomeController(UserManager<ApplicationUser> userManager, ILogger<HomeController> logger)
        {
            this.userManager = userManager;
            _logger = logger;
        }
        [Authorize(Roles = "User")]
        public IActionResult Index()
        {
            string userName = userManager.GetUserName(User);

            string logInfo = userName + "Login on";
            _logger.LogInformation(logInfo);

            return View("Index", userName);
        }
    }

    //public class HomeController : Controller
    //{
    //    private readonly ILogger<HomeController> _logger;

    //    public HomeController(ILogger<HomeController> logger)
    //    {
    //        _logger = logger;
    //    }

    //    public IActionResult Index()
    //    {
    //        _logger.LogInformation("Index page says hello");
    //        return View();
    //    }

    //    public IActionResult About()
    //    {
    //        ViewData["Message"] = "Your application description page.";

    //        return View();
    //    }

    //    public IActionResult Contact()
    //    {
    //        ViewData["Message"] = "Your contact page.";

    //        return View();
    //    }

    //    public IActionResult Privacy()
    //    {
    //        return View();
    //    }

    //    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    //    public IActionResult Error()
    //    {
    //        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    //    }
    //}
}
