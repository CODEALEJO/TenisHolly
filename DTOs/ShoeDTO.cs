using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TenisHolly.DTOs;
public class ShoeDTO
{
    public int Id { get; set; }
    [Required]
    [StringLength(255)]
    public string Reference { get; set; }
    [Required]
    public string Gender { get; set; }
    [Required]
    [Range(1, 10, ErrorMessage = "The size must be a number between 1 and 10.")]
    public int Size { get; set; }
    public decimal Price { get; set; }
    public int Stock { get; set; }
    public int StoreId { get; set; }

}