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
    public class UserUpdateController : UserController
    {
        public UserUpdateController(IUserInterface userService) : base(userService) { }

        /// <summary>
        /// Update a user.
        /// </summary>
        [HttpPut]
        [SwaggerOperation(Summary = "Update a user")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Update([FromBody] UserDto userDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var existingUser = await _userService.GetById(userDto.Id);
                if (existingUser == null)
                    return NotFound("User not found.");

                await _userService.Update(userDto);
                return Ok("User updated successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
    }
}
