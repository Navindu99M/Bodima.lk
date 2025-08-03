using bodimabackend.Models.DTOs;
using bodimabackend.Models;
using bodimabackend.Services;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;
using bodimabackend.Helpers;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace bodimabackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly JwtTokenGenerator _jwtTokenGenerator;

        //Old part--------------
        //public AuthController(IUserService userService)
        //{
        //    _userService = userService;
        //}---------------------
        //New Part--------------
        public AuthController(IUserService userService, JwtTokenGenerator jwtTokenGenerator)
        {
            _userService = userService;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        // ---------------------------
        // 1. Register Endpoint
        // ---------------------------

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest model)
        {
            var existingUser = await _userService.GetByEmailAsync(model.Email);
            if (existingUser != null)
                return BadRequest("Email already registered.");

            var user = new User
            {
                FullName = model.FullName,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                Role = model.Role,
                PasswordHash = HashPassword(model.Password)
            };

            var createdUser = await _userService.RegisterAsync(user);
            return Ok(new { message = "Registration successful", userId = createdUser.UserId });
        }

        // ---------------------------
        // 2. Login Endpoint
        // ---------------------------

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest model)
        {
            var user = await _userService.GetByEmailAsync(model.Email);
            //if (user == null)
            //    return Unauthorized("Invalid credentials.");

            //if (user.PasswordHash != HashPassword(model.Password))
            //    return Unauthorized("Invalid credentials.");
            if (user == null || user.PasswordHash != HashPassword(model.Password))
                return Unauthorized("Invalid credentials.");

            var token = _jwtTokenGenerator.GenerateToken(user);

            // 🔐 JWT not added yet — just return user info for now
            return Ok(new
            {
                message = "Login successful",
                token,
                user = new
                {
                    user.UserId,
                    user.FullName,
                    user.Email,
                    user.Role
                }
            });
        }

        // ---------------------------
        // Password Hashing
        // ---------------------------

        private string HashPassword(string password)
        {
            using var sha = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(password);
            var hash = sha.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }

        //[Authorize(Roles = "Landlord")]
        //[HttpGet("my-properties")]
        //public IActionResult GetMyProperties()
        //{
        //    // Only accessible if JWT is valid and role is "Landlord"
        //    return GetMyProperties();
        //}
    }


    [ApiController]
    [Route("api/[controller]")]
    public class PropertyController : ControllerBase
    {
        private readonly IPropertyService _propertyService;

        public PropertyController(IPropertyService propertyService)
        {
            _propertyService = propertyService;
        }

        [Authorize(Roles = "Landlord")]
        [HttpGet("my-properties")]
        public async Task<IActionResult> GetMyProperties()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId == null)
                return Unauthorized();

            var properties = await _propertyService.GetPropertiesByOwnerIdAsync(int.Parse(userId));
            return Ok(properties);
        }

        [Authorize(Roles = "Landlord")]
        [HttpPost]
        public async Task<IActionResult> CreateProperty([FromBody] CreatePropertyRequest request)
        {
            // Validate input
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
                return Unauthorized();

            // Create a new property object
            var newProperty = new Property
            {
                Title = request.Title,
                Description = request.Description,
                Location = request.Address,
                PricePerMonth = request.Price,
                OwnerId = int.Parse(userId), // automatically assign owner
                //CreatedAt = DateTime.UtcNow // optional
            };

            // Save property
            var createdProperty = await _propertyService.AddPropertyAsync(newProperty);

            return CreatedAtAction(nameof(GetMyProperties), new { id = createdProperty.PropertyId }, createdProperty);
        }
    }
}
