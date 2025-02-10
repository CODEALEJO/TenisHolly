using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TenisHolly.DTOs;

namespace TenisHolly.Interfaces;
public interface ILoanInterface
{
    Task RequestLoanAsync(LoanDTO loanDto);
    Task ApproveLoanAsync(int loanId);
    Task<List<LoanDTO>> GetAllLoansAsync();
    Task<LoanDTO> GetLoanByIdAsync(int loanId);
    Task CancelLoanAsync(int loanId);
}
