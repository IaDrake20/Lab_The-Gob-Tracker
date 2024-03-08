using API_Gob_Tracker.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API_Gob_Tracker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScoringStatController : ControllerBase
    {
        private readonly GobTrackerContext _context; // Replace YourDbContext with your actual DbContext

        public ScoringStatController(GobTrackerContext context)
        {
            _context = context;
        }

        // GET: api/AllSeasonStats
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ScoringStat>>> GetScoringStat()
        {
            return await _context.ScoringStats.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<ScoringStat>>> GetAllSeasonStat(int id)
        {

            if (_context.ScoringStats == null)
            {
                return NotFound();
            }

            var ss = await _context.ScoringStats.ToListAsync();

            if (ss == null)
            {
                return NotFound();
            }

            //LINQ
            var resultScoringStats = ss.Where(x => x.TeamID == id).ToList();

            return resultScoringStats;
        }

        /*
        // Action to display all scoring stats
        public IActionResult Index()
        {
            var scoringStats = _context.ScoringStats.ToList();
            return View(scoringStats);
        }

        // Action to display details of a specific scoring stat
        public IActionResult Details(int id)
        {
            var scoringStat = _context.ScoringStats.FirstOrDefault(s => s.GameID == id);
            if (scoringStat == null)
            {
                return NotFound();
            }
            return View(scoringStat);
        }
        */
    }
}
