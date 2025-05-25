using Microsoft.AspNetCore.Mvc;
using ECILWebApp.Data;
using ECILWebApp.Models;
using System.Linq;

namespace ECILWebApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public UsersController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] User loginData)
        {
            var user = _context.Users
                .FirstOrDefault(u => u.Username == loginData.Username && u.Password == loginData.Password);

            if (user == null)
                return Unauthorized("Invalid credentials.");

            return Ok(new
            {
                Message = "Login successful",
                Role = user.Role
            });
        }

        // Optional: Add seed data method for testing
        [HttpPost("seed")]
        public IActionResult SeedUsers()
        {
            if (!_context.Users.Any())
            {
                _context.Users.AddRange(
                    new User { Username = "user1", Password = "pass123", Role = "NormalUser" },
                    new User { Username = "champion1", Password = "pass456", Role = "ITChampion" },
                    new User { Username = "hod1", Password = "pass789", Role = "HOD" }
                );
                _context.SaveChanges();
                return Ok("Seeded successfully.");
            }
            return BadRequest("Users already exist.");
        }
    }
}