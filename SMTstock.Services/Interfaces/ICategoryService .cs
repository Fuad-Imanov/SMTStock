using SMTstock.Entities.DTO.CategoryDto;
using SMTstock.Entities.DTO.ProductDto;
using SMTstock.Entities.Models;
using SMTstock.Entities.Utilities.Request;
using SMTstock.Entities.Utilities.Results.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMTstock.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<IDataResult<IEnumerable<CategoryDTO>>> GetAllCategoriesAsync();
        Task<IDataResult<CategoryDTO>> GetCategoryByIdAsync(int id);
        Task<IDataResult<CategoryDTO>> AddCategoryAsync(AddCategoryDTO addProductDto);
        Task<IResult> UpdateCategory(int id, CategoryDTO productDto);
        Task<IResult> RemoveCategory(int id);
    }
}
