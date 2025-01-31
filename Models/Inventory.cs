using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TenisHolly.Models;

[Table("inventoryes")]
public class Inventory
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("id")]
    public int Id { get; set; }

    [Column("quantity")]
    public int Quantity { get; set; }

    [Column("store_id")]
    public int StoreId { get; set; }

    [ForeignKey("StoreId")]
    public Store Store { get; set; }

    [Column("shoe_id")]
    public int ShoeId { get; set; }

    [ForeignKey("ShoeId")]
    public Shoe Shoe { get; set; } // Solo una referencia al Shoe
}
