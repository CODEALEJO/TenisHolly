using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TenisHolly.DTOs;
using TenisHolly.Interfaces;

namespace TenisHolly.Controllers.V1.ShoeControllers
{
    [ApiController]
    [Route("api/v1/shoes")]
    [Tags("Shoe")]

    public class ShoeReadController : ShoeController
    {
        public ShoeReadController(IShoeInterface shoeInterface) : base(shoeInterface) { }

        /// <summary>
        /// Get all shoes in the inventory.
        /// </summary>
        /// <returns>A list of all shoes.</returns>
        /// <response code="200">Returns a list of all shoes.</response>
        /// <response code="500">Internal server error.</response>
        [HttpGet("all")]
        [SwaggerOperation(Summary = "Get all shoes", Description = "Fetches all shoes from the inventory.")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetAllShoesAsync()
        {
            try
            {
                var shoes = await _shoeInterface.GetAllShoesAsync();
                return Ok(shoes);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        /// <summary>
        /// Get a shoe by its ID.
        /// </summary>
        /// <param name="id">The ID of the shoe.</param>
        /// <returns>The details of the shoe.</returns>
        /// <response code="200">Returns the shoe details.</response>
        /// <response code="404">Shoe not found.</response>
        /// <response code="500">Internal server error.</response>
        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Get shoe by ID", Description = "Fetches the details of a shoe by its ID.")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetShoeByIdAsync(int id)
        {
            try
            {
                var shoe = await _shoeInterface.GetShoeByIdAsync(id);
                return Ok(shoe);
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"Shoe with ID {id} not found.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        /// <summary>
        /// Get shoes filtered by gender, size, or reference.
        /// </summary>
        /// <param name="gender">The gender of the shoes (optional).</param>
        /// <param name="size">The size of the shoes (optional).</param>
        /// <param name="reference">The reference of the shoes (optional).</param>
        /// <returns>A list of shoes matching the filters.</returns>
        /// <response code="200">Returns a list of filtered shoes.</response>
        /// <response code="500">Internal server error.</response>
        [HttpGet("filter")]
        [SwaggerOperation(Summary = "Get shoes by filters", Description = "Fetches shoes from the inventory filtered by gender, size, or reference.")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetShoesByFiltersAsync([FromQuery] string? gender, [FromQuery] string? size, [FromQuery] string? reference)
        {
            try
            {
                var shoes = await _shoeInterface.GetShoesByFiltersAsync(gender, size, reference);
                return Ok(shoes);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
    }
}
