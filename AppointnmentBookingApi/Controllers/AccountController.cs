using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AppointmentBookingApi.Models;
using AppointmentBookingApi.Data;

namespace AppointmentBookingApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AccountController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(User user)
        {
            var existingUser = _context.Users.SingleOrDefault(u => u.Email == user.Email);
            if (existingUser != null)
            {
                return BadRequest("User already exists.");
            }

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Ok(user);
        }

        [HttpPost("login")]
        public IActionResult Login(User user)
        {
            var existingUser = _context.Users.SingleOrDefault(u => u.Email == user.Email && u.Password == user.Password);
            if (existingUser == null)
            {
                return Unauthorized("Invalid credentials.");
            }

            return Ok("Login successful.");
        }
    }
}
