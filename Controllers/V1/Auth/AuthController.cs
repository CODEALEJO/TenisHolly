
using TenisHolly.Config;
using TenisHolly.Data;
using TenisHolly.DTO;
using TenisHolly.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;

namespace Assesment_Alejandro_Castrillon_Gomez_bernslee.Controllers.V1.Auth
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly Utilities _utilities;

        public AuthController(ApplicationDbContext context, Utilities utilities)
        {
            _context = context;
            _utilities = utilities;
        }
        [HttpPost("/api/v1/auth/register")]
        [SwaggerOperation(
            Summary = "Register a new User",
            Description = "Creates a new user record after validating the provided information and ensuring the email is not already in use."
        )]
        public async Task<IActionResult> Register([FromBody] RegisterUserDto registerUserDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Verificar si el correo electrónico ya existe
            if (_context.Users.Any(u => u.Email == registerUserDto.Email))
            {
                return BadRequest("Email already exists");
            }

            // Validar la longitud de la contraseña
            if (registerUserDto.PasswordHash.Length < 8)
            {
                return BadRequest("Password must be at least 8 characters long.");
            }

            // Validar que el rol sea uno de los roles válidos
            var validRoles = new List<string> { "Admin", "Patient", "Doctor" };
            if (!validRoles.Contains(registerUserDto.Role))
            {
                return BadRequest("Invalid role. Valid roles are Admin, Patient, Doctor.");
            }

            // Encriptar la contraseña usando BCrypt
            var passwordHash = BCrypt.Net.BCrypt.HashPassword(registerUserDto.PasswordHash);

            var user = new User
            {
                FullName = registerUserDto.FullName,
                Email = registerUserDto.Email,
                PasswordHash = passwordHash,
                Role = registerUserDto.Role // Asignar el rol enviado en el DTO
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Ok("User registered successfully");
        }

        // Login endpoint
        [SwaggerOperation(
            Summary = "User Login",
            Description = "Authenticates a user by validating the provided credentials (email and password). Returns a JWT token upon successful authentication."
        )]
        [HttpPost("/api/v1/auth/login")]
        public async Task<IActionResult> Login([FromBody] LoginUserDto loginUserDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == loginUserDto.Email);
            if (user == null)
            {
                return Unauthorized("Invalid email");
            }

            var passwordIsValid = BCrypt.Net.BCrypt.Verify(loginUserDto.PasswordHash, user.PasswordHash);
            if (!passwordIsValid)
            {
                return Unauthorized("Invalid password");
            }

            // Validar que las propiedades necesarias del usuario no sean nulas
            if (string.IsNullOrEmpty(user.Email) || string.IsNullOrEmpty(user.PasswordHash))
            {
                return BadRequest("User data is incomplete.");
            }

            var token = _utilities.GenerateJwtToken(user);
            return Ok(new
            {
                message = "Please store this token:",
                jwt = token
            });
        }

    }
}