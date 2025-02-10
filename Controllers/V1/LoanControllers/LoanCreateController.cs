using Microsoft.AspNetCore.Mvc;
using TenisHolly.Models;
using TenisHolly.DTOs;
using TenisHolly.Interfaces;
using System.Threading.Tasks;
using Swashbuckle.AspNetCore.Annotations;

namespace TenisHolly.Controllers.V1.LoanControllers
{
    [ApiController]
    [Route("api/v1/loans")]
    [Tags("Loan")]
    public class LoanCreateController : LoanController
    {
        public LoanCreateController(ILoanInterface loanInterface) : base(loanInterface) { }

            /// <summary>
        /// Solicita un nuevo préstamo de zapatos entre tiendas.
        /// </summary>
        /// <param name="loanDto">Datos del préstamo a solicitar.</param>
        /// <returns>Devuelve la información del préstamo creado.</returns>
        /// <response code="201">Préstamo creado exitosamente</response>
        /// <response code="400">Datos inválidos</response>
        [HttpPost("request")]
        [SwaggerOperation(Summary = "Request a loan", Description = "Create a store-to-store shoe loan application.")]
        [ProducesResponseType(typeof(LoanDTO), 201)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> RequestLoanAsync([FromBody] LoanDTO loanDto)
        {
            if (loanDto == null)
            {
                return BadRequest("The loan data is invalid.");
            }

            await _loanInterface.RequestLoanAsync(loanDto);
            return CreatedAtAction(nameof(RequestLoanAsync), new { loanDto.ShoeId }, loanDto);
        }
    }
}
