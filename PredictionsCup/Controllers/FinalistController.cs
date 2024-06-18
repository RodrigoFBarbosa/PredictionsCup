using Microsoft.AspNetCore.Mvc;
using PredictionsCup.Data;
using PredictionsCup.Models;

namespace PredictionsCup.Controllers;

[Route("api/[controller]")]
[ApiController]
public class FinalistController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public FinalistController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Finalist>> GetFinalists()
    {
        return _context.Finalists.ToList();
    }

    [HttpGet("{id}")]
    public ActionResult<Finalist> GetFinalist(int id)
    {
        var finalist = _context.Finalists.Find(id);

        if (finalist == null)
        {
            return NotFound();
        }

        return finalist;
    }

    [HttpPost]
    public async Task<ActionResult<Finalist>> PostFinalist(Finalist finalist)
    {
        _context.Finalists.Add(finalist);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetFinalist), new { id = finalist.Id }, finalist);
    }
}
