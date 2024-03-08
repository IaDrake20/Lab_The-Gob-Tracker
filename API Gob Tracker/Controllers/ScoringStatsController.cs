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
        public class ScoringStatController : ControllerBase
        {
            private readonly GobTrackerContext _context;

            public ScoringStatController(GobTrackerContext context)
            {
                _context = context;
            }

            // GET: api/ScoringStats
            [HttpGet]
            public async Task<ActionResult<IEnumerable<ScoringStat>>> GetScoringStat()
            {
                return await _context.ScoringStats.ToListAsync();
            }

            [HttpGet("{id}")]
            public async Task<ActionResult<IEnumerable<ScoringStat>>> GetAllScoringStat(int id)
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
                //IAN: for now I just made it hometeamID
                var resultScoringStats = ss.Where(x => x.HomeTeamId == id).ToList();

                return resultScoringStats;
            }
        }
    }
}
