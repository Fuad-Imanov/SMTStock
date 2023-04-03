//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using SMTstock.Core.DataAccess.UnitOfWork.Interfaces;
//using SMTstock.DAL.Context;
//using SMTstock.Entities.Models;
//using SMTstock.Services.Interfaces;

//namespace SMTstock.Controllers
//{
//    //[Route("api/[controller]")]
//    [Route("api/OrderAPI")]
//    [ApiController]
//    public class OrderAPIController : ControllerBase
//    {
//        private readonly IOrderService _orderService;
//        private readonly IUnitOfWork<ApplicationDbContext> _unitOfWork;

//        public OrderAPIController(IOrderService orderService,IUnitOfWork<ApplicationDbContext> unitOfWork)
//        {
//            _orderService = orderService;
//            _unitOfWork = unitOfWork;
//        }

//        [HttpGet("GetOrders")]
//        public IActionResult GetOrders()
//        {
//            var orders = _orderService.GetOrders();

//            return Ok(orders);
//        }

//        [HttpGet("GetOrderById/{id:int}")]
//        public async Task<IActionResult> GetOrderByIdAsync(int id)
//        {
//            var order = await _orderService.GetOrderByIdAsync(id);

//            if (order == null)
//            {
//                return NotFound();
//            }

//            return Ok(order);
//        }
//        [HttpPost("AddOrder")]
//        public async Task<IActionResult> AddOrderAsync(Order order)
//        {
//            if (ModelState.IsValid)
//            {
//                await _orderService.AddOrderAsync(order);
//                _unitOfWork.SaveChanges();

//                return CreatedAtAction(nameof(GetOrderByIdAsync), new { id = order.Id }, order);
//            }

//            return BadRequest(ModelState);
//        }

//        [HttpPut("UpdateOrder/{id:int}")]
//        public async Task<IActionResult> UpdateOrderAsync(int id)
//        {
//            if (ModelState.IsValid)
//            {
//                try
//                {
//                    var order = await _orderService.GetOrderByIdAsync(id);
//                    _orderService.UpdateOrder(order);
//                    _unitOfWork.SaveAsync();
//                }
//                catch (DbUpdateConcurrencyException)
//                {
//                    if (_orderService.GetOrderByIdAsync(id) == null)
//                    {
//                        return NotFound();
//                    }

//                    throw;
//                }

//                return NoContent();
//            }

//            return BadRequest(ModelState);
//        }

//        [HttpDelete("{id:int}")]
//        public async Task<IActionResult> DeleteAsync(int id)
//        {
//            var order = await _orderService.GetOrderByIdAsync(id);

//            if (order == null)
//            {
//                return NotFound();
//            }

//            _orderService.DeleteOrder(order);
//            _unitOfWork.SaveAsync();

//            return NoContent();
//        }

//    }
//}
