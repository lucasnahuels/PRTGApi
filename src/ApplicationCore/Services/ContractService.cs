using Microsoft.EntityFrameworkCore;
using ApplicationCore.EntityFramework;
using ApplicationCore.Models;
using ApplicationCore.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Internal;
using System.Linq;
using System;
using ApplicationCore.Services.Interfaces.Reports;

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
        public async Task<IEnumerable<ContractUser>> GetContractUsersRelations()
        {
            return await _context.ContractUsers.ToListAsync();
        }

        public async Task<IEnumerable<ContractUser>> GetContractUsersRelationsByContractId(int contractiD)
        {
            var contractUsersRelations = await GetContractUsersRelations();
            return contractUsersRelations.Where(x => x.ContractId == contractiD);
        }

        public async Task<Contract> UpdateAsync(Contract contract)
        {
            await CheckNewDevices(contract);
            await CheckNewUsers(contract);
            
            _context.Entry(contract).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        
            return contract;
        }

        public async Task<Contract> UpdateEmployeesAndUsersRelations(Contract contract)
        {
            var contractToPut = await GetAsync(Convert.ToInt32(contract.Id));

            contractToPut = UpdateEmployeesContractRelations(contract, contractToPut);
            //contractToPut = UpdateUsersContractRelations(contract, contractToPut);

            await UpdateAsync(contractToPut);

            return contractToPut;
        }

        private static Contract UpdateEmployeesContractRelations(Contract contract, Contract contractToPut)
        {
            //add new relations
            foreach (var contractEmployee in contract.ContractEmployees)
            {
                var found = false;
                foreach (var previouscontractEmployee in contractToPut.ContractEmployees)
                {
                    if (contractEmployee.EmployeeId == previouscontractEmployee.EmployeeId)
                        found = true;
                }
                if (!found)
                    contractToPut.ContractEmployees.Add(contractEmployee);
            }

            //remove obsolete relations
            var contractEmployeeRelationsToRemove = new List<ContractEmployee>();
            foreach (var previouscontractEmployee in contractToPut.ContractEmployees)
            {
                var found = false;
                foreach (var contractEmployee in contract.ContractEmployees)
                {
                    if (previouscontractEmployee.EmployeeId == contractEmployee.EmployeeId)
                        found = true;
                }
                if (!found)
                    contractEmployeeRelationsToRemove.Add(previouscontractEmployee);
            }
            foreach (var itemToRemove in contractEmployeeRelationsToRemove)
            {
                contractToPut.ContractEmployees.Remove(itemToRemove);
            }

            return contractToPut;
        }

        private static Contract UpdateUsersContractRelations(Contract contract, Contract contractToPut)
        {
            //add new relations
            foreach (var contractUser in contract.ContractUsers)
            {
                var found = false;
                foreach (var previouscontractUser in contractToPut.ContractUsers)
                {
                    if (contractUser.User_Id == previouscontractUser.User_Id)
                        found = true;
                }
                if(!found)
                    contractToPut.ContractUsers.Add(contractUser);
            }

            //remove obsolete relations
            var contractUserRelationsToRemove = new List<ContractUser>();
            foreach (var previouscontractUser in contractToPut.ContractUsers)
            {
                var found = false;
                foreach (var contractUser in contract.ContractUsers)
                {
                    if (previouscontractUser.User_Id == contractUser.User_Id)
                        found = true;
                }
                if (!found)
                    contractUserRelationsToRemove.Add(previouscontractUser);
            }
            foreach (var itemToRemove in contractUserRelationsToRemove)
            {
                contractToPut.ContractUsers.Remove(itemToRemove);
            }

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
            var userIds = await _context.Users.Select(d => d.User_Id).ToListAsync();
            if (contract.ContractUsers != null)
            {
                contract.ContractUsers.ToList().ForEach(cu =>
                {
                    if (!userIds.Any(userId => userId == cu.User_Id))
                    {
                        cu.User = new User() { User_Id = cu.User_Id };
                        cu.User_Id = null;
                    }
                });
            }
        }
    }
}
