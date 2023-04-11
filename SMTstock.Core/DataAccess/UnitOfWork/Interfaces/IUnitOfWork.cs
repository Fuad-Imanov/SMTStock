using Microsoft.EntityFrameworkCore;
using SMTstock.Core.DataAccess.GenericRepository.Interfaces;
using SMTstock.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMTstock.Core.DataAccess.UnitOfWork.Interfaces
{
    public interface IUnitOfWork<TContext> : IDisposable where TContext : DbContext
    {
        IGenericRepository<TEntity> GetRepository<TEntity>() where TEntity : BaseEntity;
        TContext GetDbContext();
        int SaveChanges();
    }
}
