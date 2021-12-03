using Back.Models;
using Microsoft.AspNetCore.Mvc;

namespace Back.Controllers
{
    [Route("api/master")]
    [ApiController]
    public class MasterController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<Master> Get()
        {
            List<Master> masters;
            using (var db = new AppDBContext())
            {
                masters = db.Masters.ToList();
            }
            return masters;
        }

        [HttpGet("{id}")]
        public ActionResult<Master> Get(int id)
        {
            Master master;
            using (var db = new AppDBContext())
            {
                master = db.Masters.Find(id);
            }
            if (master == null)
                return NotFound();

            return master;
        }

        [HttpPost]
        public ActionResult<Master> Post([FromBody] Master item)
        {
            Master master;
            using (var db = new AppDBContext())
            {
                master = db.Masters.Add(item).Entity;
                db.SaveChanges();
            }

            return master;
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Master item)
        {
            try
            {
                using (var db = new AppDBContext())
                {
                    var editable = db.Masters.Find(id);

                    if (editable == null)
                        return NotFound();

                    if (item.Name != null)
                        editable.Name = item.Name;

                    if (item.Surname != null)
                        editable.Surname = item.Surname;

                    if (item.Patronymic != null)
                        editable.Patronymic = item.Patronymic;

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
                    var forDelete = db.Masters.Find(id);

                    if (forDelete == null)
                        return NotFound();

                    db.Masters.Remove(forDelete);
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
