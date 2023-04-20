using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SMTstock.Core.DataAccess.UnitOfWork.Interfaces;
using SMTstock.DAL.Context;
using SMTstock.Entities.DTO.OrderDto;
using SMTstock.Entities.DTO.ProductDto;
using SMTstock.Entities.Models;
using SMTstock.Entities.Utilities.Request;
using SMTstock.Services.Implementations;
using SMTstock.Services.Interfaces;

namespace SMTstock.Controllers
{
    //[Route("api/[controller]")]
    [Route("api/OrderAPI")]
    [ApiController]
    public class OrderAPIController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IUnitOfWork<ApplicationDbContext> _unitOfWork;

        public OrderAPIController(IOrderService orderService, IUnitOfWork<ApplicationDbContext> unitOfWork)
        {
            _orderService = orderService;
            _unitOfWork = unitOfWork;
        }

        [HttpGet("GetOrders")]
        public async Task<IActionResult> GetOrdersAsync([FromQuery]RequestForGetAllOrder request)
        {
            var result = await _orderService.GetAllOrdersAsync(request);
            return Ok(result);
        }

        [HttpGet("GetOrderById/{id:int}")]
        public async Task<IActionResult> GetOrderByIdAsync(int id)
        {
            var order  = await _orderService.GetOrderByIdAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            return Ok(order);
        }
        [HttpPost("AddOrder")]
        public async Task<IActionResult> AddOrderAsync(OrderCreateDto orderDto)
        {
            if (ModelState.IsValid)
            {
                var result = await _orderService.AddOrderAsync(orderDto);
                return Ok(result);
            }

            return BadRequest(ModelState);
        }

        [HttpPut("UpdateOrder/{id:int}")]
        public async Task<IActionResult> UpdateOrderAsync(int id, [FromBody]OrderUpdateDto orderUpdateDto)
        {
            if (orderUpdateDto == null || id != orderUpdateDto.Id)
            {
                return BadRequest();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    await _orderService.UpdateOrder(id, orderUpdateDto);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (_orderService.GetOrderByIdAsync(id) == null)
                    {
                        return NotFound();
                    }

                    throw;
                }

                return NoContent();
            }

            return BadRequest(ModelState);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _orderService.RemoveOrder(id);
            return Ok(result);
        }

    }
}
