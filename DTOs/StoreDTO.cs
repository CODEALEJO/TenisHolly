using System.ComponentModel.DataAnnotations;

namespace TenisHolly.DTOs
{
    public class StoreDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "The store name is required.")]
        [MaxLength(100, ErrorMessage = "The store name must be at most {1} characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "The store location is required.")]
        [MaxLength(200, ErrorMessage = "The store location must be at most {1} characters.")]
        public string Location { get; set; }
    }
}
