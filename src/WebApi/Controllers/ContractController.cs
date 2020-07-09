using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;
using WebApi.Services.Interfaces;

namespace PRTG.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContractController : ControllerBase
    {
        private readonly IContractService _contractService;

        public ContractController(IContractService contractService)
        {
            _contractService = contractService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Contract>>> GetContracts()
        {
            var result = await _contractService.GetAsync();

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Contract>> GetContract(int id)
        {
            var contract = await _contractService.GetAsync(id);

            return contract == null ? NotFound() : (ActionResult<Contract>)contract;
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Contract>> PutContract(long id, Contract contract)
        {
            if (id != contract.Id)
            {
                return BadRequest();
            }
            await _contractService.UpdateAsync(contract);

            return contract;
        }

        [HttpPost]
        public async Task<ActionResult<Contract>> PostContract(Contract contract)
        {
            await _contractService.CreateAsync(contract);

            return CreatedAtAction("GetContract", new { id = contract.Id }, contract);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Contract>> DeleteContract(int id)
        {
            var contract = await _contractService.GetAsync(id);
            if (contract == null)
            {
                return NotFound();
            }

            await _contractService.DeleteAsync(contract);

            return contract;
        }
    }
}
