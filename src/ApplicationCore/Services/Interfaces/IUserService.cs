using ApplicationCore.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApplicationCore.Services.Interfaces
{
    public interface IUserService
    {
        public Task<IEnumerable<Auth0User>> GetAsync();
        public Task<Auth0User> GetAsync(string id);
    }
}
