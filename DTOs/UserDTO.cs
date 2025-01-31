using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TenisHolly.DTO;
public class UserDto
{
    
    public int Id { get; set; }
    [Required]
    [StringLength(255)]
    public string FullName { get; set; }

    [Required]
    [DataType(DataType.EmailAddress)]
    [EmailAddress]
    [StringLength(255)]
    public string Email { get; set; }

    [Required]
    [StringLength(20)]
    public string PasswordHash { get; set; }

    [Required]
    public string Role { get; set; }
}