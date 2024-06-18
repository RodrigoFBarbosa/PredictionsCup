using Microsoft.AspNetCore.Mvc;
using PredictionsCup.Data;
using PredictionsCup.Models;

namespace PredictionsCup.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SemifinalistController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public SemifinalistController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public ActionResult<IEnumerable<SemiFinalist>> GetSemifinalists()
    {
        return _context.Semifinalists.ToList();
    }

    [HttpGet("{id}")]
    public ActionResult<SemiFinalist> GetSemifinalist(int id)
    {
        var semifinalist = _context.Semifinalists.Find(id);

        if (semifinalist == null)
        {
            return NotFound();
        }
        return semifinalist;
    }

    [HttpPost]
    public async Task<ActionResult<SemiFinalist>> PostSemifinalist(SemiFinalist semifinalist)
    {
        _context.Semifinalists.Add(semifinalist);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetSemifinalist), new { id = semifinalist.Id }, semifinalist);
    }
}
