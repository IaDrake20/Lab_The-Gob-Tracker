using API_Gob_Tracker.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API_Gob_Tracker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScheduleController : ControllerBase
    {
        private readonly GobTrackerContext _context;

        public ScheduleController(GobTrackerContext context)
        {
            _context = context;
        }

        // GET: api/AllRawStats
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Schedule>>> GetSchedule()
        {
            return await _context.Schedules.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<StatsPlayer>>> GetSchedule(int id)
        {
            var _schedule = await _context.Schedules.FindAsync(id);

            if (_context.Schedules == null)
            {
                return NotFound();
            }

            var schedules = await _context.Schedules.ToListAsync();

            if (schedules == null)
            {
                return NotFound();
            }

            //LINQ
            var resultScheds = schedules.Where(x => x.Id == id).ToList();

            return resultScheds;
        }
    }
}
