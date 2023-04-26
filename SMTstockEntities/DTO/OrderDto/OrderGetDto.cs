
using SMTstock.Entities.DTO.OrderProductDto;

namespace SMTstock.Entities.DTO.OrderDto
{
    public class OrderGetDTO
    {
        public int Id { get; set; }
        public int MerchantId { get; set; }
        public double? OrderTotalPrice { get; set; }
        public DateTime OrderDate { get; set; }
        public ICollection<OrderProductGetDTO> OrdersProducts { get; set; }
    }
}
