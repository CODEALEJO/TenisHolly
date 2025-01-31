using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Threading.Tasks;
using TenisHolly.Interface;

namespace TenisHolly.Controllers.V1.StoreControllers
{
    [ApiController]
    [Route("api/v1/stores")]
    [Tags("Store")]
    public class StoreDeleteController : StoreController
    {
        public StoreDeleteController(IStoreInterface storeService) : base(storeService) { }

        /// <summary>
        /// Delete a store.
        /// </summary>
        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Delete a store", Description = "Removes a store from the system.")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteStoreAsync(int id)
        {
            await _storeService.Delete(id);
            return NoContent();
        }
    }
}
