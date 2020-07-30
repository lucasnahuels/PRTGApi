using Microsoft.EntityFrameworkCore;
using ApplicationCore.EntityFramework;
using ApplicationCore.Models;
using ApplicationCore.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class OwnerService : IOwnerService
    {
        private readonly PrtgDbContext _context;

        public OwnerService(PrtgDbContext context)
        {
            _context = context;
        }

        public async Task<Owner> CreateAsync(Owner owner)
        {
            await _context.Owners.AddAsync(owner);
            await _context.SaveChangesAsync();

            return owner;
        }

        public async Task<Owner> DeleteAsync(Owner owner)
        {
            _context.Owners.Remove(owner);
            await _context.SaveChangesAsync();

            return owner;
        }

        public async Task<bool> Exists(int id)
        {
            return await _context.Owners.AnyAsync(owner => owner.Id == id);
        }

        public async Task<IEnumerable<Owner>> GetAsync()
        {
            return await _context.Owners.ToListAsync();
        }

        public async Task<Owner> GetAsync(int id)
        {
            return await _context.Owners.FirstOrDefaultAsync(owner => owner.Id == id);
        }

        public async Task<Owner> UpdateAsync(Owner owner)
        {
            _context.Entry(owner).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return owner;
        }
    }
}
