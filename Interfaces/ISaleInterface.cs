using TenisHolly.DTOs;

namespace TenisHolly.Interfaces;
public interface ISaleInterface
{
    Task<SaleDTO> CreateSaleAsync(SaleDTO saleDto);
    Task<List<SaleDTO>> GetSalesByStoreAsync(int storeId);
    Task<List<SaleDTO>> GetSalesByDateAsync(int storeId, DateTime date);
    Task<decimal> GetDailyTotalAsync(int storeId, DateTime date);

}
