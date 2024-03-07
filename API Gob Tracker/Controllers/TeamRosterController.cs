using API_Gob_Tracker.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API_Gob_Tracker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamRosterController : ControllerBase
    {
        private readonly GobTrackerContext _context;

        public TeamRosterController(GobTrackerContext context)
        {
            _context = context;
        }

        // GET: api/TeamRoster
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TeamRoster>>> GetTeamRoster()
        {
            return await _context.TeamRosters.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<StatsPlayer>>> GetTeamRoster(int id)
        {
            var _trs = await _context.TeamRosters.FindAsync(id);

            if (_context.TeamRosters == null)
            {
                return NotFound();
            }

            var trs = await _context.TeamRosters.ToListAsync();

            if (trs == null)
            {
                return NotFound();
            }

            //LINQ
            var resultTrs = trs.Where(x => x.Id == id).ToList();

            return resultTrs;
        }
    }
}
