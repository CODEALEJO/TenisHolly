using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TenisHolly.DTO;
public class RegisterUserDto
{
    [Required]
    [StringLength(255)]
    public string FullName { get; set; }
    [Required]
    [DataType(DataType.EmailAddress)]
    [EmailAddress]
    [StringLength(255)]
    public string Email { get; set; }
    [Required]
    public string PasswordHash { get; set; }
    public string? Role { get; set; }
}