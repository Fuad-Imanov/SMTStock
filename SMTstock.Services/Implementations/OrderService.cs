//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.Logging;
//using SMTstock.Core.DataAccess.GenericRepository.Impelementations;
//using SMTstock.Core.DataAccess.UnitOfWork.Interfaces;
//using SMTstock.DAL.Repository.Impelementations;
//using SMTstock.DAL.Repository.Interfaces;
//using SMTstock.Entities.Models;
//using SMTstock.Services.Interfaces;
//using SMTstock.Core.DataAccess.UnitOfWork.Implementations;
//using SMTstock.DAL.Context;

//namespace SMTstock.Services.Implementations
//{
//    public class OrderService : IOrderService
//    {
//        private UnitOfWork<ApplicationDbContext> unitOfWork = new UnitOfWork<ApplicationDbContext>();
//        private GenericRepository<Order, ApplicationDbContext> _repository;
//        /*Order table-na aid xususi metodlar olsa bu koddan istifade etmek olar
//        private IOrderRepository orderRepository;*/
//        public OrderService()
//        {
//            //If you want to use Generic Repository with Unit of work
//            _repository = new GenericRepository<Order,ApplicationDbContext>(unitOfWork);
//            //If you want to use Specific Repository with Unit of work
//            //orderRepository = new OrderRepository(unitOfWork);
//        }

//        public async Task AddOrderAsync(Order order)
//        {
//           await _repository.InsertAsync(order);
//        }

//        public void DeleteOrder(Order order)
//        {
//            _repository.Delete(order);
            
//        }

//        public async Task<Order> GetOrderByIdAsync(int orderId)
//        {
//            return await _repository.GetByIdAsync(orderId);
//        }

//        public IEnumerable<Order> GetOrders()
//        {
//            return _repository.GetAll().ToList();
//        }

//        public void UpdateOrder(Order order)
//        {
//            _repository.Update(order);
//        }
//    }
//}
