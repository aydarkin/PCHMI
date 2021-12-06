using Back.Models;
using Microsoft.AspNetCore.Mvc;

namespace Back.Controllers
{
    [Route("api/service")]
    [ApiController]
    public class ServiceController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<Service> Get()
        {
            List<Service> services;
            using (var db = new AppDBContext())
            {
                services = db.Services.ToList();
            }
            return services;
        }

        [HttpGet("{id}")]
        public ActionResult<Service> Get(int id)
        {
            Service service;
            using (var db = new AppDBContext())
            {
                service = db.Services.Find(id);
            }
            if (service == null)
                return NotFound();

            return service;
        }

        [HttpPost]
        public ActionResult<Service> Post([FromBody] Service item)
        {
            Service service;
            using (var db = new AppDBContext())
            {
                service = db.Services.Add(item).Entity;
                db.SaveChanges();
            }

            return service;
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Service item)
        {
            try
            {
                using (var db = new AppDBContext())
                {
                    var editable = db.Services.Find(id);

                    if (editable == null)
                        return NotFound();

                    if (item.Title != null)
                        editable.Title = item.Title;

                    if (item.Cost != 0)
                        editable.Cost = item.Cost;

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
                    var forDelete = db.Services.Find(id);

                    if (forDelete == null)
                        return NotFound();

                    db.Services.Remove(forDelete);
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
