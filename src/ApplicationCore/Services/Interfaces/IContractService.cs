using ApplicationCore.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApplicationCore.Services.Interfaces
{
    public interface IContractService
    {
        Task<IEnumerable<Contract>> GetAsync();
        Task<Contract> GetAsync(int id);
        Task<Contract> CreateAsync(Contract contract);
        Task<Contract> UpdateAsync(Contract contract);
        Task<Contract> DeleteAsync(Contract contract);
        Task<bool> Exists(int id);
        Task<Contract> AssignDevice(Contract contract);
        Task<Contract> UnassignDevice(Contract contract);
        Task<IEnumerable<ContractDevice>> GetContractDevicesRelations();
        Task<IEnumerable<ContractEmployee>> GetContractEmployeesRelations();
        Task<IEnumerable<ContractEmployee>> GetContractEmployeesRelationsByContractId(int contractiD);
        Task<IEnumerable<ContractUser>> GetContractUsersRelations();
        Task<IEnumerable<ContractUser>> GetContractUsersRelationsByContractId(int contractiD);
        Task<Contract> UpdateEmployeesAndUsersRelations(Contract contract);
    }
}
