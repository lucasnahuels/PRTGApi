using ApplicationCore.Models.Auth0;
using System.Threading.Tasks;

namespace ApplicationCore.Services.Interfaces
{
    public interface IAuth0ManagementService
    {
        Task<TokenResponse> GetToken();
    }
}