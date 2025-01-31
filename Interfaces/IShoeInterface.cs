using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TenisHolly.DTOs;

namespace TenisHolly.Interfaces;
public interface IShoeInterface
{
    // Get all shoes
    Task<List<ShoeDTO>> GetAllShoesAsync();

    // Get shoe by ID
    Task<ShoeDTO> GetShoeByIdAsync(int id);

    // Filtered searches
    Task<List<ShoeDTO>> GetShoesByFiltersAsync(string? gender, string? size, string? reference);

    // Add a new shoe
    Task AddShoeAsync(ShoeDTO shoe);

    // Update shoe details
    Task<ShoeDTO> UpdateShoeAsync(int id, ShoeDTO updatedShoe);

    // Update stock
    Task<ShoeDTO> UpdateShoeStockAsync(int id, int newStock);

    // Delete shoe
    Task<bool> DeleteShoeAsync(int id);
}
