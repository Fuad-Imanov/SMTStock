using SMTstock.Entities.DTO.OrderDto;
using SMTstock.Entities.Utilities.Request;
using SMTstock.Entities.Utilities.Results.Abstract;

namespace SMTstock.Services.Interfaces
{
    public interface IOrderService
    {

        Task<IDataResult<IEnumerable<OrderGetDTO>>> GetAllOrdersAsync(RequestForGetAllOrder request);
        Task<IDataResult<OrderGetDTO>> GetOrderByIdAsync(int id);
        Task<IDataResult<OrderGetDTO>> AddOrderAsync(OrderCreateDTO orderDTO);
        Task<IResult> UpdateOrder(int id, OrderUpdateDTO orderUpdateDTO);
        Task<IResult> RemoveOrder(int id);
    }
}
