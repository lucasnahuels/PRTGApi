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
    public class OwnerController : Controller
    {

        private readonly IOwnerService _ownerService;
        public OwnerController(IOwnerService ownerService)
        {
            _ownerService = ownerService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Owner>>> GetOwners()
        {
            var result = await _ownerService.GetAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Owner>> GetOwner(int id)
        {
            var owner = await _ownerService.GetAsync(id);

            return owner == null ? NotFound() : (ActionResult<Owner>)owner;
        }

        [HttpPut]
        public async Task<ActionResult<Owner>> PutOwner(Owner owner)
        {
            await _ownerService.UpdateAsync(owner);

            return owner;
        }

        [HttpPost]
        public async Task<ActionResult<Owner>> PostOwner(Owner owner)
        {
            await _ownerService.CreateAsync(owner);

            return CreatedAtAction("GetOwner", new { id = owner.Id }, owner);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Owner>> DeleteOwner(int id)
        {
            var owner = await _ownerService.GetAsync(id);
            if (owner == null)
            {
                return NotFound();
            }

            await _ownerService.DeleteAsync(owner);

            return owner;
        }
    }
}
