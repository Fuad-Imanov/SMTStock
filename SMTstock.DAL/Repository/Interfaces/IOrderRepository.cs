using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SMTstock.Core.DataAccess.GenericRepository.Interfaces;
using SMTstock.DAL.Context;
using SMTstock.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMTstock.DAL.Repository.Interfaces
{
    public interface IOrderRepository : IGenericRepository<Order>
    {
        Task<Order> GetOrderByIdAsync(int id,bool tracking);
    }
}
