using ecommapi.Data;
using ecommapi.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ecommapi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrderController: ControllerBase
{
    private readonly ApplicationDbContext _context;

    public OrderController(ApplicationDbContext context)
    {
        _context = context;
    }

    // POST: api/Order
    [HttpPost]
    public async Task<ActionResult> Order([FromBody] Order order)
    {
        _context.Order.Add(order);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetOrder), new { id = order.Id }, order);
    }

    // GET: api/Order
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Order>>> GetOrder()
    {
        return await _context.Order.ToListAsync();
    }

    // GET: api/Order/5
    [HttpGet("{id}")]
    public async Task<ActionResult> GetOrder(int id)
    {
        var order = await _context.Order.FindAsync(id);

        if (order == null)
        {
            return NotFound();
        }

        return Ok(order);
    }

    // PUT: api/Order/5
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateOrder([FromRoute]int id, [FromBody]Order order)
    {
        if (id != order.Id)
        {
            return BadRequest();
        }

        _context.Entry(order).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!OrderExists(id))
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

    // DELETE: api/Order/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteOrder(int id)
    {
        var order = await _context.Order.FindAsync(id);
        if (order == null)
        {
            return NotFound();
        }

        _context.Order.Remove(order);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool OrderExists(int id)
    {
        return _context.Order.Any(e => e.Id == id);
    }
}