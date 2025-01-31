using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TenisHolly.DTO;
using TenisHolly.Interface;
using TenisHolly.Interfaces;

namespace TenisHolly.Controllers.V1.UserControllers
{

    [ApiController]
    [Route("api/v1/users")]
    [Tags("User")]
    public class UserReadController : UserController
    {
        public UserReadController(IUserInterface userService) : base(userService) { }

        /// <summary>
        /// Get all users.
        /// </summary>
        [HttpGet]
        [SwaggerOperation(Summary = "Get all users")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetAll()
        {
            try
            {
                return Ok(await _userService.GetAll());
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        /// <summary>
        /// Get a user by ID.
        /// </summary>
        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Get a user by ID")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<UserDto>> GetById(int id)
        {
            try
            {
                var user = await _userService.GetById(id);
                if (user == null)
                    return NotFound("User not found.");

                return Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
    }
}
