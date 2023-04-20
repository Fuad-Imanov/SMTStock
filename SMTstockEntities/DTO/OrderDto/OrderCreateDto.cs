using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SMTstock.Entities.DTO.OrderProduct;

namespace SMTstock.Entities.DTO.OrderDto
{
    public class OrderCreateDto
    {
        public int MerchantId { get; set; }
        public ICollection<OrderProductCreateDto> OrdersProducts { get; set; }
    }
}
