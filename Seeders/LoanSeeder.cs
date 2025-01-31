using Microsoft.EntityFrameworkCore;
using TenisHolly.Models;

namespace TenisHolly.Seeders
{
    public static class LoanSeeder
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Loan>().HasData(
                new Loan { Id = 1, LoanDate = DateTime.Now, ReturnDate = null, Quantity = 5, FromStoreId = 1, ToStoreId = 2, ShoeId = 1, Sizes = "42", Status = Loan.LoanStatus.Prestado },
                new Loan { Id = 2, LoanDate = DateTime.Now, ReturnDate = null, Quantity = 3, FromStoreId = 2, ToStoreId = 3, ShoeId = 2, Sizes = "38", Status = Loan.LoanStatus.Prestado },
                new Loan { Id = 3, LoanDate = DateTime.Now, ReturnDate = null, Quantity = 2, FromStoreId = 3, ToStoreId = 4, ShoeId = 3, Sizes = "40", Status = Loan.LoanStatus.PorPagar },
                new Loan { Id = 4, LoanDate = DateTime.Now, ReturnDate = null, Quantity = 4, FromStoreId = 4, ToStoreId = 5, ShoeId = 4, Sizes = "39", Status = Loan.LoanStatus.Pago },
                new Loan { Id = 5, LoanDate = DateTime.Now, ReturnDate = null, Quantity = 1, FromStoreId = 5, ToStoreId = 1, ShoeId = 5, Sizes = "41", Status = Loan.LoanStatus.Prestado }
            );
        }
    }
}
