using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.Model;
using API.Data;


namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProductsController(AppDbContext context)
        {
            _context = context;
        }


        

        [HttpPost]
        public async Task<ActionResult<bool>> SaveProducts(ProductsModel Prod) 
        { _context.Products.Add(Prod); 
            await _context.SaveChangesAsync(); 
            return Ok(true); 
        }



        [HttpGet] 
        public async Task<ActionResult<List<ProductsModel>>> GetProducts()
        { 
            var products = await _context.Products.ToListAsync(); 
            if (products == null) 
                return NotFound();
            return products; 
        }



        [HttpGet("{id}")] 
        public async Task<ActionResult<ProductsModel>> GetProductById(int id) 
        { 
            var product = await _context.Products.FindAsync(id); 
            if (product == null) 
                return NotFound($"No product found with ID = {id}"); 
            return Ok(product); 
        
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<string>> DeleteProductById(int id)
        {
            var product = await _context.Products.FindAsync(id);
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return Ok("Data removed successfully");


        }

















        [HttpPut("{id}")]
public async Task<ActionResult<bool>> UpdateProduct(int id, ProductsModel updatedProduct)
{
    // ID must match
    if (id != updatedProduct.Id)
        return BadRequest("Product ID cannot be changed.");

    // Check if record exists
    var existingProduct = await _context.Products
        .AsNoTracking()
        .FirstOrDefaultAsync(p => p.Id == id);

    if (existingProduct == null)
        return NotFound($"No product found with ID = {id}");

    // Keep the same ID and update allowed fields
    updatedProduct.Id = id;

    // Update the record
    _context.Products.Update(updatedProduct);
    var result = await _context.SaveChangesAsync();

    if (result > 0)
        return Ok(true);
    else
        return BadRequest("Record not updated — no changes detected.");
}

    }
}