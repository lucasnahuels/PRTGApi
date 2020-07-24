using Amazon.Extensions.CognitoAuthentication;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApplicationCore.Services.Interfaces
{
    public interface IUserService
    {
        public Task<IEnumerable<CognitoUser>> GetAsync();
        public Task<CognitoUser> GetAsync(string id);
    }
}
