using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SMTstock.Core.DataAccess;
using SMTstock.Core.DataAccess.GenericRepository.Impelementations;
using SMTstock.Core.DataAccess.GenericRepository.Interfaces;
using SMTstock.Core.DataAccess.UnitOfWork.Interfaces;
using SMTstock.DAL.Context;
using SMTstock.DAL.Repository.Interfaces;
using SMTstock.Entities.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMTstock.DAL.Repository.Impelementations
{
    //public class OrderRepository : GenericRepository<Order>, IOrderRepository
    //{
       
    //}
    //public class OrderRepository : IRepository<Order>, IOrderRepository
    //{
    //    public OrderRepository(IUnitOfWork<ApplicationDbContext> unitOfWork)
           
    //    {
    //    }
    //    public OrderRepository(ApplicationDbContext context)
    //    {
    //    }

    //    public void Add(Order entity)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public void Delete(Order entity)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public IEnumerable<Order> GetAll()
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public Order GetById(int id)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public void Update(Order entity)
    //    {
    //        throw new NotImplementedException();
    //    }
    //}
    
}
