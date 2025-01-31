using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TenisHolly.DTOs;
using TenisHolly.Interface;

namespace TenisHolly.Controllers.V1.StoreControllers
{
    [ApiController]
    [Route("api/v1/stores")]
    [Tags("Store")]
    public class StoreCreateController : StoreController
    {
        public StoreCreateController(IStoreInterface storeService) : base(storeService) { }

        /// <summary>
        /// Add a new store.
        /// </summary>
        /// <param name="storeDto">The store details.</param>
        /// <returns>A 201 status code if successful, or an error code otherwise.</returns>
        [HttpPost]
        [SwaggerOperation(Summary = "Add a new store", Description = "Creates a new store record.")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> AddStoreAsync([FromBody] StoreDto storeDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var store = await _storeService.Add(storeDto);
                return StatusCode(201, store);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
    }
}
