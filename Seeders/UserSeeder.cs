using Microsoft.EntityFrameworkCore;
using TenisHolly.Models;

namespace TenisHolly.Seeders
{
    public static class UserSeeder
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, FullName = "Admin User", Email = "admin@example.com", PasswordHash = "hashedpassword1", Role = "Admin" },
                new User { Id = 2, FullName = "Juan Pérez", Email = "juan.perez@example.com", PasswordHash = "hashedpassword2", Role = "User" },
                new User { Id = 3, FullName = "María Gómez", Email = "maria.gomez@example.com", PasswordHash = "hashedpassword3", Role = "User" },
                new User { Id = 4, FullName = "Carlos López", Email = "carlos.lopez@example.com", PasswordHash = "hashedpassword4", Role = "Manager" },
                new User { Id = 5, FullName = "Ana Martínez", Email = "ana.martinez@example.com", PasswordHash = "hashedpassword5", Role = "User" }
            );
        }
    }
}
