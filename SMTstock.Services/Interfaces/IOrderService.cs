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

        Task<IDataResult<IEnumerable<OrderGetDto>>> GetAllOrdersAsync(RequestForGetAllOrder request);
        Task<IDataResult<OrderGetDto>> GetOrderByIdAsync(int id);
        Task<IDataResult<OrderGetDto>> AddOrderAsync(OrderCreateDto orderDto);
        Task<IResult> UpdateOrder(int id, OrderUpdateDto orderUpdateDto);
        Task<IResult> RemoveOrder(int id);
    }
}
