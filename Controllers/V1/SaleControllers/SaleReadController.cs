using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TenisHolly.Interfaces;

namespace TenisHolly.Controllers.V1.SaleControllers
{
    [ApiController]
    [Route("api/v1/sales")]
    [Tags("Sale")]
    public class SaleReadController : SaleController
    {
        public SaleReadController(ISaleInterface saleInterface) : base(saleInterface) { }

        /// <summary>
        /// Get all sales for a specific store.
        /// </summary>
        /// <param name="storeId">The store's ID.</param>
        /// <returns>A list of sales for the store.</returns>
        /// <response code="200">Sales retrieved successfully.</response>
        /// <response code="400">Invalid store ID.</response>
        /// <response code="500">Internal server error.</response>
        [HttpGet("store/{storeId}")]
        [SwaggerOperation(Summary = "Get all sales by store", Description = "Retrieves all sales made at a specific store.")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetSalesByStoreAsync(int storeId)
        {
            try
            {
                var sales = await _saleInterface.GetSalesByStoreAsync(storeId);
                return Ok(sales);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        /// <summary>
        /// Get sales for a specific store on a specific date.
        /// </summary>
        /// <param name="storeId">The store's ID.</param>
        /// <param name="date">The date of the sales to retrieve.</param>
        /// <returns>A list of sales for the specified store on the given date.</returns>
        /// <response code="200">Sales retrieved successfully.</response>
        /// <response code="400">Invalid store ID or date.</response>
        /// <response code="500">Internal server error.</response>
        [HttpGet("store/{storeId}/date/{date}")]
        [SwaggerOperation(Summary = "Get sales by date", Description = "Retrieves sales for a specific store on a given date.")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetSalesByDateAsync(int storeId, DateTime date)
        {
            try
            {
                var sales = await _saleInterface.GetSalesByDateAsync(storeId, date);
                return Ok(sales);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        /// <summary>
        /// Get the total sales for a specific store on a specific date.
        /// </summary>
        /// <param name="storeId">The store's ID.</param>
        /// <param name="date">The date to calculate the sales total.</param>
        /// <returns>The total sales amount for the given store and date.</returns>
        /// <response code="200">Sales total retrieved successfully.</response>
        /// <response code="400">Invalid store ID or date.</response>
        /// <response code="500">Internal server error.</response>
        [HttpGet("store/{storeId}/daily-total/{date}")]
        [SwaggerOperation(Summary = "Get daily sales total", Description = "Retrieves the total sales amount for a specific store on a given date.")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetDailyTotalAsync(int storeId, DateTime date)
        {
            try
            {
                var dailyTotal = await _saleInterface.GetDailyTotalAsync(storeId, date);
                return Ok(new { DailyTotal = dailyTotal });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
    }
}
