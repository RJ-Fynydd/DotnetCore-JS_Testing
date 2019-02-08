using JS_Grid_Testing.Data;
using JS_Grid_Testing.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

            var results = new List<AutoCompleteResult>();

            foreach (var person in people)
            {
                results.Add(new AutoCompleteResult
                {
                    Label = person.Name,
                    Value = person.Name
                });
            }

            return new JsonResult(results);
        }

        [HttpGet("selected")]
        public ActionResult SelectedPerson()
        {
            var person = _context.Properties.Where(p => p.Key.Equals("SelectedPerson")).First();

            return new JsonResult(new AutoCompleteResult()
            {
                Value = person.Value,
                Label = person.Value
            });
        }

        [HttpPost("selected")]
        public ActionResult SetSelectedPerson([FromBody] AutoCompleteResult autoCompleteResult)
        {

            if(_context.People.Where(p => p.Name.Equals(autoCompleteResult.Value)).Any())
            {
                try
                {
                    var selectedPerson = _context.Properties.Where(p => p.Key.Equals("SelectedPerson")).First();

                    selectedPerson.Value = autoCompleteResult.Value;

                    _context.Properties.Update(selectedPerson);

                    _context.SaveChanges();

                    return new JsonResult(autoCompleteResult);
                }
                catch (Exception e)
                {
                    return new BadRequestResult();
                }
            }
            else
            {
                return new BadRequestResult();
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Person>> GetPerson(int id)
        {
            var person = await _context.People.FindAsync(id);

            if (person == null)
            {
                return NotFound();
            }

            return person;

        }


    }

    public class AutoCompleteResult
    {
        public string Label { get; set; }
        public string Value { get; set; }
    }
}