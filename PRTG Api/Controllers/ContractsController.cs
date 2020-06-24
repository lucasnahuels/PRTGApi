using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PRTG_Api.Models;

namespace PRTG.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContractsController : ControllerBase
    {
        private readonly DataBaseContext _context;

        public ContractsController(DataBaseContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Contract>>> GetContracts()
        {
            return await _context.Contract.OrderBy(x => x.NameOfCompany).ToListAsync().ConfigureAwait(false);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Contract>> GetContract(int id)
        {
            var contract = await _context.Contract.FindAsync(id).ConfigureAwait(false);

            return contract == null ? NotFound() : (ActionResult<Contract>)contract;
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Contract>> PutContract(int id, Contract contract)
        {
            if (id != contract.ContractId)
            {
                return BadRequest();
            }

            _context.Entry(contract).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync().ConfigureAwait(false);
            }
            catch (DbUpdateConcurrencyException)
            {
                return !ContractExists(id) ? NotFound() : (ActionResult<Contract>)Problem(title: "There has been an error");
            }

            return contract;
        }

        [HttpPost]
        public async Task<ActionResult<Contract>> PostContract(Contract contract)
        {
            _context.Contract.Add(contract);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetContract", new { id = contract.ContractId }, contract);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Contract>> DeleteContract(int id)
        {
            var contract = await _context.Contract.FindAsync(id);
            if (contract == null)
            {
                return NotFound();
            }

            _context.Contract.Remove(contract);
            await _context.SaveChangesAsync();

            return contract;
        }

        private bool ContractExists(int id)
        {
            return _context.Contract.Any(e => e.ContractId == id);
        }
    }
}
