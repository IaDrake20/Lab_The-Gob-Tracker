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
        public async Task<ActionResult<IEnumerable<TeamRoster>>> GetTeamRoster(int id)
        {

            if (_context.TeamRosters == null)
            {
                return NotFound();
            }

            var allRosters = await _context.TeamRosters.ToListAsync();

            if (allRosters == null)
            {
                return NotFound();
            }

            //LINQ
            var resultTrs = allRosters.Where(x => x.TeamId == id).ToList();

            return resultTrs;
        }
    }
}
