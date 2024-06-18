using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PredictionsCup.Data;
using PredictionsCup.Models;

namespace PredictionsCup.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BestPlayerController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public BestPlayerController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public ActionResult<IEnumerable<BestPlayer>> GetBestPlayers()
    {
        return _context.BestPlayers.ToList();
    }

    [HttpGet("{id}")]
    public ActionResult<BestPlayer> GetBestPlayer(int id)
    {
        var bestPlayer = _context.BestPlayers.Find(id);

        if (bestPlayer == null)
        {
            return NotFound();
        }

        return bestPlayer;
    }

    [HttpPost]
    public async Task<ActionResult<BestPlayer>> PostBestPlayer(BestPlayer bestPlayer)
    {
        
        var existingBestPlayer = await _context.BestPlayers.FirstOrDefaultAsync(b => b.UserId == bestPlayer.UserId);
        if (existingBestPlayer != null)
        {
            existingBestPlayer.Name = bestPlayer.Name;
            await _context.SaveChangesAsync();
            return Ok(existingBestPlayer);
        }

        _context.BestPlayers.Add(bestPlayer);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetBestPlayer), new { id = bestPlayer.Id }, bestPlayer);
    }
}
