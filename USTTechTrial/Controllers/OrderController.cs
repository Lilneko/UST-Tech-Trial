using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using USTTechTrial.Context;
using USTTechTrial.Models;   
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace USTTechTrial.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : ControllerBase
    {
        private OrderContext _context;
        private readonly ILogger<OrderController> _logger;

        public OrderController(OrderContext context, ILogger<OrderController> logger){
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        [Route("GetAllOrders")]
        public async Task<ActionResult<List<Order>>> GetAllOrders([FromServices] OrderContext context)
        {
            var orders = await context.Orders.Include(c=> c.items).ToListAsync();
            if(orders == null) { return NotFound(); }
            return Ok(orders);
        }

        [HttpPost]
        [Route("PostOrder")]
        public async Task<ActionResult<Order>> PostOrder([FromServices] OrderContext context, [FromBody] Order order)
        {
            if(!ModelState.IsValid) { return BadRequest(ModelState); }

            foreach(Item item in order.items ?? new List<Item>()){
                item.subTotal = item.units * item.pricePerUnit;
                item.vatPercentage = item.type == 1 ? 25 : 0;
                item.totalWithVat = item.subTotal + ((item.subTotal * item.vatPercentage)/100);
                order.total += item.totalWithVat;
            };

             context.Orders.Add(order);
             await context.SaveChangesAsync();
             return Ok(order);
        }
    }
}
