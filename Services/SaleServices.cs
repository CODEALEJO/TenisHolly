using Microsoft.EntityFrameworkCore;
using TenisHolly.Data;
using TenisHolly.DTOs;
using TenisHolly.Interfaces;
using TenisHolly.Models;

namespace TenisHolly.Services;
public class SaleServices : ISaleInterface
{

    private readonly ApplicationDbContext _context;

    public SaleServices(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<SaleDTO> CreateSaleAsync(SaleDTO saleDto)
    {
        // Buscar el zapato y verificar si hay suficiente cantidad en el inventario
        var inventory = await _context.Inventories
            .FirstOrDefaultAsync(i => i.StoreId == saleDto.StoreId && i.ShoeId == saleDto.ShoeId);

        if (inventory == null || inventory.Quantity < saleDto.Quantity)
            throw new Exception("Cantidad insuficiente en inventario.");

        // Calcular el total
        var shoe = await _context.Shoes.FirstOrDefaultAsync(s => s.Id == saleDto.ShoeId);
        if (shoe == null) throw new KeyNotFoundException("Zapato no encontrado.");


        decimal total = saleDto.Quantity * shoe.Price;

        // Crear la venta
        var sale = new Sale
        {
            Date = DateTime.UtcNow,
            Quantity = saleDto.Quantity,
            Total = total,
            PaymentMethod = saleDto.PaymentMethod,
            Seller = saleDto.Seller,
            ShoeId = saleDto.ShoeId,
            StoreId = saleDto.StoreId
        };

        _context.Sales.Add(sale);

        // Actualizar inventario
        inventory.Quantity -= saleDto.Quantity;

        await _context.SaveChangesAsync();

        // Devolver el DTO creado
        saleDto.Total = total;
        return saleDto;
    }

    public async Task<List<SaleDTO>> GetSalesByStoreAsync(int storeId)
    {
        var sales = await _context.Sales
            .Where(s => s.StoreId == storeId)
            .Include(s => s.Shoe)
            .Select(s => new SaleDTO
            {
                Date = s.Date,
                Quantity = s.Quantity,
                Total = s.Total,
                PaymentMethod = s.PaymentMethod,
                Seller = s.Seller,
                ShoeId = s.ShoeId,
                StoreId = s.StoreId
            })
            .ToListAsync();

        return sales;
    }

    public async Task<List<SaleDTO>> GetSalesByDateAsync(int storeId, DateTime date)
    {
        var sales = await _context.Sales
            .Where(s => s.StoreId == storeId && s.Date.Date == date.Date)
            .Include(s => s.Shoe)
            .Select(s => new SaleDTO
            {
                Date = s.Date,
                Quantity = s.Quantity,
                Total = s.Total,
                PaymentMethod = s.PaymentMethod,
                Seller = s.Seller,
                ShoeId = s.ShoeId,
                StoreId = s.StoreId
            })
            .ToListAsync();

        return sales;
    }

    public async Task<decimal> GetDailyTotalAsync(int storeId, DateTime date)
    {
        var dailyTotal = await _context.Sales
            .Where(s => s.StoreId == storeId && s.Date.Date == date.Date)
            .SumAsync(s => s.Total);

        return dailyTotal;
    }
}