using Back.Models;
using Microsoft.AspNetCore.Mvc;

namespace Back.Controllers
{
    [Route("api/record")]
    [ApiController]
    public class RecordController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<Record> Get()
        {
            List<Record> records;
            using (var db = new AppDBContext())
            {
                records = db.Records.ToList();
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
                record = db.Records.Add(item).Entity;
                db.SaveChanges();
            }

            return record;
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Record item)
        {
            try
            {
                using (var db = new AppDBContext())
                {
                    var editable = db.Records.Find(id);

                    if (editable == null)
                        return NotFound();

                    db.SaveChanges();
                }

                return Ok();
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
