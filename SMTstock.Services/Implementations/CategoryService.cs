//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.Logging;
//using SMTstock.Core.DataAccess.GenericRepository.Impelementations;
//using SMTstock.Core.DataAccess.UnitOfWork.Interfaces;
//using SMTstock.DAL.Repository.Impelementations;
//using SMTstock.DAL.Repository.Interfaces;
//using SMTstock.Entities.Models;
//using SMTstock.Services.Interfaces;
//using SMTstock.DAL.Context;
//using SMTstock.Core.DataAccess.GenericRepository.Interfaces;

//namespace SMTstock.Services.Implementations
//{
//    public class CategoryService : ICategoryService
//    {
//        private UnitOfWork<ApplicationDbContext> unitOfWork = new UnitOfWork<ApplicationDbContext>();
//        private IRepository<Category> _repository;
//        /*Category table-na aid xususi metodlar olsa bu koddan istifade etmek olar
//        private ICategoryRepository CategoryRepository;*/
//        public CategoryService()
//        {
//            //If you want to use Generic Repository with Unit of work
//            _repository = unitOfWork.GetRepository<Category>();
//            //If you want to use Specific Repository with Unit of work
//            //CategoryRepository = new CategoryRepository(unitOfWork);
//        }

//        public  void AddCategoryAsync(Category category)
//        {
//            _repository.Add(category);
//        }

//        public void DeleteCategory(Category category)
//        {
//            _repository.Delete(category);
            
//        }

//        public async Task<Category> GetCategoryByIdAsync(int orderId)
//        {
//            return await _repository.GetByIdAsync(orderId);
//        }

//        public IEnumerable<Category> GetCategories()
//        {
//            return _repository.GetAll();
//        }

//        public void UpdateCategory(Category category)
//        {
//            _repository.Update(category);
//        }
//    }
//}
