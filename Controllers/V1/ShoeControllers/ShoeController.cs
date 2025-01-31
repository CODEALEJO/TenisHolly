using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TenisHolly.Interfaces;

namespace TenisHolly.Controllers.V1.ShoeControllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Tags("Shoe")]

    public class ShoeController : ControllerBase
    {
        protected readonly IShoeInterface _shoeInterface; // Cambiado de private a protected

        public ShoeController(IShoeInterface shoeInterface)
        {
            _shoeInterface = shoeInterface;
        }
    }
}
