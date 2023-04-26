using System.ComponentModel.DataAnnotations;

namespace SMTstock.Entities.DTO.ProductDto
{
    public class AddProductDTO
    {
        [Required]
        public string ProductName { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int Balance { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public double Price { get; set; }

        [Required]
        public int CategoryId { get; set; }
    }
}
