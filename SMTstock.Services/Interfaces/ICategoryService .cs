using SMTstock.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMTstock.Services.Interfaces
{
    public interface ICategoryService
    {
        IEnumerable<Category> GetCategories();
        Task<Category> GetCategoryByIdAsync(int categoryId);
        Task AddCategoryAsync(Category category);
        void UpdateCategory(Category category);
        void DeleteCategory(Category category);
    }
}
