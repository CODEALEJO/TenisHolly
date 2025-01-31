using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Threading.Tasks;
using TenisHolly.Interface;
using TenisHolly.Interfaces;

namespace TenisHolly.Controllers.V1.UserControllers
{

    [ApiController]
    [Route("api/v1/users")]
    [Tags("User")]

    public class UserDeleteController : UserController
    {
        public UserDeleteController(IUserInterface userService) : base(userService) { }

        /// <summary>
        /// Delete a user by ID.
        /// </summary>
        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Delete a user")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var existingUser = await _userService.GetById(id);
                if (existingUser == null)
                    return NotFound("User not found.");

                await _userService.Delete(id);
                return Ok("User deleted successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
    }
}
