using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using WebApi.Models;
using WebApi.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Amazon.DynamoDb.Interfaces;

namespace WebApi.Services
{
    public class EmailService : IEmailService
    {
        private readonly IDynamoDbContext<Email> _context;

        public EmailService(IDynamoDbContext<Email> context)
        {
            _context = context;
        }

        public async Task<Email> CreateAsync(Email email)
        {
            await _context.SaveAsync(email);

            return email;
        }

        public async Task<Email> DeleteAsync(Email email)
        {
            await _context.DeleteAsync(email);

            return email;
        }

        public async Task<IEnumerable<Email>> GetAsync()
        {
            return await _context.Get();
        }

        public async Task<Email> GetAsync(int id)
        {
            var emails = await _context.Get(new List<ScanCondition>() {
                new ScanCondition("Id", ScanOperator.Equal, id)
            });

            return emails.FirstOrDefault(email => email.EmailId == id);
        }

        public async Task<Email> UpdateAsync(Email email)
        {
            return await UpdateAsync(email);
        }
    }
}
