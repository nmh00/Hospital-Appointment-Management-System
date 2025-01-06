using AppointmentBookingWeb.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace AppointmentBookingWeb.Controllers
{
    public class AccountController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public AccountController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(User user)
        {
            var client = _httpClientFactory.CreateClient();
            var json = JsonSerializer.Serialize(user);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("https://p3vsg5cb-7077.asse.devtunnels.ms/api/Account/Register/register", content);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Login");
            }

            ModelState.AddModelError(string.Empty, "Registration failed.");
            return View(user);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(User user)
        {
            var client = _httpClientFactory.CreateClient();
            var json = JsonSerializer.Serialize(user);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("https://p3vsg5cb-7077.asse.devtunnels.ms/api/Account/Login/login", content);

            if (response.IsSuccessStatusCode)
            {
                HttpContext.Session.SetString("UserEmail", user.Email);
                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError(string.Empty, "Login failed.");
            return View(user);
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Remove("UserEmail");
            return RedirectToAction("Login");
        }
    }
}
