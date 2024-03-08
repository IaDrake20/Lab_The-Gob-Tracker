using API_Gob_Tracker.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API_Gob_Tracker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeasonStatController : ControllerBase
    {
        private readonly GobTrackerContext _context;

        public SeasonStatController(GobTrackerContext context)
        {
            _context = context;
        }

        // GET: api/AllSeasonStats
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SeasonStat>>> GetAllSeasonStat()
        {
            return await _context.SeasonStats.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<SeasonStat>>> GetAllSeasonStat(int id)
        {

            if (_context.SeasonStats == null)
            {
                return NotFound();
            }

            var ss = await _context.SeasonStats.ToListAsync();

            if (ss == null)
            {
                return NotFound();
            }

            //LINQ
            var resultAllSeasonStats = ss.Where(x => x.Id == id).ToList();

            return resultAllSeasonStats;
        }
    }
}
