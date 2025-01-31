using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TenisHolly.Interfaces;

namespace TenisHolly.Controllers.V1.ShoeControllers
{
    [ApiController]
    [Route("api/v1/shoes")]
    [Tags("Shoe")]

    public class ShoeDeleteController : ShoeController
    {
        public ShoeDeleteController(IShoeInterface shoeInterface) : base(shoeInterface) { }

        /// <summary>
        /// Delete a shoe from the inventory.
        /// </summary>
        /// <param name="id">The ID of the shoe to delete.</param>
        /// <returns>A 200 status code if the shoe was successfully deleted, 404 if not found, or 500 for internal server errors.</returns>
        /// <response code="200">Shoe deleted successfully.</response>
        /// <response code="404">Shoe not found.</response>
        /// <response code="500">Internal server error.</response>
        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Delete a shoe", Description = "Deletes a shoe from the inventory by its ID.")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> DeleteShoeAsync(int id)
        {
            try
            {
                var success = await _shoeInterface.DeleteShoeAsync(id);
                if (!success)
                {
                    return NotFound($"Shoe with ID {id} not found.");
                }

                return Ok($"Shoe with ID {id} deleted successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
    }
}
