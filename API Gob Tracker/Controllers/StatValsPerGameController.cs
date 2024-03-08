using API_Gob_Tracker.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API_Gob_Tracker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatValsPerGameController : ControllerBase
    {
        private readonly GobTrackerContext _context;

        public StatValsPerGameController(GobTrackerContext context)
        {
            _context = context;
        }

        // GET: api/AllRawStats
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StatValsPerGame>>> GetStatsValsPerGame()
        {
            return await _context.StatValsPerGames.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<StatValsPerGame>>> GetStatsValsPerGame(int id)
        {
            if (_context.StatValsPerGames == null)
            {
                return NotFound();
            }

            var pstats = await _context.StatValsPerGames.ToListAsync();

            if (pstats == null)
            {
                return NotFound();
            }

            //LINQ
            var resultPlayerStats = pstats.Where(x => x.GameId == id).ToList();

            return resultPlayerStats;
        }
    }
}
