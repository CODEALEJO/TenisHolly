using Microsoft.EntityFrameworkCore;
using TenisHolly.Data;
using TenisHolly.DTOs;
using TenisHolly.Interfaces;
using TenisHolly.Models;

namespace TenisHolly.Services;

public class ShoeService : IShoeInterface
{
    private readonly ApplicationDbContext _context;

    public ShoeService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<ShoeDTO>> GetAllShoesAsync()
    {
        return await _context.Shoes
            .Select(s => new ShoeDTO
            {
                Id = s.Id,
                Reference = s.Reference,
                Gender = s.Gender,
                Size = s.Size,
                Price = s.Price,
                Stock = s.Stock,
                StoreId = s.StoreId
            })
            .ToListAsync();
    }

    public async Task<ShoeDTO> GetShoeByIdAsync(int id)
    {
        var shoe = await _context.Shoes.FindAsync(id);
        if (shoe == null) throw new KeyNotFoundException("Shoe not found");

        return new ShoeDTO
        {
            Id = shoe.Id,
            Reference = shoe.Reference,
            Gender = shoe.Gender,
            Size = shoe.Size,
            Price = shoe.Price,
            Stock = shoe.Stock,
            StoreId = shoe.StoreId
        };
    }

public async Task<List<ShoeDTO>> GetShoesByFiltersAsync(string? gender, string? size, string? reference)
{
    var query = _context.Shoes.AsQueryable();

    if (!string.IsNullOrEmpty(gender))
        query = query.Where(s => s.Gender.Equals(gender, StringComparison.OrdinalIgnoreCase));

    if (!string.IsNullOrEmpty(size) && int.TryParse(size, out int sizeValue))
        query = query.Where(s => s.Size == sizeValue);

    if (!string.IsNullOrEmpty(reference))
        query = query.Where(s => s.Reference.Contains(reference, StringComparison.OrdinalIgnoreCase));

    return await query
        .Select(s => new ShoeDTO
        {
            Id = s.Id,
            Reference = s.Reference,
            Gender = s.Gender,
            Size = s.Size,
            Price = s.Price,
            Stock = s.Stock,
            StoreId = s.StoreId
        })
        .ToListAsync();
}


    public async Task AddShoeAsync(ShoeDTO shoeDto)
    {
        var shoe = new Shoe
        {
            Reference = shoeDto.Reference,
            Gender = shoeDto.Gender,
            Size = shoeDto.Size,
            Price = shoeDto.Price,
            Stock = shoeDto.Stock,
            StoreId = shoeDto.StoreId
        };

        _context.Shoes.Add(shoe);
        await _context.SaveChangesAsync();
    }

    public async Task<ShoeDTO> UpdateShoeAsync(int id, ShoeDTO updatedShoe)
    {
        var shoe = await _context.Shoes.FindAsync(id);
        if (shoe == null) throw new KeyNotFoundException("Shoe not found");

        shoe.Reference = updatedShoe.Reference;
        shoe.Gender = updatedShoe.Gender;
        shoe.Size = updatedShoe.Size;
        shoe.Price = updatedShoe.Price;
        shoe.Stock = updatedShoe.Stock;
        shoe.StoreId = updatedShoe.StoreId;

        _context.Shoes.Update(shoe);
        await _context.SaveChangesAsync();

        return updatedShoe;
    }

    public async Task<ShoeDTO> UpdateShoeStockAsync(int id, int newStock)
    {
        var shoe = await _context.Shoes.FindAsync(id);
        if (shoe == null) throw new KeyNotFoundException("Shoe not found");

        shoe.Stock = newStock;

        _context.Shoes.Update(shoe);
        await _context.SaveChangesAsync();

        return new ShoeDTO
        {
            Id = shoe.Id,
            Reference = shoe.Reference,
            Gender = shoe.Gender,
            Size = shoe.Size,
            Price = shoe.Price,
            Stock = shoe.Stock,
            StoreId = shoe.StoreId
        };
    }

    public async Task<bool> DeleteShoeAsync(int id)
    {
        var shoe = await _context.Shoes.FindAsync(id);
        if (shoe == null) return false;

        _context.Shoes.Remove(shoe);
        await _context.SaveChangesAsync();

        return true;
    }
}
