using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ApplicationCore.Services.Interfaces;
using ApplicationCore.Models;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Auth0User>>> GetUsers()
        {
            try
            {
                return Ok(await _userService.GetAsync());
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
    }
}
