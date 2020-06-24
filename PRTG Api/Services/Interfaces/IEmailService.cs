using PRTG_Api.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PRTG_Api.Services.Interfaces
{
    public interface IEmailService
    {
        Task<IEnumerable<Email>> GetAsync();
        Task<Email> GetAsync(int id);
        Task<Email> CreateAsync(Email email);
        Task<Email> UpdateAsync(Email email);
        Task<Email> DeleteAsync(Email email);
    }
}
