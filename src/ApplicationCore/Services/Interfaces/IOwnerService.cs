using ApplicationCore.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApplicationCore.Services.Interfaces
{
    public interface IOwnerService
    {
        Task<IEnumerable<Owner>> GetAsync();
        Task<Owner> GetAsync(int id);
        Task<Owner> CreateAsync(Owner owner);
        Task<Owner> UpdateAsync(Owner owner);
        Task<Owner> DeleteAsync(Owner owner);
        Task<bool> Exists(int id);
    }
}
