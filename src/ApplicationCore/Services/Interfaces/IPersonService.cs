using ApplicationCore.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApplicationCore.Services.Interfaces
{
    public interface IPersonService
    {
        Task<IEnumerable<Person>> GetAsync();
        Task<Person> GetAsync(int id);
        Task<Person> CreateAsync(Person person);
        Task<Person> UpdateAsync(Person person);
        Task<Person> DeleteAsync(Person person);
        Task<bool> Exists(int id);
    }
}
