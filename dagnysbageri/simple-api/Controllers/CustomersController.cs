using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BakeryAPI.Data;
using BakeryAPI.Entities;

namespace BakeryAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomersController : ControllerBase
    {
        private readonly BakeryContext _context;

        public CustomersController(BakeryContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult AddCustomer(Customer customer)
        {
            _context.Customers.Add(customer);
            _context.SaveChanges();
            return Ok(customer);
        }

        [HttpGet]
        public IActionResult GetCustomers()
        {
            return Ok(_context.Customers.ToList());
        }

        [HttpGet("{id}")]
        public IActionResult GetCustomer(int id)
        {
            var customer = _context.Customers
                .Include(c => c.Orders)
                .ThenInclude(o => o.Product)
                .FirstOrDefault(c => c.Id == id);

            if (customer == null) return NotFound();

            return Ok(customer);
        }
    }
}