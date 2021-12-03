using Back.Models;
using Microsoft.AspNetCore.Mvc;

namespace Back.Controllers
{
    [Route("api/client")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<Client> Get()
        {
            List<Client> clients;
            using (var db = new AppDBContext())
            {
                clients = db.Clients.ToList();
            }
            return clients;
        }

        [HttpGet("{id}")]
        public ActionResult<Client> Get(int id)
        {
            Client client;
            using (var db = new AppDBContext())
            {
                client = db.Clients.Find(id);
            }
            if (client == null)
                return NotFound();

            return client;
        }

        [HttpPost]
        public ActionResult<Client> Post([FromBody] Client item)
        {
            Client client;
            using (var db = new AppDBContext())
            {
                client = db.Clients.Add(item).Entity;
                db.SaveChanges();
            }

            return client;
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Client item)
        {
            try
            {
                using (var db = new AppDBContext())
                {
                    var editable = db.Clients.Find(id);

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
                    var forDelete = db.Clients.Find(id);

                    if (forDelete == null)
                        return NotFound();

                    db.Clients.Remove(forDelete);
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
