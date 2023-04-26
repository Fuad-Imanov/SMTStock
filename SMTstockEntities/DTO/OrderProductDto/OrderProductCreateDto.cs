using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMTstock.Entities.DTO.OrderProductDto
{
    public class OrderProductCreateDTO
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
