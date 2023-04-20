using Microsoft.EntityFrameworkCore;
using SMTstock.Core.DataAccess.GenericRepository.Impelementations;
using SMTstock.DAL.Context;
using SMTstock.DAL.Repository.Interfaces;
using SMTstock.Entities.Models;

namespace SMTstock.DAL.Repository.Impelementations
{

    public class OrderRepository : GenericRepository<Order>, IOrderRepository
    {
        private readonly ApplicationDbContext _context;
        public OrderRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Order> GetOrderByIdAsync(int id,bool tracking)
        {
            if(tracking)
            {
                return await _context.Set<Order>().Include(o => o.OrdersProducts).ThenInclude(op => op.Product)
                .SingleOrDefaultAsync(o => o.Id == id);
            }
            return  await _context.Set<Order>().AsNoTracking().Include(o => o.OrdersProducts).ThenInclude(op => op.Product)
                .SingleOrDefaultAsync(o => o.Id == id);
        }
    }
}
