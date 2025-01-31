using Microsoft.AspNetCore.Mvc;
using TenisHolly.Interfaces;

namespace TenisHolly.Controllers.V1.SaleControllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Tags("Sale")]
    public class SaleController : ControllerBase
    {
        protected readonly ISaleInterface _saleInterface;

        public SaleController(ISaleInterface saleInterface)
        {
            _saleInterface = saleInterface;
        }
    }
}
