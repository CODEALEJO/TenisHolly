using Microsoft.EntityFrameworkCore;
using TenisHolly.Models;

namespace TenisHolly.Seeders
{
    public static class SaleSeeder
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Sale>().HasData(
                new Sale { Id = 1, Date = DateTime.Now, Quantity = 2, Total = 241.00m, PaymentMethod = "Tarjeta", Seller = "Carlos", ShoeId = 1, StoreId = 1 },
                new Sale { Id = 2, Date = DateTime.Now, Quantity = 1, Total = 95.99m, PaymentMethod = "Efectivo", Seller = "Andrea", ShoeId = 2, StoreId = 2 },
                new Sale { Id = 3, Date = DateTime.Now, Quantity = 3, Total = 240.00m, PaymentMethod = "Transferencia", Seller = "Pedro", ShoeId = 3, StoreId = 3 },
                new Sale { Id = 4, Date = DateTime.Now, Quantity = 1, Total = 75.99m, PaymentMethod = "Tarjeta", Seller = "Lucía", ShoeId = 4, StoreId = 4 },
                new Sale { Id = 5, Date = DateTime.Now, Quantity = 2, Total = 220.00m, PaymentMethod = "Efectivo", Seller = "Miguel", ShoeId = 5, StoreId = 5 }
            );
        }
    }
}
