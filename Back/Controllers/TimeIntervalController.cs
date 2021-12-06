using Back.Models;
using Microsoft.AspNetCore.Mvc;

namespace Back.Controllers
{
    [Route("api/timeinterval")]
    [ApiController]
    public class TimeIntervalController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<TimeInterval> Get()
        {
            List<TimeInterval> timeIntervals;
            using (var db = new AppDBContext())
            {
                timeIntervals = db.TimeIntervals.ToList();
            }
            return timeIntervals;
        }

        [HttpGet("{id}")]
        public ActionResult<TimeInterval> Get(int id)
        {
            TimeInterval timeInterval;
            using (var db = new AppDBContext())
            {
                timeInterval = db.TimeIntervals.Find(id);
            }
            if (timeInterval == null)
                return NotFound();

            return timeInterval;
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] TimeInterval item)
        {
            try
            {
                using (var db = new AppDBContext())
                {
                    var editable = db.TimeIntervals.Find(id);

                    if (editable == null)
                        return NotFound();

                    if (item.Title != null)
                        editable.Title = item.Title;

                    db.SaveChanges();
                }

                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
