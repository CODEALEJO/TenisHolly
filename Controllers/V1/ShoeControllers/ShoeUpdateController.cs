using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TenisHolly.DTOs;
using TenisHolly.Interfaces;

namespace TenisHolly.Controllers.V1.ShoeControllers
{
    [ApiController]
    [Route("api/v1/shoes")]
    [Tags("Shoe")]

    public class ShoeUpdateController : ShoeController
    {
        public ShoeUpdateController(IShoeInterface shoeInterface) : base(shoeInterface) { }

        /// <summary>
        /// Update an existing shoe.
        /// </summary>
        /// <param name="id">The ID of the shoe to update.</param>
        /// <param name="updatedShoe">The updated details of the shoe.</param>
        /// <returns>A 200 status code if successful, 404 if the shoe is not found, or 500 for internal server errors.</returns>
        /// <response code="200">Shoe updated successfully.</response>
        /// <response code="404">Shoe not found.</response>
        /// <response code="400">Invalid input data.</response>
        /// <response code="500">Internal server error.</response>
        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Update an existing shoe", Description = "Updates the details of an existing shoe by its ID.")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> UpdateShoeAsync(int id, [FromBody] ShoeDTO updatedShoe)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var result = await _shoeInterface.UpdateShoeAsync(id, updatedShoe);
                return Ok(result);
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
        /// Update the stock of an existing shoe.
        /// </summary>
        /// <param name="id">The ID of the shoe to update stock for.</param>
        /// <param name="newStock">The new stock value.</param>
        /// <returns>A 200 status code if successful, 404 if the shoe is not found, or 500 for internal server errors.</returns>
        /// <response code="200">Shoe stock updated successfully.</response>
        /// <response code="404">Shoe not found.</response>
        /// <response code="400">Invalid input data.</response>
        /// <response code="500">Internal server error.</response>
        [HttpPatch("{id}/stock")]
        [SwaggerOperation(Summary = "Update shoe stock", Description = "Updates the stock of an existing shoe by its ID.")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> UpdateShoeStockAsync(int id, [FromBody] int newStock)
        {
            if (newStock < 0)
                return BadRequest("Stock value cannot be negative.");

            try
            {
                var result = await _shoeInterface.UpdateShoeStockAsync(id, newStock);
                return Ok(result);
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
    }
}
