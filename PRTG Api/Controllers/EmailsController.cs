using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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

        // GET: api/Emails
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Email>>> GetEmails()
        {
            return await _context.Email.ToListAsync();
        }

        // GET: api/Emails/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Email>> GetEmail(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var email = await _context.Email
                .FirstOrDefaultAsync(m => m.EmailId == id);
            if (email == null)
            {
                return NotFound();
            }

            return email;
        }

        // POST: api/Emails
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

        // POST: api/Emails/5
        [HttpPut("{id}")]
        public async Task<ActionResult<Email>> PutEmail(int id, Email email)
        {
            if (id != email.EmailId)
            {
                return BadRequest();
            }

            //if (_context.Entry(email).State == EntityState.Detached)
            //{
                //_context.Email.Attach(email);

                _context.Entry(email).State = EntityState.Modified;
            //}

            await _context.SaveChangesAsync();

            return email;
        }


        // DELETE: api/Emails/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Email>> Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var email = await _context.Email.FirstOrDefaultAsync(m => m.EmailId == id);
            if (email == null)
            {
                return NotFound();
            }

            _context.Email.Remove(email);
            await _context.SaveChangesAsync();

            return email;
        }

        private bool EmailExists(int id)
        {
            return _context.Email.Any(e => e.EmailId == id);
        }
    }
}
