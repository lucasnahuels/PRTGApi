using Microsoft.EntityFrameworkCore;
using WebApi.Models;
using WebApi.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.EntityFramework;

namespace WebApi.Services
{
    public class EmailService : IEmailService
    {
        private readonly PrtgDbContext _context;

        public EmailService(PrtgDbContext context)
        {
            _context = context;
        }

        public async Task<Email> CreateAsync(Email email)
        {
            await _context.Emails.AddAsync(email);
            await _context.SaveChangesAsync();

            return email;
        }

        public async Task<Email> DeleteAsync(Email email)
        {
            _context.Emails.Remove(email);
            await _context.SaveChangesAsync();

            return email;
        }

        public async Task<IEnumerable<Email>> GetAsync()
        {
            return await _context.Emails.ToListAsync();
        }

        public async Task<Email> GetAsync(int id)
        {
            return await _context.Emails.FirstOrDefaultAsync(email => email.EmailId == id);
        }

        public async Task<Email> UpdateAsync(Email email)
        {
            _context.Entry(email).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return email;
        }
    }
}
