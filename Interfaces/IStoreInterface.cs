using System.Collections.Generic;
using System.Threading.Tasks;
using TenisHolly.DTO;
using TenisHolly.DTOs;

namespace TenisHolly.Interface
{
    public interface IStoreInterface
    {
        Task<StoreDto> Add(StoreDto storeDto);
        Task<IEnumerable<StoreDto>> GetAll();
        Task<StoreDto> GetById(int id);
        Task Update(StoreDto storeDto);
        Task Delete(int id);
    }
}
