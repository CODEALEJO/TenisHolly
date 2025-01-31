using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;
using System.Threading.Tasks;
using TenisHolly.DTOs;
using TenisHolly.Interface;

namespace TenisHolly.Controllers.V1.StoreControllers
{
    [ApiController]
    [Route("api/v1/stores")]
    [Tags("Store")]
    public class StoreReadController : StoreController
    {
        public StoreReadController(IStoreInterface storeService) : base(storeService) { }

        /// <summary>
        /// Get all stores.
        /// </summary>
        [HttpGet]
        [SwaggerOperation(Summary = "Get all stores", Description = "Retrieves a list of all stores.")]
        [ProducesResponseType(200)]
        public async Task<ActionResult<IEnumerable<StoreDto>>> GetAllStoresAsync()
        {
            return Ok(await _storeService.GetAll());
        }

        /// <summary>
        /// Get a store by ID.
        /// </summary>
        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Get a store by ID", Description = "Retrieves details of a specific store.")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<StoreDto>> GetStoreByIdAsync(int id)
        {
            var store = await _storeService.GetById(id);
            if (store == null) return NotFound("Store not found.");
            return Ok(store);
        }
    }
}
