using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TenisHolly.DTOs;
public class SaleDTO
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public int ShoeId { get; set; }
    public int Quantity { get; set; }
    public string PaymentMethod { get; set; }
    public string Seller { get; set; }
    public int StoreId { get; set; }//duda
    public decimal Total { get; set; }
}
