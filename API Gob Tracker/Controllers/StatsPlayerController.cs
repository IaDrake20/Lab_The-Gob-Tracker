using API_Gob_Tracker.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API_Gob_Tracker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatsPlayerController : ControllerBase
    {
        private readonly GobTrackerContext _context;

        public StatsPlayerController(GobTrackerContext context)
        {
            _context = context;
        }

        // GET: api/AllRawStats
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StatsPlayer>>> GetStatsPlayer()
        {
            return await _context.StatsPlayers.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<StatsPlayer>>> GetStatsPlayer(int id)
        {
            var pstat = await _context.StatsPlayers.FindAsync(id);

            if(_context.StatsPlayers == null)
            {
                return NotFound();
            }

            var pstats = await _context.StatsPlayers.ToListAsync();

            if(pstats == null)
            {
                return NotFound();
            }

            //LINQ
            var resultPlayerStats = pstats.Where(x => x.Id == id).ToList();

            return resultPlayerStats;
        }
    }
}

