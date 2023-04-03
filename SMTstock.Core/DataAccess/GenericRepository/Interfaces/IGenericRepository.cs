using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SMTstock.Core.DataAccess.GenericRepository.Interfaces
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        Task<TEntity> GetByIdAsync(int id);
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<TEntity> GetByIdAsync(Expression<Func<TEntity, bool>> predicate);
        Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate);

        Task<TEntity> AddAsync(TEntity entity);
        Task AddRangeAsync(IEnumerable<TEntity> entities);
        Task<bool> Update(TEntity entity);
        Task Remove(TEntity entity);
        //IEnumerable<TEntity> GetAll();
        //TEntity GetById(int id);
        //void Add(TEntity entity);
        //void Update(TEntity entity);
        //void Delete(TEntity entity);
    }
}
