using Microsoft.EntityFrameworkCore;
using SMTstock.Core.DataAccess;
using SMTstock.Core.DataAccess.GenericRepository.Impelementations;
using SMTstock.Core.DataAccess.GenericRepository.Interfaces;
using SMTstock.Core.DataAccess.UnitOfWork.Interfaces;
using SMTstock.Entities.Models;

namespace SMTstock.Core.DataAccess.UnitOfWork.Implementations
{
    public class UnitOfWork<TContext> : IUnitOfWork<TContext> where TContext : DbContext, new()
    {
        private readonly TContext _context;
        private readonly Dictionary<Type, object> _repositories = new Dictionary<Type, object>();

        public UnitOfWork()
        {
            _context = new TContext();
        }

        public IGenericRepository<TEntity> GetRepository<TEntity>() where TEntity : BaseEntity
        {
            if (_repositories.TryGetValue(typeof(TEntity), out object repository))
            {
                return (IGenericRepository<TEntity>)repository;
            }

            IGenericRepository<TEntity> newRepository = new GenericRepository<TEntity>(_context);
            _repositories.Add(typeof(TEntity), newRepository);
            return newRepository;
        }

        public TContext GetDbContext()
        {
            return _context;
        }

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
    //public class UnitOfWork : IUnitOfWork
    //{
    //    private readonly DbContext _dbContext;
    //    private readonly Dictionary<Type, object> _repositories;

    //    public UnitOfWork()
    //    {
    //        _context = new TContext();
    //    }
    //    //public UnitOfWork()
    //    //{

    //    //}

    //    public IGenericRepository<TEntity> GetRepository<TEntity>() where TEntity : class
    //    {
    //        if (_repositories.TryGetValue(typeof(TEntity), out object repository))
    //        {
    //            return (IGenericRepository<TEntity>)repository;
    //        }

    //        var newRepository = new GenericRepository<TEntity>(_dbContext);
    //        _repositories.Add(typeof(TEntity), newRepository);
    //        return newRepository;
    //    }

    //    public async Task<int> SaveChangesAsync()
    //    {
    //        return await _dbContext.SaveChangesAsync();
    //    }

    //    public void Dispose()
    //    {
    //        _dbContext.Dispose();
    //    }
    //}

//public class UnitOfWork<TContext> : IUnitOfWork<TContext> where TContext : DbContext, new()
//{
//    private readonly TContext _context;
//    private readonly Dictionary<Type, object> _repositories;

//    public UnitOfWork()
//    {
//        _context = new TContext();
//        _repositories = new Dictionary<Type, object>();
//    }

//    public IRepository<TEntity> GetRepository<TEntity>() where TEntity : class
//    {
//        if (_repositories.ContainsKey(typeof(TEntity)))
//        {
//            return (IRepository<TEntity>)_repositories[typeof(TEntity)];
//        }

//        var repository = new Repository<TEntity>(_context);
//        _repositories.Add(typeof(TEntity), repository);
//        return repository;
//    }

//    public TContext GetDbContext()
//    {
//        return _context;
//    }

//    public int SaveChanges()
//    {
//        return _context.SaveChanges();
//    }

//    public void Dispose()
//    {
//        _context.Dispose();
//    }
//}
