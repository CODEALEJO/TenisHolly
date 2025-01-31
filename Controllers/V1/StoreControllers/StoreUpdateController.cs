using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TenisHolly.DTOs;
using TenisHolly.Interface;

namespace TenisHolly.Controllers.V1.StoreControllers
{
    [ApiController]
    [Route("api/v1/stores")]
    [Tags("Store")]
    public class StoreUpdateController : StoreController
    {
        public StoreUpdateController(IStoreInterface storeService) : base(storeService) { }

        /// <summary>
        /// Update a store.
        /// </summary>
        [HttpPut]
        [SwaggerOperation(Summary = "Update a store", Description = "Updates an existing store.")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> UpdateStoreAsync([FromBody] StoreDto storeDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                await _storeService.Update(storeDto);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
    }
}
