using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TenisHolly.Models;

[Table("shoes")]
public class Shoe
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("id")]
    public int Id { get; set; }

    [Column("reference")]
    public string Reference { get; set; } // Nombre o referencia del zapato

    [Column("gender")]
    public string Gender { get; set; } // Dama o Caballero

    [Column("size")]
    public int Size { get; set; } // Talla

    [Column("price")]
    public decimal Price { get; set; } // Precio

    [Column("stock")]
    public int Stock { get; set; } // Cantidad disponible

    [Column("store_id")]
    public int StoreId { get; set; }

    [ForeignKey("StoreId")]
    public Store Store { get; set; }

    public ICollection<Sale> Sales { get; set; } = new List<Sale>(); // Un zapato puede estar en muchas ventas

    public ICollection<Loan> Loans { get; set; } = new List<Loan>(); // Un zapato puede estar en muchos préstamos

    public ICollection<Inventory> Inventories { get; set; } = new List<Inventory>(); // Un zapato puede estar en el inventario de varias tiendas
}
