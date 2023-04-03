using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SMTstock.Core.DataAccess.UnitOfWork.Interfaces;
using SMTstock.DAL.Context;
using SMTstock.Entities.DTO;
using SMTstock.Entities.Models;
using SMTstock.Services.Implementations;
using SMTstock.Services.Interfaces;

namespace SMTstock.Controllers
{
    //[Route("api/[controller]")]
    [Route("api/ProductAPI")]
    [ApiController]
    public class ProductAPIController : ControllerBase
    {

        private readonly IProductService _productService;
        //private readonly IUnitOfWork<ApplicationDbContext> _unitOfWork;

        public ProductAPIController(IProductService productService /*,IUnitOfWork<ApplicationDbContext> unitOfWork*/)
        {
            _productService = productService;
            //_unitOfWork = unitOfWork;
        }


        [HttpGet("GetProducts")]
        [ProducesResponseType(StatusCodes.Status200OK)]//Experiment ucun yazilib,olmasa da olar
        public async Task<IActionResult> GetAsync()
        {
            var products = await _productService.GetAllProductsAsync();

            return Ok(products);
        }


        [HttpGet("GetProductById/{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }


        [HttpPost("AddProduct")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> AddProductAsync(AddProductDto addProductDTO)
        {


            if (ModelState.IsValid)
            {

                ProductDTO createdProduct  = await _productService.AddProductAsync(addProductDTO);
                return CreatedAtAction(nameof(GetByIdAsync), new { id = createdProduct.Id }, createdProduct);
            }

            return BadRequest(ModelState);


        }

        [HttpPut("UpdateProduct/{id:int}")]
        public async Task<IActionResult> UpdateProductAsync(int id, [FromBody] ProductDTO productDTO)
        {
            if (productDTO == null || id != productDTO.Id)
            {
                return BadRequest();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    await _productService.UpdateProduct(id, productDTO);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (_productService.GetProductByIdAsync(id) == null)
                    {
                        return NotFound();
                    }

                    throw;
                }

                return NoContent();
            }

            return BadRequest(ModelState);
        }



        [HttpDelete("DeleteProduct/{id:int}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            await _productService.RemoveProduct(id);

            return NoContent();
        }

    }

}
