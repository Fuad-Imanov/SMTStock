using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMTstock.Entities.DTO.OrderDto
{
    public class OrderGetDto
    {
        public int Id { get; set; }
        public int MerchantId { get; set; }
        public double? OrderTotalPrice { get; set; }
        public DateTime OrderDate { get; set; }
        public ICollection<OrderProductGetDto> OrdersProducts { get; set; }
    }
}
