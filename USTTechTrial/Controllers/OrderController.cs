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

        public OrderController(OrderContext context){
            _context = context;
        }
        public OrderController()
        {
            _context = new OrderContext();
        }

        /// <summary>
        /// Get the complete list of orders.
        /// </summary>
        /// <returns>List<Order></returns>
        [HttpGet]
        [Route("GetAllOrders")]
        public async Task<ActionResult<List<Order>>> GetAllOrders()
        {
            var orders = await _context.Orders.Include(c=> c.items).ToListAsync();
            if(orders == null) { 
                return NotFound(); 
            }
            var asd = 1;
            return Ok(orders);
        }

        /// <summary>
        /// Post a Order and calculate price values.
        /// </summary>
        /// <param name="order">Order class</param>
        /// <returns>Order with calculated values</returns>
        [HttpPost]
        [Route("PostOrder")]
        public async Task<ActionResult<Order>> PostOrder([FromBody] Order order)
        {
            if(!ModelState.IsValid) { 
                return BadRequest(ModelState); 
            }

            foreach(Item item in order.items ?? new List<Item>()){
                item.subTotal = item.units * item.pricePerUnit;
                item.vatPercentage = item.type == 1 ? 25 : 0;
                item.totalWithVat = item.subTotal + ((item.subTotal * item.vatPercentage)/100);
                order.total += item.totalWithVat;
            };

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            return Ok(order);
        }
    }
}
