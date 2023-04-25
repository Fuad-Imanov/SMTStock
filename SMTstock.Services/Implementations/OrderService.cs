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
using SMTstock.Entities.Utilities.Request;
using SMTstock.Entities.Utilities.Pagination;
using SMTstock.Entities.Utilities.Sort;
using SMTstock.Entities.DTO.OrderProduct;
using Microsoft.Data.SqlClient;

namespace SMTstock.Services.Implementations
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork<ApplicationDbContext> _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IOrderRepository _orderRepository;
        private readonly IGenericRepository<Product> _productRepository;
        private readonly IGenericRepository<Merchant> _merchantRepository;
        private readonly IGenericRepository<OrderProduct> _orderProductRepository;
        public OrderService(IUnitOfWork<ApplicationDbContext> unitOfWork, IMapper mapper, IOrderRepository orderRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _orderRepository = orderRepository;
            _productRepository = _unitOfWork.GetRepository<Product>();
            _merchantRepository = _unitOfWork.GetRepository<Merchant>();
            _orderProductRepository = _unitOfWork.GetRepository<OrderProduct>();
        }

        public async Task<IDataResult<IEnumerable<OrderGetDto>>> GetAllOrdersAsync(RequestForGetAllOrder request)
        {
            IQueryable<Order> orders = _orderRepository.GetAll().Include(o => o.Merchant).Include(o => o.OrdersProducts).ThenInclude(op => op.Product).ThenInclude(p => p.Category);
            //filter

            if (request.filterOrder is not null && request.filterOrder.MerchantId.Length != 0)
            {
                orders = orders.Where(o => request.filterOrder.MerchantId.Contains(o.MerchantId));
            }

            //search
            if (!String.IsNullOrEmpty(request.searchString))
            {
                var productName = new List<string>();
                foreach (var order in orders)
                {
                    foreach (var productOrder in order.OrdersProducts)
                    {
                        productName.Add(productOrder.Product.ProductName);
                    }
                }
                orders = orders.Where(o => productName.Contains(request.searchString));
            }
            //order
            bool ordering = false;//orders-in sort olunma veziyyeti
            if (request.sortOrder is not null && (request.sortOrder.OrderTotalPriceAsc || request.sortOrder.OrderTotalPriceDesc))
            {
                if (request.sortOrder.OrderTotalPriceDesc)
                {
                    orders = orders.OrderByDynamic<Order>("OrderTotalPrice", true, ordering);
                    request.sortOrder.OrderTotalPriceAsc = false;
                }
                orders = orders.OrderByDynamic<Order>("OrderTotalPrice", false, ordering);
                ordering = true;
            }
            if (request.sortOrder is not null && (request.sortOrder.OrderDateAsc || request.sortOrder.OrderDateDesc))
            {
                if (request.sortOrder.OrderDateDesc)
                {
                    orders = orders.OrderByDynamic<Order>("OrderDate", true, ordering);
                    request.sortOrder.OrderDateAsc = false;
                }
                orders = orders.OrderByDynamic<Order>("OrderDate", true, ordering);
                ordering = true;
            }
            //pagination
            var totalItem = await orders.CountAsync();
            var pl = new PagedList(totalItem, request.page);
            var result = await orders.Skip((pl.CurrentPage - 1) * pl.PageSize).Take(pl.PageSize).ToListAsync();

            //mapping
            #region Excample without Automapper
            //var listorder = orders.Select(o => new OrderGetDto
            //{
            //    Id = o.Id,
            //    MerchantId = o.MerchantId,
            //    OrderDate = o.OrderDate,
            //    OrderTotalPrice = o.OrderTotalPrice,
            //    OrdersProducts = o.OrdersProducts.Select(op => new OrderProductGetDto
            //    {
            //        Id = op.Id,
            //        ProductId = op.ProductId,
            //        Quantity = op.Quantity,
            //        TotalPrice = op.TotalPrice,
            //        ProductDto = new ProductDTO
            //        {
            //            Id = op.Product.Id,
            //            ProductName = op.Product.ProductName,
            //            Price = op.Product.Price
            //        }
            //    }).ToList()
            //}).ToList();


            //Without select and mapping

            //var orderDtos = new List<OrderGetDto>();

            //foreach (var order in orders)
            //{
            //    var orderDto = new OrderGetDto
            //    {
            //        Id = order.Id,
            //        MerchantId = order.MerchantId,
            //        OrderDate = order.OrderDate,
            //        OrderTotalPrice = order.OrderTotalPrice,
            //        OrdersProducts = new List<OrderProductGetDto>()
            //    };

            //    foreach (var orderProduct in order.OrdersProducts)
            //    {
            //        var orderProductDto = new OrderProductGetDto
            //        {
            //            Id = orderProduct.Id,
            //            ProductId = orderProduct.ProductId,
            //            Quantity = orderProduct.Quantity,
            //            TotalPrice = orderProduct.TotalPrice,
            //            ProductDto = new ProductDTO
            //            {
            //                Id = orderProduct.Product.Id,
            //                ProductName = orderProduct.Product.ProductName,
            //                Price = orderProduct.Product.Price,
            //                CategoryId = orderProduct.Product.CategoryId,

            //            }
            //        };

            //        orderDto.OrdersProducts.Add(orderProductDto);
            //    }

            //    orderDtos.Add(orderDto);
            //}
            #endregion
            List<OrderGetDto> ordersDto = new List<OrderGetDto>();

            if (result is not null && result.Count != 0)
            {
                ordersDto = _mapper.Map<List<OrderGetDto>>(result);
            }

            //Create and return Response
            var pageList = new PagedList(totalItem, request.page);
            return (ordersDto is not null && ordersDto.Count != 0)
                ? new SuccessDataResult<IEnumerable<OrderGetDto>>(ordersDto, request.filterOrder, request.searchString, request.sortOrder, pageList, "The product was successfully delivered.")
                : new ErrorDataResult<IEnumerable<OrderGetDto>>(ordersDto, request.filterOrder, request.searchString, request.sortOrder, pageList, "Product not found.");


        }
        public async Task<IDataResult<OrderGetDto>> GetOrderByIdAsync(int id)
        {

            var order = await _orderRepository.GetOrderByIdAsync(id, false);
            var orderDto = _mapper.Map<OrderGetDto>(order);
            return (orderDto is not null)
                 ? new SuccessDataResult<OrderGetDto>(orderDto, "Order succesfully get!")
                : new ErrorDataResult<OrderGetDto>("Order not found");
        }
        public async Task<IDataResult<OrderGetDto>> AddOrderAsync(OrderCreateDto orderCreateDto)
        {
            var merchant = await _merchantRepository.GetByIdAsync(orderCreateDto.MerchantId, true);
            if (merchant is null)
            {
                return new ErrorDataResult<OrderGetDto>("Merchant not found");
            }
            var order = new Order
            {
                MerchantId = orderCreateDto.MerchantId,
                //Merchant = merchant,
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
                var product = await _productRepository.GetByIdAsync(orderProductCreateDto.ProductId, true);
                if (product == null)
                {
                    return new ErrorDataResult<OrderGetDto>("Product not found");
                }
                if (product.Balance < orderProductCreateDto.Quantity)
                {
                    return new ErrorDataResult<OrderGetDto>($"Not enough units of product {product.ProductName}.");
                }
                product.Balance -= orderProductCreateDto.Quantity;

                //add info to orderproduct 
                //orderProduct.Product = product;
                orderProduct.TotalPrice = orderProductCreateDto.Quantity * product.Price;
                order.OrderTotalPrice = order.OrderTotalPrice + (orderProductCreateDto.Quantity * product.Price);
                var orderProductAdd = await _orderProductRepository.AddAsync(orderProduct);
                if (!orderProductAdd)
                {
                    return new ErrorDataResult<OrderGetDto>("Error when added OrderProduct");
                }
                order.OrdersProducts.Add(orderProduct);

            }

            var orderAdd = await _orderRepository.AddAsync(order);
            if (!orderAdd)
            {
                return new ErrorDataResult<OrderGetDto>("Error when added Order");
            }
            var save = _unitOfWork.SaveChanges();
            if (save != 0)
            {
                var orderDto = _mapper.Map<OrderGetDto>(order);
                return new SuccessDataResult<OrderGetDto>(orderDto, "Order succesfully created!");
            }
            else
            {
                return new ErrorDataResult<OrderGetDto>("Order not created - Error when SaveChanges");

            }

        }
        public async Task<IResult> UpdateOrder(int id, OrderUpdateDto orderUpdateDto)
        {
            var order = await _orderRepository.GetOrderByIdAsync(id, false);

            if (order is null)
            {
                return new ErrorResult("Order not found");
            }
            var merchant = await _merchantRepository.GetByIdAsync(orderUpdateDto.MerchantId, false);
            if (merchant is null)
            {
                return new ErrorResult("Merchant not found");
            }
            //Update olunan orderin icindeki butun productlarin sayini bazaya  qaytaririq
            foreach(var orderProduct in order.OrdersProducts)
            {
                var product = await _productRepository.GetByIdAsync(orderProduct.ProductId,true);
                if(product is not null)
                {
                    product.Balance += orderProduct.Quantity;
                }
            }
            
            //yeni order yaradiriq
            var orderForDb = new Order
            {
                MerchantId = orderUpdateDto.MerchantId,
                OrderTotalPrice = 0,
                OrderDate = DateTime.Now,
                OrdersProducts = new List<OrderProduct>()
            };

            foreach (var orderProductCreateDto in orderUpdateDto.OrdersProducts)
            {
                //create new orderproduct
                var orderProduct = new OrderProduct
                {
                    ProductId = orderProductCreateDto.ProductId,
                    Quantity = orderProductCreateDto.Quantity

                };
                //get product from db and check Balance
                var product = await _productRepository.GetByIdAsync(orderProductCreateDto.ProductId, true);
                if (product == null)
                {
                    return new ErrorDataResult<OrderGetDto>("Product not found");
                }
                if (product.Balance < orderProductCreateDto.Quantity)
                {
                    return new ErrorDataResult<OrderGetDto>($"Not enough units of product {product.ProductName}.");
                }
                product.Balance -= orderProductCreateDto.Quantity;

                //add info to orderproduct 
                //orderProduct.Product = product;
                orderProduct.TotalPrice = orderProductCreateDto.Quantity * product.Price;
                orderForDb.OrderTotalPrice = orderForDb.OrderTotalPrice + (orderProductCreateDto.Quantity * product.Price);
                var orderProductAdd = await _orderProductRepository.AddAsync(orderProduct);
                if (!orderProductAdd)
                {
                    return new ErrorDataResult<OrderGetDto>("Error when added OrderProduct");
                }
                orderForDb.OrdersProducts.Add(orderProduct);

            }
            var orderAdd = await _orderRepository.AddAsync(orderForDb);
            if (!orderAdd)
            {
                return new ErrorDataResult<OrderGetDto>("Error when added Order");
            }
           
            var save = _unitOfWork.SaveChanges();
            if (save != 0)
            {
                //eger yeni order dugun elave olunubsa onda kohne order silinir
                var orderIdParameter = new SqlParameter("@OrderId", id);
                var result = await _unitOfWork.GetDbContext().Database.ExecuteSqlRawAsync("EXEC DeleteOrder @OrderId", orderIdParameter);
                return new SuccessResult("Order succesfully update!");
            }
            else
            {
                return new ErrorResult("Order update failed");

            }
            
            #region
            ////Orderin icindeki yeni sifarish olunan productlarin sayini bazadan azaldiriq
            //foreach (var orderProduct in orderUpdateDto.OrdersProducts)
            //{
            //    var product = await _productRepository.GetByIdAsync(orderProduct.ProductId, true);
            //    var orderProductFromDb = await _orderProductRepository.GetByIdAsync(orderProduct.Id, true);
            //    if (product is null)
            //    {
            //        return new ErrorResult("Product not found");
            //    }
            //    if (product.Balance < orderProduct.Quantity)
            //    {
            //        return new ErrorResult($"Not enough units of product {product.ProductName}.");
            //    }
            //    product.Balance -= orderProduct.Quantity;
            //    orderProduct.TotalPrice = orderProduct.Quantity * product.Price;
            //    orderUpdateDto.OrderTotalPrice += (orderProduct.Quantity * product.Price);
            //    //Eger orderProductFromDb null-dursa demek order-e  yeni product elave olunub eks halda kohne orderproduct yenilenir
            //    if (orderProductFromDb != null)
            //    {
            //        // Update the properties of the OrderProduct entity
            //        orderProductFromDb.ProductId = orderProduct.ProductId;
            //        orderProductFromDb.TotalPrice = orderProduct.TotalPrice;
            //        orderProductFromDb.Quantity = orderProduct.Quantity;
            //    }
            //    else
            //    {zzzzz
            //        // If the OrderProduct entity is not found, add it as a new entity
            //        //orderProductFromDb = _mapper.Map<OrderProduct>(orderProduct);

            //        //orderUpdateDto.OrdersProducts.Add(orderProduct);
            //        var orderProductForDb = _mapper.Map<OrderProduct>(orderProduct);

            //        if (orderProductForDb != null)
            //        {
            //            orderProductForDb.OrderId = id;
            //            var orderProductAdd = await _orderProductRepository.AddAsync(orderProductForDb);
            //        }
            //        else
            //        {
            //            return new ErrorResult("Error when mapping orderProdut");
            //        }

            //    }
            //}
            //order = _mapper.Map<Order>(orderUpdateDto);

            //if (order is not null)
            //{
            //    var save = _unitOfWork.SaveChanges();
            //    if (save != 0)
            //    {
            //        return new SuccessResult("Order succesfully update");
            //    }
            //    else
            //    {
            //        return new ErrorResult("Order update failed");
            //    }
            //    }
            //return new ErrorResult("Order update failed");
            #endregion
        }
        public async Task<IResult> RemoveOrder(int id)
        {
            var order = await _orderRepository.GetOrderByIdAsync(id, false);
            if (order == null)
            {
                return new ErrorResult("Order  not found");
            }
            var orderIdParameter = new SqlParameter("@OrderId", id);
            var result = await _unitOfWork.GetDbContext().Database.ExecuteSqlRawAsync("EXEC DeleteOrder @OrderId", orderIdParameter);
            //var save = _unitOfWork.SaveChanges();

            return new SuccessResult("Order  succesfully removed");
        }
    }
}

