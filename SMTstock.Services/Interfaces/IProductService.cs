using SMTstock.Entities.DTO;
using SMTstock.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMTstock.Services.Interfaces
{
    public interface IProductService
    {
        Task<ProductDTO> GetProductByIdAsync(int id);
        Task<IEnumerable<ProductDTO>> GetAllProductsAsync();
        Task<ProductDTO> AddProductAsync(AddProductDto addProductDto);
        Task<bool> UpdateProduct(int id ,ProductDTO productDto);
        Task RemoveProduct(int id);
        //IEnumerable<Product> GetProducts();
        //Product GetProductByIdAsync(int productId);
        //void AddProductAsync(Product product);
        //void UpdateProduct(Product product);
        //void DeleteProduct(Product product);
    }
}
