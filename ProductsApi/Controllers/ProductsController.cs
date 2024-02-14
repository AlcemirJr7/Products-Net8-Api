using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductsAPI.Context;
using ProductsAPI.Models;

namespace ProductsAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {        
        
        private readonly AppDbContext _appDbContext;

        public ProductsController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProduct()
        {
            return Ok(await _appDbContext.Products.ToListAsync());
        }

        [HttpPost]
        public async Task<ActionResult<Product>> CreateProduct([FromBody] Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _appDbContext.Products.AddAsync(product);
            await _appDbContext.SaveChangesAsync();

            return Ok(product);
        }

        [HttpPut()]
        public async Task<ActionResult<Product>> UpdateProduct(Guid id, [FromBody] Product productJson)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var product = await _appDbContext.Products.FindAsync(id);
            if (product == null) 
            {
                return NotFound();
            }

            product.Name = productJson.Name;
            product.Description = productJson.Description;
            product.Price = productJson.Price;


            await _appDbContext.SaveChangesAsync();

            return Ok(productJson);
        }

        [HttpDelete("{id}")]

        public async Task<ActionResult<Product>> DeleteProduct(Guid id)
        {
            var product = await _appDbContext.Products.FindAsync(id);
            
            if (product != null) 
            {
                return NotFound();
            }

            _appDbContext.Remove(product);
            await _appDbContext.SaveChangesAsync(); 
            return Ok(product);

        }
    }
}
