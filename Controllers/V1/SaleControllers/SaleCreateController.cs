using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TenisHolly.DTOs;
using TenisHolly.Interfaces;

namespace TenisHolly.Controllers.V1.SaleControllers
{
    [ApiController]
    [Route("api/v1/sales")]
    [Tags("Sale")]
    public class SaleCreateController : SaleController
    {
        public SaleCreateController(ISaleInterface saleInterface) : base(saleInterface) { }

        /// <summary>
        /// Create a new sale.
        /// </summary>
        /// <param name="saleDto">The details of the sale to create.</param>
        /// <returns>A 201 status code if successful, 400 for invalid data, or 500 for internal server errors.</returns>
        /// <response code="201">Sale created successfully.</response>
        /// <response code="400">Invalid input data.</response>
        /// <response code="500">Internal server error.</response>
        [HttpPost]
        [SwaggerOperation(Summary = "Create a new sale", Description = "Creates a new sale record.")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> CreateSaleAsync([FromBody] SaleDTO saleDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var createdSale = await _saleInterface.CreateSaleAsync(saleDto);
                return StatusCode(201, createdSale);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
    }
}
