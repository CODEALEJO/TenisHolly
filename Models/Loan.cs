using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TenisHolly.Models;

[Table("loans")]
public class Loan
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("id")]
    public int Id { get; set; }

    [Column("loan_date")]
    public DateTime LoanDate { get; set; }

    [Column("return_date")]
    public DateTime? ReturnDate { get; set; } // Opcional si el préstamo regresa

    [Column("quantity")]
    public int Quantity { get; set; }

    [Column("from_store_id")]
    public int FromStoreId { get; set; }

    [ForeignKey("FromStoreId")]
    public Store FromStore { get; set; }

    [Column("to_store_id")]
    public int ToStoreId { get; set; }

    [ForeignKey("ToStoreId")]
    public Store ToStore { get; set; }

    [Column("shoe_id")]
    public int ShoeId { get; set; }

    [ForeignKey("ShoeId")]
    public Shoe Shoe { get; set; }

    
    [Column("sizes")]
    public string Sizes { get; set; } 

    // Estado del préstamo
    [Column("status")]
    public LoanStatus Status { get; set; }

    public enum LoanStatus
{
    Prestado,  
    PorPagar,      
    Pago       
}

}
