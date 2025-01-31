using Microsoft.EntityFrameworkCore;
using TenisHolly.Models;

namespace TenisHolly.Seeders
{
    public static class InventorySeeder
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Inventory>().HasData(
                new Inventory { Id = 1, StoreId = 1, ShoeId = 1, Quantity = 50 },
                new Inventory { Id = 2, StoreId = 2, ShoeId = 2, Quantity = 40 },
                new Inventory { Id = 3, StoreId = 3, ShoeId = 3, Quantity = 30 },
                new Inventory { Id = 4, StoreId = 4, ShoeId = 4, Quantity = 20 },
                new Inventory { Id = 5, StoreId = 5, ShoeId = 5, Quantity = 25 }
            );
        }
    }
}
