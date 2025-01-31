using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Threading.Tasks;
using TenisHolly.DTO;
using TenisHolly.Interface;
using TenisHolly.Interfaces;

namespace TenisHolly.Controllers.V1.UserControllers
{
    [ApiController]
    [Route("api/v1/users")]
    [Tags("User")]

    public class UserCreateController : UserController
    {
        public UserCreateController(IUserInterface userService) : base(userService) { }

        /// <summary>
        /// Register a new user.
        /// </summary>
        /// <param name="userDto">User data</param>
        /// <returns>Created user</returns>
        [HttpPost("register")]
        [SwaggerOperation(Summary = "Register a new user")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Register([FromBody] UserDto userDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                await _userService.Register(userDto);
                return StatusCode(201, "User registered successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
    }
}
