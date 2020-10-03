using Amazon.AspNetCore.Identity.Cognito;
using Amazon.Extensions.CognitoAuthentication;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationCore.Services.Interfaces;

namespace ApplicationCore.Services
{
    public class UserService : IUserService
    {
        //private readonly UserManager<CognitoUser> userManager;

        //public UserService(UserManager<CognitoUser> userManager)
        //{
        //    this.userManager = userManager;
        //}
        //public async Task<IEnumerable<CognitoUser>> GetAsync()
        //{
        //    return await (userManager as CognitoUserManager<CognitoUser>).GetUsersAsync();
        //}
        //public async Task<CognitoUser> GetAsync(string id)
        //{
        //    var users = await (userManager as CognitoUserManager<CognitoUser>).GetUsersAsync();
        //    return users.FirstOrDefault(u => u.UserID == id);
        //}
    }
}
