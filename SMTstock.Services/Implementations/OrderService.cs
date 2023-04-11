using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SMTstock.Core.DataAccess.GenericRepository.Impelementations;
using SMTstock.Core.DataAccess.UnitOfWork.Interfaces;
using SMTstock.DAL.Repository.Impelementations;
using SMTstock.DAL.Repository.Interfaces;
using SMTstock.Entities.Models;
using SMTstock.Services.Interfaces;
using SMTstock.Core.DataAccess.UnitOfWork.Implementations;
using SMTstock.DAL.Context;
using AutoMapper;
using SMTstock.Core.DataAccess.GenericRepository.Interfaces;
using SMTstock.Entities.DTO.OrderDto;
using SMTstock.Entities.Utilities.Results.Abstract;
using SMTstock.Entities.DTO.ProductDto;
using SMTstock.Entities.Utilities.Results.Concrete;
using SMTstock.Entities.DTO;

namespace SMTstock.Services.Implementations
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork<ApplicationDbContext> _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IGenericRepository<Order> _orderRepository;
        private readonly IGenericRepository<Product> _productRepository;
        private readonly IGenericRepository<Merchant> _merchantRepository;
        private readonly IGenericRepository<OrderProduct> _orderProductRepository;
        public OrderService(IUnitOfWork<ApplicationDbContext> unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _orderRepository = _unitOfWork.GetRepository<Order>();
            _productRepository = _unitOfWork.GetRepository<Product>(); 
            _merchantRepository= _unitOfWork.GetRepository<Merchant>();
            _orderProductRepository = _unitOfWork.GetRepository<OrderProduct>();
        }

        public async Task<IDataResult<Order>> AddOrderAsync(OrderCreateDto orderCreateDto)
        {
            var merchant = await _merchantRepository.GetByIdAsync(orderCreateDto.MerchantId, true);
            if (merchant is null)
            {
                throw new Exception("Product not found");
            }
            var order = new Order
            {
                MerchantId = orderCreateDto.MerchantId,
                Merchant = merchant,
                OrderTotalPrice = 0,
                OrderDate = DateTime.Now,
                OrdersProducts = new List<OrderProduct>()
            };

            foreach (var orderProductCreateDto in orderCreateDto.OrdersProducts)
            {
                //create new orderproduct
                var orderProduct = new OrderProduct
                {
                    ProductId = orderProductCreateDto.ProductId,
                    Quantity = orderProductCreateDto.Quantity

                };
                //get product from db and check Balance
                var product = await _productRepository.GetByIdAsync(orderProductCreateDto.ProductId,true);
                if (product == null)
                {
                    throw new Exception("Product not found");
                }
                if (product.Balance < orderProductCreateDto.Quantity)
                {
                    throw new InvalidOperationException($"Not enough units of product {product.ProductName}.");
                }
                product.Balance -= orderProductCreateDto.Quantity;

                //add info to orderproduct 
                orderProduct.Product = product;
                orderProduct.TotalPrice = orderProductCreateDto.Quantity * product.Price;
                order.OrderTotalPrice = order.OrderTotalPrice + (orderProductCreateDto.Quantity * product.Price);
                //_orderProductRepository.AddAsync(orderProduct);
                order.OrdersProducts.Add(orderProduct);
                
            }
            
            var result = await _orderRepository.AddAsync(order);
            _unitOfWork.SaveChanges();
                return (result is not null)
                    ? new SuccessDataResult<Order>(order, "Product succesfully created!")
                    : new ErrorDataResult<Order>("Product not created");
            

            
        }

        public List<OrderGetDto> GetAllOrdersAsync()
        {
            var orders =  _orderRepository.GetAll();
             var listorder = orders.Select(o => new OrderGetDto
                {
                    Id = o.Id,
                    MerchantId = o.MerchantId,
                    OrderDate = o.OrderDate,
                    OrderTotalPrice = o.OrderTotalPrice,
                    OrdersProducts = o.OrdersProducts.Select(op => new OrderProductGetDto
                    {
                        ProductId = op.ProductId,
                        Quantity = op.Quantity,
                        TotalPrice = op.TotalPrice,
                        ProductDto = new ProductDTO
                        {
                            Id = op.Product.Id,
                            ProductName = op.Product.ProductName,
                            Price = op.Product.Price
                        }
                    }).ToList()
                }).ToList();

            return listorder;
            
        }

        public Task<IDataResult<Order>> GetOrderByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IResult> RemoveOrder(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IResult> UpdateOrder(int id, Order order)
        {
            throw new NotImplementedException();
        }
    }
}
