using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PRTG_Api.Models;
using PRTG_Api.Services.Interfaces;

namespace PRTG.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly IEmailService _emailService;

        public EmailController(IEmailService emailService)
        {
            _emailService = emailService;
        }
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Email>>> GetEmails()
        {
            var result = await _emailService.GetAsync();
            
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Email>> GetEmail(int id)
        {
            var email = await _emailService.GetAsync(id);

            return email == null ? NotFound() : (ActionResult<Email>)Ok(email);
        }

        [HttpPost]
        public async Task<ActionResult<Email>> PostEmail(Email email)
        {
            return !ModelState.IsValid ? BadRequest() : (ActionResult<Email>)await _emailService.CreateAsync(email);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Email>> PutEmail(int id, Email email)
        {
            return id != email.EmailId ? BadRequest() : (ActionResult<Email>)await _emailService.UpdateAsync(email);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Email>> Delete(int id)
        {
            var email = await _emailService.GetAsync(id);
            
            return email == null ? NotFound() : (ActionResult<Email>)await _emailService.DeleteAsync(email);
        }
    }
}
