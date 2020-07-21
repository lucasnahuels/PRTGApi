using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.EntityFramework;
using WebApi.Models.Reports;
using WebApi.Services.Interfaces.Reports;

namespace WebApi.Services
{
    public class DailyPrinterService : IDailyPrinterService
    {
        private readonly PrtgDbContext _context;

        public DailyPrinterService(PrtgDbContext context)
        {
            _context = context;
        }
        public async Task<DailyPrinter> CreateAsync(DailyPrinter weeklyPrinter)
        {
            await _context.WeeklyPrinters.AddAsync(weeklyPrinter);
            await _context.SaveChangesAsync();

            return weeklyPrinter;
        }

        public async Task<IEnumerable<DailyPrinter>> GetAsync()
        {
            return await _context.WeeklyPrinters.ToListAsync();
        }

        public async Task<DailyPrinter> GetAsync(int id)
        {
            return await _context.WeeklyPrinters.FirstOrDefaultAsync(weeklyPrinter => weeklyPrinter.Id == id);
        }
    }
}
