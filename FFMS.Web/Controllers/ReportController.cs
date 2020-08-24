using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FFMS.Web.Controllers
{
    [Authorize]
    public class ReportController : Controller
    {
        public IActionResult Details()
        {
            return View();
        }
    }
}
