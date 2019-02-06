using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JS_Grid_Testing.Data;
using JS_Grid_Testing.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JS_Grid_Testing.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeopleController : ControllerBase
    {

        private readonly ApplicationDbContext _context;

        public PeopleController(ApplicationDbContext context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<Person>>> GetPeople()
        {
            var people = await _context.People.ToListAsync();
            return people;
        }

        [HttpGet("search")]
        public ActionResult SearchPeople(string term)
        {
            var people = _context.People.Where(p => p.Name.Contains(term) || p.Age.ToString().Contains(term)).ToList();

            var results = new List<AutocompleteResult>();

            foreach(var person in people)
            {
                results.Add(new AutocompleteResult
                {
                    Id = person.Id,

                });
            }

            return new JsonResult();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Person>> GetPerson(int id)
        {
            var person = await _context.People.FindAsync(id);

            if(person == null)
            {
                return NotFound();
            }

            return person;

        }


    }

    public class AutocompleteResult
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
    }
}