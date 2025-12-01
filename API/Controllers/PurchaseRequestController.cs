using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using API.Model;
using API.Data;
using Microsoft.EntityFrameworkCore;


namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PurchaseRequestController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PurchaseRequestController(AppDbContext context)
        {
            _context = context;
        }


        [HttpPost]
        public async Task<ActionResult<bool>> SavePR (PurchaseRequestmodel PRmodel)
        {
            _context.PurchaseRequestmodels.Add(PRmodel);
            await _context.SaveChangesAsync();
            return Ok();

        }
        [HttpGet]
        public async Task<ActionResult<List<PurchaseRequestmodel>>> GetAllPR()
        {
            var prList = await _context.PurchaseRequestmodels
                .Include(p => p.Product)
                .Include(m => m.MUnit)
                .ToListAsync();

            if (prList == null || prList.Count == 0)
            {
                return NotFound("No purchase requests found.");
            }
            return Ok(prList);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<PurchaseRequestmodel>> GetPRById(int id)
        {
            var pr = await _context.PurchaseRequestmodels
                .Include(p => p.Product)
                .Include(m => m.MUnit)
                .FirstOrDefaultAsync(pr => pr.PRID == id);
            if (pr == null)
            {
                return NotFound($"No Purchase Request found with ID={id}");
            }
            return Ok(pr);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<string>> DeletePRById(int id)
        {
            var pr = await _context.PurchaseRequestmodels.FindAsync(id);
            if (pr == null)
            {
                return NotFound($"No Purchase Request found with ID={id}");
            }
            _context.PurchaseRequestmodels.Remove(pr);
            await _context.SaveChangesAsync();
            return Ok("Purchase Request removed Successfully");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePR(int id, PurchaseRequestmodel model)
        {
            if (id != model.PRID)
            {
                return BadRequest("ID mismatch");
            }
            var existingPR = await _context.PurchaseRequestmodels.FindAsync(id);
            if (existingPR == null)
            {
                return NotFound($"No Purchase Request found with ID={id}");
            }
            // Update fields
            existingPR.PRNo = model.PRNo;
            existingPR.PRDate = model.PRDate;
            existingPR.PRQuantity = model.PRQuantity;
            existingPR.MUnitId = model.MUnitId;
            existingPR.Id = model.Id;
            await _context.SaveChangesAsync();
            return Ok(existingPR);
        }


    }
}
