using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.Models.Reports;

namespace WebApi.Services.Interfaces.Reports
{
    public interface IDailyPrinterService
    {
        Task<IEnumerable<DailyPrinter>> GetAsync();
        Task<DailyPrinter> GetAsync(int id);
        Task<DailyPrinter> CreateAsync(DailyPrinter contract);
    }
}
