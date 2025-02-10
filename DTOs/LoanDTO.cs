using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TenisHolly.DTOs;
public class LoanDTO
{
    public int ShoeId { get; set; }
    public int FromStoreId { get; set; }
    public int ToStoreId { get; set; }
    public int Quantity { get; set; }
    public DateTime LoanDate { get; set; }
    public DateTime? ReturnDate { get; set; }
    public string Sizes { get; set; }
}
