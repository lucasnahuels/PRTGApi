using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.Models.Reports;

namespace WebApi.Services.Interfaces.Reports
{
    public interface IWeeklyPrinterService
    {
        Task<IEnumerable<WeeklyPrinter>> GetAsync();
        Task<WeeklyPrinter> GetAsync(int id);
        Task<WeeklyPrinter> CreateAsync(WeeklyPrinter contract);
    }
}
