using API_Gob_Tracker.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API_Gob_Tracker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AllRawStatsController : ControllerBase
    {
        private readonly GobTrackerContext _context;

        public AllRawStatsController(GobTrackerContext context)
        {
            _context = context;
        }

        // GET: api/AllRawStats
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AllRawStat>>> GetAllRawStat()
        {
            return await _context.AllRawStats.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<AllRawStat>>> GetAllRawStat(int id)
        {

            if (_context.AllRawStats == null)
            {
                return NotFound();
            }

            var ars = await _context.AllRawStats.ToListAsync();

            if (ars == null)
            {
                return NotFound();
            }

            //LINQ
            var resultAllRawStats = ars.Where(x => x.Id == id).ToList();

            return resultAllRawStats;
        }
    }
}
