using Microsoft.EntityFrameworkCore;
using TenisHolly.Models;

namespace TenisHolly.Seeders
{
    public static class ShoeSeeder
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Shoe>().HasData(
                new Shoe { Id = 1, Reference = "Nike AirMax", Gender = "Caballero", Size = 42, Price = 120.50m, Stock = 10, StoreId = 1 },
                new Shoe { Id = 2, Reference = "Adidas Superstar", Gender = "Dama", Size = 38, Price = 95.99m, Stock = 15, StoreId = 2 },
                new Shoe { Id = 3, Reference = "Puma Classic", Gender = "Caballero", Size = 40, Price = 80.00m, Stock = 20, StoreId = 3 },
                new Shoe { Id = 4, Reference = "Reebok Runner", Gender = "Dama", Size = 39, Price = 75.99m, Stock = 12, StoreId = 4 },
                new Shoe { Id = 5, Reference = "New Balance 574", Gender = "Caballero", Size = 41, Price = 110.00m, Stock = 8, StoreId = 5 }
            );
        }
    }
}
