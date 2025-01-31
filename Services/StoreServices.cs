using Microsoft.EntityFrameworkCore;
using TenisHolly.Data;
using TenisHolly.DTOs;
using TenisHolly.Interface;
using TenisHolly.Models;

namespace TenisHolly.Services
{
    public class StoreServices : IStoreInterface
    {
        private readonly ApplicationDbContext _context;

        public StoreServices(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<StoreDto> Add(StoreDto storeDto)
        {
            var store = new Store
            {
                Name = storeDto.Name,
                Location = storeDto.Location
            };

            _context.Stores.Add(store);
            await _context.SaveChangesAsync();

            storeDto.Id = store.Id;
            return storeDto;
        }

        public async Task<IEnumerable<StoreDto>> GetAll()
        {
            return await _context.Stores
                .Select(store => new StoreDto
                {
                    Id = store.Id,
                    Name = store.Name,
                    Location = store.Location
                })
                .ToListAsync();
        }

        public async Task<StoreDto> GetById(int id)
        {
            return await _context.Stores
                .Where(s => s.Id == id)
                .Select(store => new StoreDto
                {
                    Id = store.Id,
                    Name = store.Name,
                    Location = store.Location
                })
                .FirstOrDefaultAsync();
        }

        public async Task Update(StoreDto storeDto)
        {
            var store = await _context.Stores.FindAsync(storeDto.Id);
            if (store == null)
            {
                throw new Exception("Store not found.");
            }

            store.Name = storeDto.Name;
            store.Location = storeDto.Location;

            _context.Stores.Update(store);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var store = await _context.Stores.FindAsync(id);
            if (store != null)
            {
                _context.Stores.Remove(store);
                await _context.SaveChangesAsync();
            }
        }
    }
}
