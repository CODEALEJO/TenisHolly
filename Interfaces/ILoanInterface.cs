using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TenisHolly.DTOs;

namespace TenisHolly.Interfaces;
public interface ILoanInterface
{
    Task<LoanResponseDTO> RequestLoanAsync(LoanDTO loanDto);
    Task ApproveLoanAsync(int loanId);
    Task<List<LoanResponseDTO>> GetAllLoansAsync();
    Task<LoanResponseDTO> GetLoanByIdAsync(int loanId);
    Task CancelLoanAsync(int loanId);

}
