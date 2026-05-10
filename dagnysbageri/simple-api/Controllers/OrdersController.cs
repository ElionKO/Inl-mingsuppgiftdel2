using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BakeryAPI.Data;
using BakeryAPI.Entities;

namespace BakeryAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly BakeryContext _context;

        public OrdersController(BakeryContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult AddOrder(Order order)
        {
            order.TotalPrice = order.Price * order.Quantity;

            _context.Orders.Add(order);
            _context.SaveChanges();

            return Ok(order);
        }

        [HttpGet]
        public IActionResult GetOrders()
        {
            var orders = _context.Orders
       .Include(o => o.Customer)
       .Include(o => o.Product)
       .Select(o => new
       {
           o.OrderNumber,
           o.OrderDate,
           Customer = o.Customer.StoreName,
           Product = o.Product.Name,
           o.Quantity,
           o.Price,
           o.TotalPrice
       })
       .ToList();

            return Ok(orders);
        }

        [HttpGet("search")]
        public IActionResult Search(string orderNumber, DateTime? date)
        {
            var query = _context.Orders.AsQueryable();

            if (!string.IsNullOrEmpty(orderNumber))
                query = query.Where(o => o.OrderNumber == orderNumber);

            if (date.HasValue)
                query = query.Where(o => o.OrderDate.Date == date.Value.Date);

            return Ok(query
                .Include(o => o.Customer)
                .Include(o => o.Product)
                .ToList());
        }
    }
}