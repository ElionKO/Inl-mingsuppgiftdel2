using Microsoft.AspNetCore.Mvc;
using BakeryAPI.Data;
using BakeryAPI.Entities;

namespace BakeryAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly BakeryContext _context;

        public ProductsController(BakeryContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult AddProduct(Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();
            return Ok(product);
        }

        [HttpGet]
        public IActionResult GetProducts()
        {
            return Ok(_context.Products.ToList());
        }

        // VIKTIGT för G
        [HttpGet("{id}")]
        public IActionResult GetProduct(int id)
        {
            var product = _context.Products.Find(id);
            if (product == null) return NotFound();

            return Ok(product);
        }

        [HttpPut("{id}/price")]
        public IActionResult UpdatePrice(int id, decimal newPrice)
        {
            var product = _context.Products.Find(id);
            if (product == null) return NotFound();

            product.PricePerPiece = newPrice;
            _context.SaveChanges();

            return Ok(product);
        }
    }
}