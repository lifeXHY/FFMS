using System.Diagnostics;
using FFMS.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace FFMS.Web.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        //private readonly ISession _session;
        public HomeController()
        {

        }

        
        public IActionResult Index()
        {
            //if(_session.)
            return View();
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
}
