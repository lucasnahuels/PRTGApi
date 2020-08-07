using Microsoft.EntityFrameworkCore;
using ApplicationCore.EntityFramework;
using ApplicationCore.Models;
using ApplicationCore.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Internal;
using System.Linq;

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
            await CheckNewDevices(contract);

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
            return await _context.Contracts
                .Include(c => c.Owner)
                .Include(c => c.ContractDevices).ThenInclude(c => c.Device)
                .Include(c => c.ContractEmployees).ThenInclude(c => c.Employee)
                .Include(c => c.ContractUsers).ThenInclude(c => c.User)
                .ToListAsync();
        }

        public async Task<Contract> GetAsync(int id)
        {
            return await _context.Contracts
                .Include(c => c.Owner)
                .Include(c => c.ContractDevices).ThenInclude(c => c.Device)
                .Include(c => c.ContractEmployees).ThenInclude(c => c.Employee)
                .Include(c => c.ContractUsers).ThenInclude(c => c.User)
                .FirstOrDefaultAsync(contract => contract.Id == id);
        }

        public async Task<Contract> UpdateAsync(Contract contract)
        {
            _context.Entry(contract).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return contract;
        }

        private async Task CheckNewDevices(Contract contract)
        {
            var deviceIds = await _context.Devices.Select(d => d.ObjId).ToListAsync();
            contract.ContractDevices.ToList().ForEach(cd =>
            {
                if (!deviceIds.Any(deviceId => deviceId == cd.ObjId))
                {
                    cd.Device = new Device() { ObjId = cd.ObjId };
                    cd.ObjId = null;
                }
            });
        }
    }
}
