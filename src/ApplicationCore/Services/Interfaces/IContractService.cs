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
    }
}
