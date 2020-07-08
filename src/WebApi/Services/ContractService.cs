using Microsoft.EntityFrameworkCore;
using PRTG_Api.EntityFramework;
using PRTG_Api.Models;
using PRTG_Api.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PRTG_Api.Services
{
    public class ContractService : IContractService
    {
        private readonly DataBaseContext _context;

        public ContractService(DataBaseContext context)
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
            return await _context.Contracts.AnyAsync(contract => contract.ContractId == id);
        }

        public async Task<IEnumerable<Contract>> GetAsync()
        {
            return await _context.Contracts.ToListAsync();
        }

        public async Task<Contract> GetAsync(int id)
        {
            return await _context.Contracts.FirstOrDefaultAsync(contract => contract.ContractId == id);
        }

        public async Task<Contract> UpdateAsync(Contract contract)
        {
            _context.Entry(contract).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return contract;
        }
    }
}
