using SMTstock.Core.DataAccess.GenericRepository.Interfaces;
using SMTstock.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMTstock.Services.Interfaces
{
    public interface IOrderService
    {
        IEnumerable<Order> GetOrders();
        Task<Order> GetOrderByIdAsync(int orderId);
        Task AddOrderAsync(Order order);
        void UpdateOrder(Order order);
        void DeleteOrder(Order order);
    }
}
