using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SMTstock.Core.DataAccess.GenericRepository.Interfaces;
using SMTstock.Core.DataAccess.UnitOfWork.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SMTstock.Core.DataAccess.GenericRepository.Impelementations
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        private readonly DbContext _context;
        private readonly DbSet<TEntity> _dbSet;

        public GenericRepository(DbContext context)
        {
            _context = context;
            _dbSet = _context.Set<TEntity>();
        }

        public async Task<TEntity> GetByIdAsync(int id)
        {
            var product = await _dbSet.FindAsync(id);
            return product;
        }

        public async Task<TEntity> GetByIdAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _dbSet.AsNoTracking().FirstOrDefaultAsync(predicate);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _dbSet.Where(predicate).ToListAsync();
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
            return entity;
        }

        public async Task AddRangeAsync(IEnumerable<TEntity> entities)
        {
            await _dbSet.AddRangeAsync(entities);
        }

        public async Task<bool> Update(TEntity entity)
        {

            var entry = _dbSet.Update(entity);
            return entry.State == EntityState.Modified;

        }

        public async Task Remove(TEntity entity)
        {
            _dbSet.Remove(entity);
        }
    }
}