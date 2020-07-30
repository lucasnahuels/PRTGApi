using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ApplicationCore.Models;
using ApplicationCore.Services.Interfaces;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PersonController : Controller
    {

        private readonly IPersonService _personService;
        public PersonController(IPersonService personService)
        {
            _personService = personService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Person>>> GetPersons()
        {
            var result = await _personService.GetAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Person>> GetPerson(int id)
        {
            var person = await _personService.GetAsync(id);

            return person == null ? NotFound() : (ActionResult<Person>)person;
        }

        [HttpPut]
        public async Task<ActionResult<Person>> PutPerson(Person person)
        {

            await _personService.UpdateAsync(person);

            return person;
        }

        [HttpPost]
        public async Task<ActionResult<Person>> PostPerson(Person person)
        {
            await _personService.CreateAsync(person);

            return CreatedAtAction("GetPerson", new { id = person.Id }, person);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Person>> DeletePerson(int id)
        {
            var person = await _personService.GetAsync(id);
            if (person == null)
            {
                return NotFound();
            }

            await _personService.DeleteAsync(person);

            return person;
        }
    }
}
