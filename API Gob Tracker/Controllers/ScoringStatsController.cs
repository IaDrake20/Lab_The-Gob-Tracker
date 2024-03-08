using API_Gob_Tracker.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API_Gob_Tracker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScoringStatsController : ControllerBase
    {
        [Route("api/[controller]")]
        [ApiController]
        public class ScoringStatController : Controller
        {
            private readonly GobTrackerContext _context; // Replace YourDbContext with your actual DbContext

            public ScoringStatController(GobTrackerContext context)
            {
                _context = context;
            }

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

        }
    }
    }
