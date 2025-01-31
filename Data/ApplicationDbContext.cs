using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TenisHolly.Models;
using TenisHolly.Seeders;

namespace TenisHolly.Data;
public class ApplicationDbContext : DbContext
{
    public DbSet<Loan> Loans { get; set; }
    public DbSet<Sale> Sales { get; set; }
    public DbSet<Shoe> Shoes { get; set; }
    public DbSet<Store> Stores { get; set; }
    public DbSet<Inventory> Inventories { get; set; }
    public DbSet<User> Users { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        // Configuración de relaciones entre Loan y Store
        modelBuilder.Entity<Loan>()
            .HasOne(l => l.FromStore)
            .WithMany(s => s.LoansGiven) // Aquí sí especificamos la colección correcta
            .HasForeignKey(l => l.FromStoreId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Loan>()
            .HasOne(l => l.ToStore)
            .WithMany(s => s.LoansReceived) // Aquí también
            .HasForeignKey(l => l.ToStoreId)
            .OnDelete(DeleteBehavior.Restrict);


        // Seeders
        StoreSeeder.Seed(modelBuilder);
        ShoeSeeder.Seed(modelBuilder);
        SaleSeeder.Seed(modelBuilder);
        LoanSeeder.Seed(modelBuilder);
        InventorySeeder.Seed(modelBuilder);
        UserSeeder.Seed(modelBuilder);
    }
}