using Microsoft.EntityFrameworkCore;
using TenisHolly.Models;

namespace TenisHolly.Seeders
{
    public static class StoreSeeder
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Store>().HasData(
                new Store { Id = 1, Name = "Tienda Central", Location = "Centro Comercial Aventura" },
                new Store { Id = 2, Name = "Tienda Norte", Location = "Mall Norte Plaza" },
                new Store { Id = 3, Name = "Tienda Sur", Location = "Centro Comercial del Sur" },
                new Store { Id = 4, Name = "Tienda Este", Location = "Plaza Este" },
                new Store { Id = 5, Name = "Tienda Oeste", Location = "Centro Comercial Oeste" }
            );
        }
    }
}
