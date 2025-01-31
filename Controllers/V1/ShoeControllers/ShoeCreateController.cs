using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TenisHolly.DTOs;
using TenisHolly.Interfaces;

namespace TenisHolly.Controllers.V1.ShoeControllers
{
    [ApiController]
    [Route("api/v1/shoes")]
    [Tags("Shoe")]
    public class ShoeCreateController : ShoeController
    {
        public ShoeCreateController(IShoeInterface shoeInterface) : base(shoeInterface) { }

        /// <summary>
        /// Add a new shoe to the inventory.
        /// </summary>
        /// <param name="shoeDto">The details of the shoe to add.</param>
        /// <returns>A 201 status code if successful, 400 for invalid data, or 500 for internal server errors.</returns>
        /// <response code="201">Shoe added successfully.</response>
        /// <response code="400">Invalid input data.</response>
        /// <response code="500">Internal server error.</response>
        [HttpPost]
        [SwaggerOperation(Summary = "Add a new shoe", Description = "Creates a new shoe record in the inventory.")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> AddShoeAsync([FromBody] ShoeDTO shoeDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                await _shoeInterface.AddShoeAsync(shoeDto);
                return StatusCode(201, "Shoe added successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
    }
}
