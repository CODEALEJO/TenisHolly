using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TenisHolly.DTOs;
public class LoanResponseDTO
{
    public string ShoeReference { get; set; }
    public string FromStoreName { get; set; }
    public string ToStoreName { get; set; }
    public int Quantity { get; set; }
    public DateTime LoanDate { get; set; }
    public DateTime? ReturnDate { get; set; }
    public List<SizeDetailDTO> Sizes { get; set; }
    public string Status { get; set; }
}