using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.EntityFramework;
using WebApi.Models.Reports;
using WebApi.Services.Interfaces.Reports;

namespace WebApi.Services
{
    public class WeeklyPrinterService : IWeeklyPrinterService
    {
        private readonly PrtgDbContext _context;

        public WeeklyPrinterService(PrtgDbContext context)
        {
            _context = context;
        }
        public async Task<WeeklyPrinter> CreateAsync(WeeklyPrinter weeklyPrinter)
        {
            await _context.WeeklyPrinters.AddAsync(weeklyPrinter);
            await _context.SaveChangesAsync();

            return weeklyPrinter;
        }

        public async Task<IEnumerable<WeeklyPrinter>> GetAsync()
        {
            return await _context.WeeklyPrinters.ToListAsync();
        }

        public async Task<WeeklyPrinter> GetAsync(int id)
        {
            return await _context.WeeklyPrinters.FirstOrDefaultAsync(weeklyPrinter => weeklyPrinter.Id == id);
        }
    }
}
