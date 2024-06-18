using Microsoft.AspNetCore.Mvc;
using PredictionsCup.Data;
using PredictionsCup.Models;

namespace PredictionsCup.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PredictionController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PredictionController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Prediction>> GetPredictions()
        {
            return _context.Predictions.ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<Prediction> GetPrediction(int id)
        {
            var prediction = _context.Predictions.Find(id);

            if (prediction == null)
            {
                return NotFound();
            }

            return prediction;
        }

        [HttpPost]
        public async Task<ActionResult<Prediction>> PostPrediction(Prediction prediction)
        {
            _context.Predictions.Add(prediction);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPrediction), new { id = prediction.Id }, prediction);
        }

        [HttpPost("calculate")]
        public ActionResult<int> CalculatePoints([FromBody] Prediction prediction)
        {
            int points = 0;

            if (prediction.Champion == "RealChampion")
                points += 20;

            return points;
        }
    }
}