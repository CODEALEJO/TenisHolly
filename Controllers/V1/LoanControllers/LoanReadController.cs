using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TenisHolly.Interfaces;
using TenisHolly.DTOs;

namespace TenisHolly.Controllers.V1.LoanControllers
{
    [ApiController]
    [Route("api/v1/loans")]
    [Tags("Loan")]
    public class LoanReadController : LoanController
    {
        public LoanReadController(ILoanInterface loanInterface) : base(loanInterface) { }

        /// <summary>
        /// Get all loans.
        /// </summary>
        /// <returns>A list of all loans.</returns>
        /// <response code="200">Loans retrieved successfully.</response>
        /// <response code="500">Internal server error.</response>
        [HttpGet]
        [SwaggerOperation(Summary = "Get all loans", Description = "Retrieves a list of all loans.")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetAllLoansAsync()
        {
            try
            {
                var loans = await _loanInterface.GetAllLoansAsync();
                return Ok(loans);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        /// <summary>
        /// Get a loan by ID.
        /// </summary>
        /// <param name="loanId">The loan ID.</param>
        /// <returns>The loan details.</returns>
        /// <response code="200">Loan retrieved successfully.</response>
        /// <response code="404">Loan not found.</response>
        /// <response code="500">Internal server error.</response>
        [HttpGet("{loanId}")]
        [SwaggerOperation(Summary = "Get loan by ID", Description = "Retrieves details of a specific loan by its ID.")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetLoanByIdAsync(int loanId)
        {
            try
            {
                var loan = await _loanInterface.GetLoanByIdAsync(loanId);
                return Ok(loan);
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Loan not found.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
    }
}
