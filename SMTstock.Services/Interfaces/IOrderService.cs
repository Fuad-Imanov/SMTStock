using SMTstock.Core.DataAccess.GenericRepository.Interfaces;
using SMTstock.Entities.DTO;
using SMTstock.Entities.DTO.OrderDto;
using SMTstock.Entities.Models;
using SMTstock.Entities.Utilities.Request;
using SMTstock.Entities.Utilities.Results.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMTstock.Services.Interfaces
{
    public interface IOrderService
    {
        Task<IDataResult<Order>> GetOrderByIdAsync(int id);
        //Task<IDataResult<IEnumerable<Order>>> GetAllOrdersAsync(Order order);
        List<OrderGetDto> GetAllOrdersAsync();
        Task<IDataResult<Order>> AddOrderAsync(OrderCreateDto orderDto);
        Task<IResult> UpdateOrder(int id, Order order);
        Task<IResult> RemoveOrder(int id);
    }
}
