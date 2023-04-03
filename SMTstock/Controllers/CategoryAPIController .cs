//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using SMTstock.Core.DataAccess.UnitOfWork.Interfaces;
//using SMTstock.DAL.Context;
//using SMTstock.Entities.Models;
//using SMTstock.Services.Implementations;
//using SMTstock.Services.Interfaces;

//namespace SMTstock.Controllers
//{
//    //[Route("api/[controller]")]
//    [Route("api/CategoryAPI")]
//    [ApiController]
//    public class CategoryAPIController : ControllerBase
//    {

//        private readonly ICategoryService _categoryService;
//        private readonly IUnitOfWork<ApplicationDbContext> _unitOfWork;

//        public CategoryAPIController(ICategoryService categoryService, IUnitOfWork<ApplicationDbContext> unitOfWork)
//        {
//            _categoryService = categoryService;
//            _unitOfWork = unitOfWork;
//        }

//        [HttpGet("GetCategories")]
//        public IActionResult Get()
//        {
//            var categories = _categoryService.GetCategories();

//            return Ok(categories);
//        }

//        [HttpGet("GetCategoryById/{id}")]
//        public async Task<IActionResult> GetAsync(int id)
//        {
//            var category = await _categoryService.GetCategoryByIdAsync(id);

//            if (category == null)
//            {
//                return NotFound();
//            }

//            return Ok(category);
//        }
//        [HttpPost("AddCategory")]
//        public async Task<IActionResult> AddCategory(Category category)
//        {
//            if (ModelState.IsValid)
//            {
//                await _categoryService.AddCategoryAsync(category);
//                _unitOfWork.SaveAsync();

//                return CreatedAtAction(nameof(Get), new { id = category.Id }, category);
//            }

//            return BadRequest(ModelState);
//        }

//        [HttpPut("UpdateCategory/{id}")]
//        public async Task<IActionResult> UpdateCategoryAsync(int id, [FromBody] Category category)
//        {
//            if (category == null || id != category.Id)
//            {
//                return BadRequest();
//            }
//            if (ModelState.IsValid)
//            {
//                try
//                {
//                    category = await _categoryService.GetCategoryByIdAsync(id);
//                    _categoryService.UpdateCategory(category);
//                    _unitOfWork.SaveAsync();
//                }
//                catch (DbUpdateConcurrencyException)
//                {
//                    if (_categoryService.GetCategoryByIdAsync(id) == null)
//                    {
//                        return NotFound();
//                    }

//                    throw;
//                }

//                return NoContent();
//            }

//            return BadRequest(ModelState);
//        }

//        [HttpDelete("DeleteCategory/{id}")]
//        [ProducesResponseType(204)]
//        [ProducesResponseType(404)]
//        public async Task<IActionResult> DeleteAsync(int id)
//        {
//            var category = await _categoryService.GetCategoryByIdAsync(id);

//            if (category == null)
//            {
//                return NotFound();
//            }

//            _categoryService.DeleteCategory(category);
//            _unitOfWork.SaveAsync();

//            return NoContent();
//        }

//    }

//}
