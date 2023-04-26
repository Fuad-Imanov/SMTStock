using System.Linq;
using Microsoft.EntityFrameworkCore;
using SMTstock.Core.DataAccess.UnitOfWork.Interfaces;
using SMTstock.Entities.Models;
using SMTstock.Services.Interfaces;
using SMTstock.DAL.Context;
using SMTstock.Core.DataAccess.GenericRepository.Interfaces;
using AutoMapper;
using SMTstock.Entities.Utilities.Results.Abstract;
using SMTstock.Entities.Utilities.Results.Concrete;
using SMTstock.Entities.Utilities.Sort;
//using System.Xml.Linq;
using SMTstock.Entities.Utilities.Pagination;
using SMTstock.Entities.Utilities.Sort.SortProduct;
using SMTstock.Entities.Utilities.Request;
using System.Collections.Generic;
using SMTstock.Entities.DTO.ProductDto;

namespace SMTstock.Services.Implementations
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork<ApplicationDbContext> _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IGenericRepository<Product> _productRepository;
        public ProductService(IUnitOfWork<ApplicationDbContext> unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _productRepository = _unitOfWork.GetRepository<Product>();

        }


        public async Task<IDataResult<IEnumerable<ProductDTO>>> GetAllProductsAsync(RequestForGetAllProduct request)
        {
            IQueryable<Product> products = _productRepository.GetAll().Include(p => p.Category);

            //filter

            if (request.filterProduct is not null && request.filterProduct.CategoryId.Length != 0)
            {
                products = products.Where(p => request.filterProduct.CategoryId.Contains(p.CategoryId));
            }

            //search
            if (!String.IsNullOrEmpty(request.searchString))
            {
                products = products.Where(p => p.ProductName.Contains(request.searchString));
            }
            //order
            bool ordering = false;//product-un order olunma veziyyeti
            if (request.sortProduct is not null && (request.sortProduct.PriceAsc || request.sortProduct.PriceDesc))
            {
                if (request.sortProduct.PriceDesc)
                {
                    products = products.OrderByDynamic<Product>("Price", true, ordering);
                    request.sortProduct.PriceAsc = false;
                }
                products = products.OrderByDynamic<Product>("Price", false, ordering);
                ordering = true;
            }
            if (request.sortProduct is not null && (request.sortProduct.NameAsc || request.sortProduct.NameDesc))
            {
                if (request.sortProduct.NameDesc)
                {
                    products = products.OrderByDynamic<Product>("ProductName", true, ordering);
                    request.sortProduct.NameAsc = false;
                }
                products = products.OrderByDynamic<Product>("ProductName", true, ordering);
                ordering = true;
            }
            //pagination
            var totalItem = await products.CountAsync();
            var pl = new PagedList(totalItem, request.page);
            var result = await products.Skip((pl.CurrentPage - 1) * pl.PageSize).Take(pl.PageSize).ToListAsync();

            //mapping
            List<ProductDTO> productsDTO = new List<ProductDTO>();

            if (result is not null && result.Count != 0)
            {
                productsDTO = _mapper.Map<List<ProductDTO>>(result);
            }

            //Create and return Response
            var pageList = new PagedList(totalItem, request.page);
            return (productsDTO is not null && productsDTO.Count != 0)
                ? new SuccessDataResult<IEnumerable<ProductDTO>>(productsDTO, request.filterProduct, request.searchString, request.sortProduct, pageList, "The product was successfully delivered.")
                : new ErrorDataResult<IEnumerable<ProductDTO>>(productsDTO, request.filterProduct, request.searchString, request.sortProduct, pageList, "Product not found.");
        }

        public async Task<IDataResult<ProductDTO>> GetProductByIdAsync(int id)
        {
            //var productRepository = _unitOfWork.GetRepository<Product>();
            var product = await _productRepository.GetByIdAsync(id, false);
            var productDTO = _mapper.Map<ProductDTO>(product);
            return (productDTO is not null)
                 ? new SuccessDataResult<ProductDTO>(productDTO, "Product succesfully get!")
                : new ErrorDataResult<ProductDTO>("Product not created");
        }     

        public async Task<IDataResult<ProductDTO>> AddProductAsync(AddProductDTO addProductDTO)
        {
            var product = _mapper.Map<Product>(addProductDTO);
            var productAdd = await _productRepository.AddAsync(product);
            if (!productAdd)
            {
                return new ErrorDataResult<ProductDTO>("Error when add product");
            }
            var save = _unitOfWork.SaveChanges();
            if (save!=0)
            {
                var createdProductDTO = _mapper.Map<ProductDTO>(product);
                return new SuccessDataResult<ProductDTO>(createdProductDTO, "Product succesfully created!");
            }
            else
            {
                return new ErrorDataResult<ProductDTO>("Product not created");
            }
        }
            

        public async Task<IResult> UpdateProduct(int id, ProductDTO productDTO)
        {
            var product = await _productRepository.GetByIdAsync(id, false);

            if (product == null)
            {
                return new ErrorResult("Product not found");
            }
            product = _mapper.Map<Product>(productDTO);
            if (await _productRepository.Update(product))
            {
                var save = _unitOfWork.SaveChanges();
                if(save !=0)
                {
                    return new SuccessResult("Product succesfully update");
                }
                return new ErrorResult("Product not update");
            }
                
            return new ErrorResult("Product not update");
        }
        public async Task<IResult> RemoveProduct(int id)
        {

            var product = await _productRepository.GetByIdAsync(id, false);
            if (product == null)
            {
                return new ErrorResult("Product not found");
            }
            if (await _productRepository.Remove(product))
            {
               var save =  _unitOfWork.SaveChanges();
                return new SuccessResult("Product succesfully deleted");
            }
            return new ErrorResult("Product not deleted");

        }
    }
}
