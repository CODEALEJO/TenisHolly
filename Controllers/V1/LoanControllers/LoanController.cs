using Microsoft.AspNetCore.Mvc;
using TenisHolly.Interfaces;

namespace TenisHolly.Controllers.V1.LoanControllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Tags("Loan")]
    public class LoanController : ControllerBase
    {
          protected readonly ILoanInterface _loanInterface;

        public LoanController(ILoanInterface loanInterface)
        {
            _loanInterface = loanInterface;
        }
    }
}