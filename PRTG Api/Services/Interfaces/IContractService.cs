using PRTG_Api.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PRTG_Api.Services.Interfaces
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
