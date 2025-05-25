using Microsoft.AspNetCore.Mvc;
using ECILWebApp.Data;
using ECILWebApp.Models;
using Microsoft.EntityFrameworkCore;

namespace ECILWebApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UsersController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginData)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Username == loginData.Username && u.Password == loginData.Password);

            if (user == null)
                return Unauthorized(new { Message = "Invalid credentials." });

            return Ok(new
            {
                Message = "Login successful",
                Role = user.Role,
                Username = user.Username,
                UserId = user.Id
            });
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }

        [HttpPost("seed")]
        public async Task<IActionResult> SeedUsers()
        {
            if (!await _context.Users.AnyAsync())
            {
                _context.Users.AddRange(
                    new User { Username = "user1", Password = "pass123", Role = "NormalUser" },
                    new User { Username = "champion1", Password = "pass456", Role = "ITChampion" },
                    new User { Username = "hod1", Password = "pass789", Role = "HOD" }
                );
                await _context.SaveChangesAsync();
                return Ok("Seeded successfully.");
            }
            return BadRequest("Users already exist.");
        }
    }

    public class LoginRequest
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}