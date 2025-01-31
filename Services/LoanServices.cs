// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;
// using Microsoft.EntityFrameworkCore;
// using TenisHolly.Data;
// using TenisHolly.DTOs;
// using TenisHolly.Interfaces;
// using TenisHolly.Models;

// namespace TenisHolly.Services;
// public class LoanService : ILoanInterface
// {
//     private readonly ApplicationDbContext _context;

//     public LoanService(ApplicationDbContext context)
//     {
//         _context = context;
//     }

//     // Lógica para gestionar solicitudes de préstamo
//     public async Task RequestLoan(LoanDTO loan)
//     {
//         // Validar existencia del zapato en la tienda de origen
//         var shoeInStore = await _context.Inventory
//             .FirstOrDefaultAsync(i => i.StoreId == loan.FromStoreId && i.ShoeId == loan.ShoeId);

//         if (shoeInStore == null || shoeInStore.Quantity < loan.Quantity)
//         {
//             throw new Exception("La tienda de origen no tiene suficiente stock del zapato solicitado.");
//         }

//         // Restar el stock en la tienda de origen
//         shoeInStore.Quantity -= loan.Quantity;

//         // Verificar si el zapato ya existe en la tienda de destino
//         var shoeInDestination = await _context.Inventory
//             .FirstOrDefaultAsync(i => i.StoreId == loan.ToStoreId && i.ShoeId == loan.ShoeId);

//         if (shoeInDestination == null)
//         {
//             // Si no existe, crear un nuevo registro en el inventario de la tienda de destino
//             _context.Inventory.Add(new Inventory
//             {
//                 StoreId = loan.ToStoreId,
//                 ShoeId = loan.ShoeId,
//                 Quantity = loan.Quantity
//             });
//         }
//         else
//         {
//             // Si ya existe, incrementar el stock
//             shoeInDestination.Quantity += loan.Quantity;
//         }

//         // Registrar el préstamo
//         var newLoan = new Loan
//         {
//             ShoeId = loan.ShoeId,
//             FromStoreId = loan.FromStoreId,
//             ToStoreId = loan.ToStoreId,
//             Quantity = loan.Quantity,
//             LoanDate = DateTime.UtcNow
//         };

//         _context.Loans.Add(newLoan);

//         // Guardar los cambios en la base de datos
//         await _context.SaveChangesAsync();
//     }

//     // Obtener préstamos realizados por una tienda específica
//     public async Task<List<LoanDTO>> GetLoansByStore(int storeId)
//     {
//         return await _context.Loans
//             .Where(l => l.FromStoreId == storeId || l.ToStoreId == storeId)
//             .Select(l => new LoanDTO
//             {
//                 ShoeId = l.ShoeId,
//                 FromStoreId = l.FromStoreId,
//                 ToStoreId = l.ToStoreId,
//                 Quantity = l.Quantity,
//                 LoanDate = l.LoanDate
//             })
//             .ToListAsync();
//     }

//     // Crear un préstamo directamente (sin validaciones complejas)
//     public async Task<LoanDTO> CreateLoanAsync(LoanDTO loanDto)
//     {
//         var loan = new Loan
//         {
//             ShoeId = loanDto.ShoeId,
//             FromStoreId = loanDto.FromStoreId,
//             ToStoreId = loanDto.ToStoreId,
//             Quantity = loanDto.Quantity,
//             LoanDate = loanDto.LoanDate
//         };

//         _context.Loans.Add(loan);
//         await _context.SaveChangesAsync();

//         return loanDto;
//     }
// }
