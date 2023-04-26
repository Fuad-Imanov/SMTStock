

using SMTstock.Entities.DTO.OrderProductDto;

namespace SMTstock.Entities.DTO.OrderDto
{
    public class OrderCreateDTO
    {
        public int MerchantId { get; set; }
        public ICollection<OrderProductCreateDTO> OrdersProducts { get; set; }
    }
}
