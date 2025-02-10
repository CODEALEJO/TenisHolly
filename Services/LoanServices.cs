using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TenisHolly.Data;
using TenisHolly.DTOs;
using TenisHolly.Interfaces;
using TenisHolly.Models;

namespace TenisHolly.Services
{
    public class LoanService : ILoanInterface
    {
        private readonly ApplicationDbContext _context;

        public LoanService(ApplicationDbContext context)
        {
            _context = context;
        }

        // Solicitar un préstamo
        public async Task RequestLoanAsync(LoanDTO loanDto)
        {
            var loan = new Loan
            {
                ShoeId = loanDto.ShoeId,
                FromStoreId = loanDto.FromStoreId,
                ToStoreId = loanDto.ToStoreId,
                Quantity = loanDto.Quantity,
                LoanDate = loanDto.LoanDate,
                ReturnDate = loanDto.ReturnDate,
                Sizes = loanDto.Sizes,
                Status = Loan.LoanStatus.Prestado
            };

            _context.Loans.Add(loan);
            await _context.SaveChangesAsync();
        }

        // Aprobar un préstamo
        public async Task ApproveLoanAsync(int loanId)
        {
            var loan = await _context.Loans.FindAsync(loanId);
            if (loan == null) throw new KeyNotFoundException("Loan not found.");

            // Verificar inventario en la tienda de destino
            var inventory = await _context.Inventories
                .FirstOrDefaultAsync(i => i.ShoeId == loan.ShoeId && i.StoreId == loan.ToStoreId);

            if (inventory == null)
            {
                inventory = new Inventory
                {
                    ShoeId = loan.ShoeId,
                    StoreId = loan.ToStoreId,
                    Quantity = loan.Quantity
                };
                _context.Inventories.Add(inventory);
            }
            else
            {
                inventory.Quantity += loan.Quantity;
                _context.Inventories.Update(inventory);
            }

            // Restar del inventario de la tienda de origen
            var fromInventory = await _context.Inventories
                .FirstOrDefaultAsync(i => i.ShoeId == loan.ShoeId && i.StoreId == loan.FromStoreId);

            if (fromInventory == null || fromInventory.Quantity < loan.Quantity)
            {
                throw new InvalidOperationException("Not enough stock to transfer.");
            }

            fromInventory.Quantity -= loan.Quantity;
            _context.Inventories.Update(fromInventory);

            // Cambiar estado del préstamo
            loan.Status = Loan.LoanStatus.Pago;
            _context.Loans.Update(loan);

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
        public async Task<List<LoanDTO>> GetAllLoansAsync()
        {
            return await _context.Loans.Select(l => new LoanDTO
            {
                ShoeId = l.ShoeId,
                FromStoreId = l.FromStoreId,
                ToStoreId = l.ToStoreId,
                Quantity = l.Quantity,
                LoanDate = l.LoanDate,
                ReturnDate = l.ReturnDate,
                Sizes = l.Sizes
            }).ToListAsync();
        }

        // Obtener préstamo por ID
        public async Task<LoanDTO> GetLoanByIdAsync(int loanId)
        {
            var loan = await _context.Loans.FindAsync(loanId);
            if (loan == null) throw new KeyNotFoundException("Loan not found.");

            return new LoanDTO
            {
                ShoeId = loan.ShoeId,
                FromStoreId = loan.FromStoreId,
                ToStoreId = loan.ToStoreId,
                Quantity = loan.Quantity,
                LoanDate = loan.LoanDate,
                ReturnDate = loan.ReturnDate,
                Sizes = loan.Sizes
            };
        }
    }
}
