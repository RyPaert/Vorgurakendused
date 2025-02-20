using backend.Model;
using Backend.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend.Controllers
{
    public class PersonsController : ControllerBase
    {
        private readonly DataContext context;
        public PersonsController(DataContext c)
        {
            context = c;
        }
        [HttpGet]
        public IActionResult GetPersons()
        {
            var events = context.Persons!.AsQueryable();
            return Ok(events);
        }
        [HttpPost]
        public IActionResult Create([FromBody] Person p)
        {
            var dbPerson = context.Persons?.Find(p.Id);
            if (dbPerson == null)
            {
                context.Persons?.Add(p);
                context.SaveChanges();
                return CreatedAtAction(nameof(GetPersons), new { p.Id }, p);
            }
            return Conflict();
        }
        [HttpPut("{id}")]
        public IActionResult Update(int? id, [FromBody] Person p)
        {
            var dbPerson = context.Persons!.AsNoTracking().FirstOrDefault(personInDb => personInDb.Id == p.Id);
            if (id != p.Id || dbPerson == null) return NotFound();
            context.Update(p);
            context.SaveChanges();
            return NoContent();
        }
        [HttpDelete("{id}")] public IActionResult Delete(int id)
        {
            var personToDelete = context.Persons?.Find(id);
            if (personToDelete == null) return NotFound();
            context.Persons?.Remove(personToDelete);
            context.SaveChanges();
            return NoContent();
        }
    }
}
