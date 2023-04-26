using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SMTstock.Entities.DTO.ProductDto;
using SMTstock.Entities.Utilities;
using SMTstock.Entities.Utilities.Request;
using SMTstock.Entities.Utilities.Sort.SortProduct;
using SMTstock.Services.Interfaces;
using System.Xml.Linq;

namespace SMTstock.Controllers
{
    //[Route("api/[controller]")]
    [Route("api/ProductAPI")]
    [ApiController]
    public class ProductAPIController : ControllerBase
    {

        private readonly IProductService _productService;

        public ProductAPIController(IProductService productService)
        {
            _productService = productService;
        }


        //[HttpGet("GetProducts")]
        //[ProducesResponseType(StatusCodes.Status200OK)]//Experiment ucun yazilib,olmasa da olar
        //[HttpGet]
        //public async Task<IActionResult> GetAsync([FromQuery(Name = "sortFields")][ModelBinder(typeof(SortFieldsModelBinder))] List<SortFieldForProduct> sortFields,int count,string searchtext)
        //{
        //    return Ok(sortFields);
        //}

        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery]RequestForGetAllProduct request)
        {
            var result = await _productService.GetAllProductsAsync(request);
            return Ok(result);
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
        public async Task<IActionResult> AddProductAsync(AddProductDTO addProductDTO)
        {


            if (ModelState.IsValid)
            {

                var result = await _productService.AddProductAsync(addProductDTO);
                return CreatedAtAction(nameof(GetByIdAsync), new { id = result.Data.Id }, result);
            }

            return BadRequest(ModelState);


        }

        [HttpPut("UpdateProduct/{id:int}")]
        public async Task<IActionResult> UpdateProductAsync(int id, [FromBody]ProductDTO productDTO)
        {
            if (productDTO == null || id != productDTO.Id)
            {
                return BadRequest();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    var result = await _productService.UpdateProduct(id, productDTO);
                    return Ok(result);
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
            var result = await _productService.RemoveProduct(id);

            return Ok(result);
        }

    }

}
