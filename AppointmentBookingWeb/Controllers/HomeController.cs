using Microsoft.AspNetCore.Mvc;

namespace AppointmentBookingWeb.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("UserEmail") == null)
            {
                return RedirectToAction("Login", "Account");
            }
            return View();
        }

        public IActionResult Privacy()
        {
            if (HttpContext.Session.GetString("UserEmail") == null)
            {
                return RedirectToAction("Login", "Account");
            }
            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
