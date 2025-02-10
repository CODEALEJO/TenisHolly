using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TenisHolly.Interfaces;
using TenisHolly.DTOs;
using TenisHolly.Models;
using Swashbuckle.AspNetCore.Annotations;

namespace TenisHolly.Controllers.V1.LoanControllers
{
    [ApiController]
    [Route("api/v1/loans")]
    [Tags("Loan")]
    public class LoanUpdateController : LoanController
    {
        public LoanUpdateController(ILoanInterface loanInterface) : base(loanInterface){}

       /// <summary>
        /// Aprueba un préstamo de zapatos entre tiendas.
        /// </summary>
        /// <param name="loanId">ID del préstamo a aprobar.</param>
        /// <returns>Devuelve un mensaje de éxito o error.</returns>
        /// <response code="200">Préstamo aprobado exitosamente</response>
        /// <response code="400">Stock insuficiente para aprobar el préstamo</response>
        /// <response code="404">Préstamo no encontrado</response>
        [HttpPut("approve/{loanId}")]
        [SwaggerOperation(Summary = "approve loan", Description = "Approves an inter-store shoe loan.")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> ApproveLoanAsync(int loanId)
        {
            try
            {
                await _loanInterface.ApproveLoanAsync(loanId);
                return Ok(new { Message = "Loan successfully approved." });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { Message = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }
    }
}
