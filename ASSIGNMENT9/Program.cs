using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using OrderManagement;

namespace OrderManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly OrderService orderService;

        public OrdersController(OrderContext context)
        {
            orderService = new OrderService(context);
        }

        [HttpGet]
        public IEnumerable<Order> Get()
        {
            return orderService.QueryOrdersByTotalAmount(double.MaxValue);
        }

        [HttpGet("{orderNumber}")]
        public ActionResult<Order> Get(int orderNumber)
        {
            var orders = orderService.QueryOrdersByOrderNumber(orderNumber);
            if (!orders.Any())
                return NotFound();
            return orders.First();
        }

        [HttpPost]
        public ActionResult Post([FromBody] Order order)
        {
            try
            {
                orderService.AddOrder(order);
                return CreatedAtAction(nameof(Get), new { orderNumber = order.OrderNumber }, order);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpDelete("{orderNumber}")]
        public ActionResult Delete(int orderNumber)
        {
            try
            {
                orderService.DeleteOrder(orderNumber);
                return NoContent();
            }
            catch
            {
                return NotFound();
            }
        }
    }
}

