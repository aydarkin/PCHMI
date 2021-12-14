using Back.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Back.Controllers
{
    [Route("api/record")]
    [ApiController]
    public class RecordController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<Record> Get()
        {
            IEnumerable<Record> records;
            using (var db = new AppDBContext())
            {
                records = db.Records
                    .Include(x=>x.Client)
                    .Include(x=>x.Service)
                    .Include(x=>x.TimeInterval)
                    .Include(x=>x.Master)
                    .ToList();

                records = records.OrderByDescending(l => l.Date).ThenByDescending(l => l.TimeIntervalId).ToList();
            }
            
            return records;
        }

        [HttpGet("{id}")]
        public ActionResult<Record> Get(int id)
        {
            Record record;
            using (var db = new AppDBContext())
            {
                record = db.Records.Find(id);

                db.Clients.Where(c => c.Id == record.ClientId).Load();
                db.Services.Where(c => c.Id == record.ServiceId).Load();
                db.TimeIntervals.Where(c => c.Id == record.TimeIntervalId).Load();
                db.Masters.Where(c => c.Id == record.MasterId).Load();
            }
            if (record == null)
                return NotFound();

            return record;
        }

        [HttpPost]
        public ActionResult<Record> Post([FromBody] Record item)
        {
            Record record;
            using (var db = new AppDBContext())
            {
                if (item.ClientId != null)
                    item.Client = null;

                record = db.Records.Add(item).Entity;
                db.SaveChanges();

                db.Clients.Where(c => c.Id == record.ClientId).Load();
                db.Services.Where(c => c.Id == record.ServiceId).Load();
                db.TimeIntervals.Where(c => c.Id == record.TimeIntervalId).Load();
                db.Masters.Where(c => c.Id == record.MasterId).Load();
            }

            return record;
        }

        [HttpPut("{id}")]
        public ActionResult<Record> Put(int id, [FromBody] Record item)
        {
            try
            {
                using (var db = new AppDBContext())
                {
                    var editable = db.Records.Find(id);

                    if (editable == null)
                        return NotFound();

                    if (item.ClientId != null)
                        editable.ClientId = item.ClientId;

                    if (item.MasterId != null)
                        editable.MasterId = item.MasterId;

                    if (item.ServiceId != null)
                        editable.ServiceId = item.ServiceId;

                    if (item.TimeIntervalId != null)
                        editable.TimeIntervalId = item.TimeIntervalId;

                    if (item.Date != null)
                        editable.Date = item.Date;

                    if (item.Check == "create")
                        editable.Check = new Random().Next(1000000, 9999999).ToString();

                    db.SaveChanges();
                    return editable;
                }
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                using (var db = new AppDBContext())
                {
                    var forDelete = db.Records.Find(id);

                    if (forDelete == null)
                        return NotFound();

                    db.Records.Remove(forDelete);
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
