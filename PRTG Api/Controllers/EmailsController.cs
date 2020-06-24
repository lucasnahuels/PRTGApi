using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PRTG_Api.Models;

namespace PRTG.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailsController : Controller
    {
        private readonly DataBaseContext _context;

        public EmailsController(DataBaseContext context)
        {
            _context = context;
        }
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Email>>> GetEmails()
        {
            return await _context.Email.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Email>> GetEmail(int id)
        {
            var email = await _context.Email.FirstOrDefaultAsync(m => m.EmailId == id);
            if (email == null)
            {
                return NotFound();
            }

            return email;
        }

        [HttpPost]
        public async Task<ActionResult<Email>> PostEmail(Email email)
        {
            if (ModelState.IsValid)
            {
                _context.Add(email);
                await _context.SaveChangesAsync();
            }
            return email;
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Email>> PutEmail(int id, Email email)
        {
            if (id != email.EmailId)
            {
                return BadRequest();
            }

            _context.Entry(email).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return email;
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Email>> Delete(int id)
        {
            var email = await _context.Email.FirstOrDefaultAsync(m => m.EmailId == id);
            if (email == null)
            {
                return NotFound();
            }

            _context.Email.Remove(email);
            await _context.SaveChangesAsync();

            return email;
        }
    }
}
