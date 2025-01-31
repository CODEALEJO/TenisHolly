using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TenisHolly.Models;
[Table("sales")]
public class Sale
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("id")]
    public int Id { get; set; }
    [Column("date")]
    public DateTime Date { get; set; }
    [Column("quantity")]
    public int Quantity { get; set; }
    [Column("total")]
    public decimal Total { get; set; } // Cantidad * Precio
    [Column("payment_method")]
    public string PaymentMethod { get; set; } // Tarjeta, Efectivo, Transferencia
    [Column("seller")]
    public string Seller { get; set; }
    [Column("shoe_id")]
    public int ShoeId { get; set; }
    [ForeignKey("ShoeId")]
    public Shoe Shoe { get; set; }
    [Column("store_id")]
    public int StoreId { get; set; }
    [ForeignKey("StoreId")]
    public Store Store { get; set; }
}
