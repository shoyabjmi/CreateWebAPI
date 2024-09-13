using ecommapi.Data;
using ecommapi.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ecommapi.Controllers;


[ApiController]
[Route("api/[controller]")]
public class BrandController: ControllerBase
{
    private readonly ApplicationDbContext _context;

    public BrandController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: api/Brand
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Brand>>> GetBrand()
    {
        return await _context.Brand.ToListAsync();
    }

    // GET: api/Brand/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Brand>> GetBrand(int id)
    {
        var brand = await _context.Brand.FindAsync(id);

        if (brand == null)
        {
            return NotFound();
        }

        return brand;
    }

    // POST: api/Brand
    [HttpPost]
    public async Task<ActionResult<Product>> CreateBrand(Brand brand)
    {
        _context.Brand.Add(brand);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetBrand), new { id = brand.Id }, brand);
    }

    // PUT: api/Brand/5
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateBrand(int id, Brand brand)
    {
        if (id != brand.Id)
        {
            return BadRequest();
        }

        _context.Entry(brand).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!BrandExists(id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    // DELETE: api/Brand/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBrand(int id)
    {
        var brand = await _context.Brand.FindAsync(id);
        if (brand == null)
        {
            return NotFound();
        }

        _context.Brand.Remove(brand);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool BrandExists(int id)
    {
        return _context.Brand.Any(e => e.Id == id);
    }
}
