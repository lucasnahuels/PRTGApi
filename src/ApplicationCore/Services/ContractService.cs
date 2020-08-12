using Microsoft.EntityFrameworkCore;
using ApplicationCore.EntityFramework;
using ApplicationCore.Models;
using ApplicationCore.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Internal;
using System.Linq;
using System;

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
            await CheckNewUsers(contract);

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

        public async Task<IEnumerable<ContractDevice>> GetContractDevicesRelations()
        {
            return await _context.ContractDevices.ToListAsync();
        }
        public async Task<IEnumerable<ContractEmployee>> GetContractEmployeesRelations()
        {
            return await _context.ContractEmployees.ToListAsync();
        }

        public async Task<IEnumerable<ContractEmployee>> GetContractEmployeesRelationsByContractId(int contractiD)
        {
            var contractEmployeesRelations= await GetContractEmployeesRelations();
            return contractEmployeesRelations.Where(x => x.ContractId == contractiD);
        }

        public async Task<Contract> UpdateAsync(Contract contract)
        {
            await CheckNewDevices(contract);
            await CheckNewUsers(contract);
            _context.Entry(contract).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return contract;
        }

        public async Task<Contract> UpdateEmployeesAndUsers(Contract contract)
        {
            var contractToPut = await GetAsync(Convert.ToInt32(contract.Id));
            
            List<ContractEmployee> contractEmployeesList = contract.ContractEmployees as List<ContractEmployee>;
            foreach (var contractEmployee in contractEmployeesList)
            {
                contractToPut.ContractEmployees.Add(contractEmployee);
            }

            //List<ContractUser> contractUsersList = contract.ContractUsers as List<ContractUser>;
            //foreach (var contractUser in contractUsersList)
            //{
            //    contractToPut.ContractUsers.Add(contractUser);
            //}

            await UpdateAsync(contractToPut);

            return contractToPut;
        }


        public async Task<Contract> AssignDevice(Contract contract)
        {
            var contractToPut = await GetAsync(Convert.ToInt32(contract.Id));

            List<ContractDevice> contractDevicesList = contract.ContractDevices as List<ContractDevice>;
            foreach (var contractDevice in contractDevicesList)
            {
                contractToPut.ContractDevices.Add(contractDevice);
            }

            await UpdateAsync(contractToPut);

            return contractToPut;
        }

        public async Task<Contract> UnassignDevice(Contract contract)
        {
            List<ContractDevice> contractDevicesList = contract.ContractDevices as List<ContractDevice>;
            var deviceToRemove = new ContractDevice();
            
            var contractToPut = await GetAsync(Convert.ToInt32(contract.Id));

            foreach (var contractDevice in contractDevicesList)
            {
                foreach(var existingContractDevice in contractToPut.ContractDevices)
                {
                    if (contractDevice.ObjId == existingContractDevice.ObjId)
                    {
                        deviceToRemove = existingContractDevice;
                    }
                }
            }

            contractToPut.ContractDevices.Remove(deviceToRemove);

            await UpdateAsync(contractToPut);

            return contractToPut;
        }

        private async Task CheckNewDevices(Contract contract)
        {
            var deviceIds = await _context.Devices.Select(d => d.ObjId).ToListAsync();
            if (contract.ContractDevices != null)
            {
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

        private async Task CheckNewUsers(Contract contract)
        {
            var userIds = await _context.Users.Select(d => d.UserId).ToListAsync();
            if (contract.ContractUsers != null)
            {
                contract.ContractUsers.ToList().ForEach(cu =>
                {
                    if (!userIds.Any(userId => userId == cu.UserId))
                    {
                        cu.User = new User() { UserId = cu.UserId };
                        cu.UserId = null;
                    }
                });
            }
        }
    }
}
