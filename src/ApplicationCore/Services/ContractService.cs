using Microsoft.EntityFrameworkCore;
using ApplicationCore.EntityFramework;
using ApplicationCore.Models;
using ApplicationCore.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class ContractService : IContractService
    {
        private readonly PrtgDbContext _context;

        public ContractService(PrtgDbContext context)
        {
            _context = context;
        }

        public async Task<Contract> CreateAsync(Contract contract)
        {
            await _context.Contracts.AddAsync(contract);
            await _context.SaveChangesAsync();

            return contract;
        }

        public async Task<Contract> DeleteAsync(Contract contract)
        {
            _context.Contracts.Remove(contract);
            await _context.SaveChangesAsync();

            return contract;
        }

        public async Task<bool> Exists(int id)
        {
            return await _context.Contracts.AnyAsync(contract => contract.Id == id);
        }

        public async Task<IEnumerable<Contract>> GetAsync()
        {
            return await _context.Contracts.ToListAsync();
        }

        public async Task<Contract> GetAsync(int id)
        {
            return await _context.Contracts.FirstOrDefaultAsync(contract => contract.Id == id);
        }

        public async Task<Contract> UpdateAsync(Contract contract)
        {
            _context.Entry(contract).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return contract;
        }
    }
}
