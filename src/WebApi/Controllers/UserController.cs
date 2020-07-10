using Amazon;
using Amazon.CognitoIdentityProvider;
using Amazon.CognitoIdentityProvider.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.Services.Interfaces;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IContractService _contractService;

        public UserController(IContractService contractService)
        {
            _contractService = contractService;
        }

        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<IdentityUser>>> GetUsers()
        //{
        //    var cognito = new AmazonCognitoIdentityProviderClient(RegionEndpoint.USEast1);
        //    var variable = await cognito.ListUsersAsync(new ListUsersRequest()
        //    {
        //        UserPoolId = ""
        //    });

        //    //variable.Users
        //    var result = await _contractService.GetAsync();

        //    return Ok(result);
        //}

        //[HttpGet("{id}")]
        //public async Task<ActionResult<IdentityUser>> GetUser(string id)
        //{
        //    var contract = await _contractService.GetAsync(id);

        //    return contract == null ? NotFound() : (ActionResult<IdentityUser>)contract;
        //}
    }
}
