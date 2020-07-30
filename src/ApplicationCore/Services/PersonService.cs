using Microsoft.EntityFrameworkCore;
using ApplicationCore.EntityFramework;
using ApplicationCore.Models;
using ApplicationCore.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class PersonService : IPersonService
    {
        private readonly PrtgDbContext _context;

        public PersonService(PrtgDbContext context)
        {
            _context = context;
        }

        public async Task<Person> CreateAsync(Person person)
        {
            await _context.Persons.AddAsync(person);
            await _context.SaveChangesAsync();

            return person;
        }

        public async Task<Person> DeleteAsync(Person person)
        {
            _context.Persons.Remove(person);
            await _context.SaveChangesAsync();

            return person;
        }

        public async Task<bool> Exists(int id)
        {
            return await _context.Persons.AnyAsync(person => person.Id == id);
        }

        public async Task<IEnumerable<Person>> GetAsync()
        {
            return await _context.Persons.ToListAsync();
        }

        public async Task<Person> GetAsync(int id)
        {
            return await _context.Persons.FirstOrDefaultAsync(person => person.Id == id);
        }

        public async Task<Person> UpdateAsync(Person person)
        {
            _context.Entry(person).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return person;
        }
    }
}
