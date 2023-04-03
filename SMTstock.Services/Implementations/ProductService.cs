using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SMTstock.Core.DataAccess.GenericRepository.Impelementations;
using SMTstock.Core.DataAccess.UnitOfWork.Interfaces;
using SMTstock.DAL.Repository.Impelementations;
using SMTstock.DAL.Repository.Interfaces;
using SMTstock.Entities.Models;
using SMTstock.Services.Interfaces;
using SMTstock.DAL.Context;
using SMTstock.Core.DataAccess.GenericRepository.Interfaces;
using SMTstock.Entities.DTO;
using System.Reflection.Metadata.Ecma335;
using AutoMapper;

namespace SMTstock.Services.Implementations
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork<ApplicationDbContext> _unitOfWork;
        private readonly IMapper _mapper;
        public ProductService(IUnitOfWork<ApplicationDbContext> unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }



        public async Task<ProductDTO> GetProductByIdAsync(int id)
        {
            var productRepository = _unitOfWork.GetRepository<Product>();
            var product = await productRepository.GetByIdAsync(p => p.Id == id);

            if (product == null)
            {
                return null;
            }
            var productDto = _mapper.Map<ProductDTO>(product);
            return productDto;
            //return new ProductDTO
            //{
            //    Id = product.Id,
            //    ProductName = product.ProductName,
            //    Balance = product.Balance,
            //    Price = product.Price,
            //    CategoryId = product.CategoryId
            //};

            //return await productRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<ProductDTO>> GetAllProductsAsync()
        {
            var productRepository = _unitOfWork.GetRepository<Product>();
            IEnumerable<Product> products = await productRepository.GetAllAsync();
            List<ProductDTO> productsDto = new List<ProductDTO>();
            foreach (var product in products)
            {
                var productDto = _mapper.Map<ProductDTO>(product);
                //var productDto = new ProductDTO
                //{
                //    Id = product.Id,
                //    ProductName = product.ProductName,
                //    Balance = product.Balance,
                //    Price = product.Price,
                //    CategoryId = product.CategoryId
                //};
                productsDto.Add(productDto);
            }
            return productsDto;
        }


        public async Task<ProductDTO> AddProductAsync(AddProductDto addProductDto)
        {
            var productRepository = _unitOfWork.GetRepository<Product>();
            var product = _mapper.Map<Product>(addProductDto);
            //var product = new Product
            //{
            //    //Id = productDto.Id,
            //    ProductName = productDto.ProductName,
            //    Balance = productDto.Balance,
            //    Price = productDto.Price,
            //    CategoryId = productDto.CategoryId
            //};

            await productRepository.AddAsync(product);
            _unitOfWork.SaveChanges();
            var createdProductDto = _mapper.Map<ProductDTO>(product);
            return createdProductDto;
            //return new ProductDTO
            //{
            //    Id = product.Id,
            //    ProductName = product.ProductName,
            //    Balance = product.Balance,
            //    Price = product.Price,
            //    CategoryId = product.CategoryId
            //};
        }

        public async Task<bool> UpdateProduct(int id, ProductDTO productDto)
        {

            var productRepository = _unitOfWork.GetRepository<Product>();
            var product = await productRepository.GetByIdAsync(p => p.Id == id);

            if (product == null)
            {
                return false;
            }
            product = _mapper.Map<Product>(productDto);
            //product.Id = productDto.Id;
            //product.ProductName = productDto.ProductName;
            //product.Balance = productDto.Balance;
            //product.Price = productDto.Price;
            //product.CategoryId = productDto.CategoryId;

            if (await productRepository.Update(product))
            {
                _unitOfWork.SaveChanges();
                return true;
            }
            return false;
        }
        public async Task RemoveProduct(int id)
        {
            var productRepository = _unitOfWork.GetRepository<Product>();
            var product = await productRepository.GetByIdAsync(p => p.Id == id);
            await productRepository.Remove(product);
            _unitOfWork.SaveChanges();
        }
    }
}
