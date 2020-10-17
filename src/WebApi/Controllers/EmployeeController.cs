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
    public class EmployeeController : Controller
    {

        private readonly IEmployeeService _employeeService;
        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployees()
        {
            var result = await _employeeService.GetAsync();
            return Ok(result);
        }

        [HttpGet("GetEmployeesByOwner/{ownerId}")]
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployeesByOwnerId(long ownerId)
        {
            var result = await _employeeService.GetEmployeesByOwnerId(ownerId);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> GetEmployee(int id)
        {
            var employee = await _employeeService.GetAsync(id);

            return employee == null ? NotFound() : (ActionResult<Employee>)employee;
        }

        [HttpPut]
        public async Task<ActionResult<Employee>> PutEmployee(Employee employee)
        {

            await _employeeService.UpdateAsync(employee);

            return employee;
        }

        [HttpPost]
        public async Task<ActionResult<Employee>> PostEmployee(Employee employee)
        {
            await _employeeService.CreateAsync(employee);

            return CreatedAtAction("GetEmployee", new { id = employee.Id }, employee);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Employee>> DeleteEmployee(int id)
        {
            var employee = await _employeeService.GetAsync(id);
            if (employee == null)
            {
                return NotFound();
            }

            await _employeeService.DeleteAsync(employee);

            return employee;
        }
    }
}
