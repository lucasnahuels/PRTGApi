using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ApplicationCore.Models;
using ApplicationCore.Services.Interfaces;
using System;
using System.Collections;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
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
            //order by contract.ownerName
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Contract>> GetContract(int id)
        {
            var contract = await _contractService.GetAsync(id);
            return contract == null ? NotFound() : (ActionResult<Contract>)contract;
        }

        [HttpGet("getContractEmployeesRelations/{id}")]
        public async Task<ActionResult<IEnumerable<ContractEmployee>>> GetContractEmployeesRelationsByContractId(int id)
        {
            return Ok(await _contractService.GetContractEmployeesRelationsByContractId(id));
        }

        [HttpGet("getContractUsersRelations/{id}")]
        public async Task<ActionResult<IEnumerable<ContractUser>>> GetContractUsersRelationsByContractId(int id)
        {
            return Ok(await _contractService.GetContractUsersRelationsByContractId(id));
        }

        [HttpPut]
        public async Task<ActionResult<Contract>> PutContract(Contract contract)
        {
            await _contractService.UpdateAsync(contract);
            return contract;
        }

        [Route("updateEmployeesAndUsers")]
        [HttpPut]
        public async Task<ActionResult<Contract>> UpdateEmployeesAndUsers(Contract contract)
        {
            return Ok(await _contractService.UpdateEmployeesAndUsersRelations(contract));
        }

        [Route("assignDevice")]
        [HttpPut]
        public async Task<ActionResult<Contract>> AssignDevice(Contract contract)
        {
            return Ok(await _contractService.AssignDevice(contract));
        }

        [Route("unassignDevice")]
        [HttpPut]
        public async Task<ActionResult<Contract>> UnassignDevice(Contract contract)
        {
            var deviceAssigned = await _contractService.UnassignDevice(contract);

            return deviceAssigned;
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
