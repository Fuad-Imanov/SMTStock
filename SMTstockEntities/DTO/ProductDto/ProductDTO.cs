using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMTstock.Entities.DTO.ProductDto
{
    public class ProductDTO
    {
        public int Id { get; set; }
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
