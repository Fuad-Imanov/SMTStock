using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMTstock.Entities.DTO.OrderProduct
{
    public class OrderProductUpdateDto
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public double? TotalPrice { get; set; }
        public int Quantity { get; set; }
    }
}
