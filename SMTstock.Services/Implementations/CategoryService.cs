using SMTstock.Core.DataAccess.UnitOfWork.Interfaces;
using SMTstock.Entities.Models;
using SMTstock.Services.Interfaces;
using SMTstock.DAL.Context;
using SMTstock.Core.DataAccess.GenericRepository.Interfaces;
using SMTstock.Entities.Utilities.Results.Abstract;
using AutoMapper;
using SMTstock.Entities.Utilities.Results.Concrete;
using SMTstock.Entities.DTO.CategoryDto;

namespace SMTstock.Services.Implementations
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork<ApplicationDbContext> _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IGenericRepository<Category> _categoryRepository;
        public CategoryService(IUnitOfWork<ApplicationDbContext> unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _categoryRepository = _unitOfWork.GetRepository<Category>();

        }


        public async Task<IDataResult<IEnumerable<CategoryDTO>>> GetAllCategoriesAsync()
        {
            List<Category> categories = _categoryRepository.GetAll().ToList();
            //mapping
            List<CategoryDTO> categoriesDTO = new List<CategoryDTO>();

            if (categories is not null && categories.Count != 0)
            {
                categoriesDTO = _mapper.Map<List<CategoryDTO>>(categories);
            }

            //Create and return Response
            return (categoriesDTO is not null && categoriesDTO.Count != 0)
                ? new SuccessDataResult<IEnumerable<CategoryDTO>>(categoriesDTO, "Categories was successfully delivered.")
                : new ErrorDataResult<IEnumerable<CategoryDTO>>(categoriesDTO, "Category not found.");
        }

        public async Task<IDataResult<CategoryDTO>> GetCategoryByIdAsync(int id)
        {

            var category = await _categoryRepository.GetByIdAsync(id, false);
            var CategoryDTO = _mapper.Map<CategoryDTO>(category);
            return (CategoryDTO is not null)
                 ? new SuccessDataResult<CategoryDTO>(CategoryDTO, "Category succesfully get!")
                : new ErrorDataResult<CategoryDTO>("Category not found");
        }

        public async Task<IDataResult<CategoryDTO>> AddCategoryAsync(AddCategoryDTO addCategoryDTO)
        {
            var category = _mapper.Map<Category>(addCategoryDTO);
            var categoryAdd = await _categoryRepository.AddAsync(category);
            if (!categoryAdd)
            {
                return new ErrorDataResult<CategoryDTO>("Error when add category");
            }
            var save = _unitOfWork.SaveChanges();
            if (save != 0)
            {
                var createdCategoryDTO = _mapper.Map<CategoryDTO>(category);
                return new SuccessDataResult<CategoryDTO>(createdCategoryDTO, "Category succesfully created!");
            }
            else
            {
                return new ErrorDataResult<CategoryDTO>("Category not created");
            }
        }


        public async Task<IResult> UpdateCategory(int id, CategoryDTO CategoryDTO)
        {
            var category = await _categoryRepository.GetByIdAsync(id, false);

            if (category == null)
            {
                return new ErrorResult("Category not found");
            }
            category = _mapper.Map<Category>(CategoryDTO);
            if (await _categoryRepository.Update(category))
            {
                var save = _unitOfWork.SaveChanges();
                if (save != 0)
                {
                    return new SuccessResult("Category succesfully update");
                }
                return new ErrorResult("Category not update");
            }

            return new ErrorResult("Category not update");
        }
        public async Task<IResult> RemoveCategory(int id)
        {

            var category = await _categoryRepository.GetByIdAsync(id, false);
            if (category == null)
            {
                return new ErrorResult("Category not found");
            }
            if (await _categoryRepository.Remove(category))
            {
                var save = _unitOfWork.SaveChanges();
                if (save != 0)
                {
                    return new SuccessResult("Category succesfully deleted");
                }
                return new ErrorResult("Category not deleted");
            }
            return new ErrorResult("Category not deleted");

        }
    }
}
