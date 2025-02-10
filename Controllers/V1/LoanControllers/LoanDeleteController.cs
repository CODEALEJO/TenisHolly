using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TenisHolly.Interfaces;

namespace TenisHolly.Controllers.V1.LoanControllers
{
    [ApiController]
    [Route("api/v1/loans")]
    [Tags("Loan")]
    public class LoanDeleteController : LoanController
    {
        public LoanDeleteController(ILoanInterface loanInterface) : base(loanInterface) { }

       
        /// <summary>
        /// Cancela un préstamo de zapatos.
        /// </summary>
        /// <param name="loanId">ID del préstamo a cancelar.</param>
        /// <returns>Devuelve un estado 204 si la cancelación fue exitosa.</returns>
        /// <response code="204">Préstamo cancelado exitosamente</response>
        /// <response code="404">Préstamo no encontrado</response>
        [HttpDelete("cancel/{loanId}")]
        [SwaggerOperation(Summary = "Cancel loan", Description = "Delete a shoe loan request.")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> CancelLoanAsync(int loanId)
        {
            await _loanInterface.CancelLoanAsync(loanId);
            return NoContent();
        }
    }
}
