using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PredictionsCup.Data;
using PredictionsCup.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PredictionsCup.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public UserController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<User>> GetUsers()
        {
            return _context.Users.Include(u => u.TopScorer).Include(u => u.BestPlayer).ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<User> GetUser(int id)
        {
            var user = _context.Users.Include(u => u.TopScorer).Include(u => u.BestPlayer).FirstOrDefault(u => u.Id == id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
        }
    }
}