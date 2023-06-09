﻿using SMTstock.Entities.DTO.ProductDto;
using SMTstock.Entities.Utilities.Request;
using SMTstock.Entities.Utilities.Results.Abstract;
using SMTstock.Entities.Utilities.Sort.SortProduct;

namespace SMTstock.Services.Interfaces
{
    public interface IProductService
    {

        Task<IDataResult<IEnumerable<ProductDTO>>> GetAllProductsAsync(RequestForGetAllProduct request);
        Task<IDataResult<ProductDTO>> GetProductByIdAsync(int id);
        Task<IDataResult<ProductDTO>> AddProductAsync(AddProductDTO addProductDTO);
        Task<IResult> UpdateProduct(int id, ProductDTO productDTO);
        Task<IResult> RemoveProduct(int id);

    }
}
