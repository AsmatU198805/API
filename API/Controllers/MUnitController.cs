using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.Model;
using API.Data;


namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MUnitController : ControllerBase
    {
        private readonly AppDbContext _context;
        public MUnitController(AppDbContext context)
        {
            _context = context;
        }


        [HttpPost]
        public async Task<ActionResult<bool>>SaveMUnit(MUnitModel munitmodel)
        {
            _context.Munit.Add(munitmodel);
            await _context .SaveChangesAsync();
            return Ok(true);
        }


        [HttpGet]
        public async Task<ActionResult<List<MUnitModel>>> GetMunit()
        {
            var munit = await _context.Munit.ToListAsync();
            if (munit == null)
                return NotFound();
            return munit;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MUnitModel>>GetMunitById(int id)
        {
            var munit= await _context.Munit.FindAsync(id);
            if(munit==null)
                return NotFound($"No M.Unit found with ID={id}");
            return Ok(munit);
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult<string>> DeleteMunitById(int id)
        {
            var munit = await _context.Munit.FindAsync(id);
            _context.Munit.Remove(munit);
            await _context .SaveChangesAsync();
            return Ok("M.Unit removed Successfully");
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMUnit(int id, MUnitModel model)
        {
            if (id != model.MUnitId)
                return BadRequest("ID mismatch");

            var existing = await _context.Munit.FindAsync(id);
            if (existing == null)
                return NotFound($"M.Unit with ID {id} not found.");

            existing.MUnitCode = model.MUnitCode;
            existing.MUnitName = model.MUnitName;

            await _context.SaveChangesAsync();
            return Ok("Updated Successfully");
        }


    }
}
   