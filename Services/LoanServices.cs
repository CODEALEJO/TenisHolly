
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TenisHolly.Data;
using TenisHolly.DTOs;
using TenisHolly.Helpers;
using TenisHolly.Interfaces;
using TenisHolly.Models;

namespace TenisHolly.Services
{
    public class LoanService : ILoanInterface
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<LoanService> _logger;

        public LoanService(
            ApplicationDbContext context,
            ILogger<LoanService> logger)
        {
            _context = context;
            _logger = logger;
        }

        // Solicitar un préstamo
        public async Task<LoanResponseDTO> RequestLoanAsync(LoanDTO loanDto)
        {
            try
            {
                // Convertir la lista de SizeDetailDTO a string para guardar en la BD
                var sizesString = JsonSerializer.Serialize(loanDto.Sizes);

                var loan = new Loan
                {
                    ShoeId = loanDto.ShoeId,
                    FromStoreId = loanDto.FromStoreId,
                    ToStoreId = loanDto.ToStoreId,
                    Quantity = loanDto.Quantity,
                    LoanDate = loanDto.LoanDate,
                    ReturnDate = loanDto.ReturnDate,
                    Sizes = sizesString, // Guardamos como JSON string
                    Status = Loan.LoanStatus.Prestado
                };

                _context.Loans.Add(loan);
                await _context.SaveChangesAsync();

                var loanWithDetails = await _context.Loans
                    .Include(l => l.Shoe)
                    .Include(l => l.FromStore)
                    .Include(l => l.ToStore)
                    .FirstOrDefaultAsync(l => l.Id == loan.Id);

                return new LoanResponseDTO
                {
                    ShoeReference = loanWithDetails.Shoe.Reference,
                    FromStoreName = loanWithDetails.FromStore.Name,
                    ToStoreName = loanWithDetails.ToStore.Name,
                    Quantity = loanWithDetails.Quantity,
                    LoanDate = loanWithDetails.LoanDate,
                    ReturnDate = loanWithDetails.ReturnDate,
                    Sizes = loanDto.Sizes,
                    Status = loanWithDetails.Status.ToString()
                };
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while requesting the loan.", ex);
            }
        }

        // Aprobar un préstamo
        public async Task ApproveLoanAsync(int loanId)
        {
            var loan = await _context.Loans.SingleOrDefaultAsync(l => l.Id == loanId);
            if (loan == null) throw new KeyNotFoundException("Loan not found.");

            var inventory = await _context.Inventories.SingleOrDefaultAsync(i => i.ShoeId == loan.ShoeId && i.StoreId == loan.ToStoreId);

            inventory ??= new Inventory
            {
                ShoeId = loan.ShoeId,
                StoreId = loan.ToStoreId,
                Quantity = 0
            };

            var fromInventory = await _context.Inventories.SingleOrDefaultAsync(i => i.ShoeId == loan.ShoeId && i.StoreId == loan.FromStoreId);

            if (fromInventory == null || fromInventory.Quantity < loan.Quantity)
            {
                throw new InvalidOperationException("Not enough stock to transfer.");
            }

            inventory.Quantity += loan.Quantity;
            fromInventory.Quantity -= loan.Quantity;

            loan.Status = Loan.LoanStatus.Pago;

            await _context.SaveChangesAsync();
        }


        // Cancelar un préstamo
        public async Task CancelLoanAsync(int loanId)
        {
            var loan = await _context.Loans.FindAsync(loanId);
            if (loan == null) throw new KeyNotFoundException("Loan not found.");

            _context.Loans.Remove(loan);
            await _context.SaveChangesAsync();
        }

        // Obtener todos los préstamos
        public async Task<List<LoanResponseDTO>> GetAllLoansAsync()
        {
            try
            {
                var loans = await _context.Loans
                    .Include(l => l.Shoe)
                    .Include(l => l.FromStore)
                    .Include(l => l.ToStore)
                    .ToListAsync();

                return loans.Select(l => new LoanResponseDTO
                {
                    ShoeReference = l.Shoe.Reference,
                    FromStoreName = l.FromStore.Name,
                    ToStoreName = l.ToStore.Name,
                    Quantity = l.Quantity,
                    LoanDate = l.LoanDate,
                    ReturnDate = l.ReturnDate,
                    Sizes = JsonHelper.ParseSizes(l.Sizes),
                    Status = l.Status.ToString()
                }).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting all loans");
                throw new Exception("An error occurred while retrieving loans", ex);
            }
        }


        // Obtener préstamo por ID
        public async Task<LoanResponseDTO> GetLoanByIdAsync(int loanId)
        {
            var loan = await _context.Loans
                .Include(l => l.Shoe)
                .Include(l => l.FromStore)
                .Include(l => l.ToStore)
                .FirstOrDefaultAsync(l => l.Id == loanId);

            if (loan == null) throw new KeyNotFoundException("Loan not found.");

            return new LoanResponseDTO
            {
                ShoeReference = loan.Shoe.Reference,
                FromStoreName = loan.FromStore.Name,
                ToStoreName = loan.ToStore.Name,
                Quantity = loan.Quantity,
                LoanDate = loan.LoanDate,
                ReturnDate = loan.ReturnDate,
                Sizes = JsonHelper.ParseSizes(loan.Sizes),
                Status = loan.Status.ToString()
            };
        }
    }
}
