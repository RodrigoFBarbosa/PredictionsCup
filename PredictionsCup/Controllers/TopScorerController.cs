using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PredictionsCup.Data;
using PredictionsCup.Models;

namespace PredictionsCup.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TopScorerController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public TopScorerController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public ActionResult<IEnumerable<TopScorer>> GetTopScorers()
    {
        return _context.TopScorers.ToList();
    }

    [HttpGet("{id}")]
    public ActionResult<TopScorer> GetTopScorer(int id)
    {
        var topScorer = _context.TopScorers.Find(id);

        if (topScorer == null)
        {
            return NotFound();
        }

        return topScorer;
    }

    [HttpPost]
    public async Task<ActionResult<TopScorer>> PostTopScorer(TopScorer topScorer)
    {
        var existingTopScorer = await _context.TopScorers.FirstOrDefaultAsync(t => t.UserId == topScorer.UserId);
        if (existingTopScorer != null)
        {
            existingTopScorer.Name = topScorer.Name;
            await _context.SaveChangesAsync();
            return Ok(existingTopScorer);
        }

        _context.TopScorers.Add(topScorer);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetTopScorer), new { id = topScorer.Id }, topScorer);
    }
}
