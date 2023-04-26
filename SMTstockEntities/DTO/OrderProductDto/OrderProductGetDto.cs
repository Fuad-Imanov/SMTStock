using SMTstock.Entities.DTO;
using SMTstock.Entities.DTO.ProductDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMTstock.Entities.DTO.OrderProductDto
{
    public class OrderProductGetDTO
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public double ProductPrice { get; set; }
        public int Quantity { get; set; }
        public double? TotalPrice { get; set; }
        public ProductDTO ProductDTO { get; set; }
    }
}
