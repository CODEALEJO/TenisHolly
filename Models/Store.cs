using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TenisHolly.Models;
[Table("stores")]
public class Store
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("id")]
    public int Id { get; set; }
    [Column("name")]
    public string Name { get; set; }
    [Column("location")]
    public string Location { get; set; } // Dirección o detalle
    public ICollection<Shoe> Shoes { get; set; } = new List<Shoe>();
    public ICollection<Sale> Sales { get; set; } = new List<Sale>();

    [InverseProperty("FromStore")]
    public ICollection<Loan> LoansGiven { get; set; } = new List<Loan>(); // Préstamos hechos

    [InverseProperty("ToStore")]
    public ICollection<Loan> LoansReceived { get; set; } = new List<Loan>(); // Préstamos recibidos

}
