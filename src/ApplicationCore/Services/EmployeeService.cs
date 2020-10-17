using Microsoft.EntityFrameworkCore;
using ApplicationCore.EntityFramework;
using ApplicationCore.Models;
using ApplicationCore.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace ApplicationCore.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly PrtgDbContext _context;

        public EmployeeService(PrtgDbContext context)
        {
            _context = context;
        }

        public async Task<Employee> CreateAsync(Employee employee)
        {
            await _context.Employees.AddAsync(employee);
            await _context.SaveChangesAsync();
      
            return employee;
        }

        public async Task<Employee> DeleteAsync(Employee employee)
        {
            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();

            return employee;
        }

        public async Task<bool> Exists(int id)
        {
            return await _context.Employees.AnyAsync(employee => employee.Id == id);
        }

        public async Task<IEnumerable<Employee>> GetAsync()
        {
            return await _context.Employees.Include(employee => employee.Owner).ToListAsync();
        }

        public async Task<IEnumerable<Employee>> GetEmployeesByOwnerId(long ownerId)
        {
            return await _context.Employees.Include(employee => employee.Owner).Where(x => x.OwnerId == ownerId).ToListAsync();
        }

        public async Task<Employee> GetAsync(int id)
        {
            return await _context.Employees.Include(employee => employee.Owner).FirstOrDefaultAsync(employee => employee.Id == id);
        }

        public async Task<Employee> UpdateAsync(Employee employee)
        {
            _context.Entry(employee).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return employee;
        }
    }
}
