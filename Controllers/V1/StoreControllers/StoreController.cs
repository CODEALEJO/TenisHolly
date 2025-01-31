using Microsoft.AspNetCore.Mvc;
using TenisHolly.Interface;

namespace TenisHolly.Controllers.V1.StoreControllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Tags("Store")]
    public class StoreController : ControllerBase
    {
        protected readonly IStoreInterface _storeService;

        public StoreController(IStoreInterface storeService)
        {
            _storeService = storeService;
        }
    }
}
