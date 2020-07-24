using System.Collections.Generic;
using System.Threading.Tasks;
using ApplicationCore.Models.Reports;

namespace ApplicationCore.Services.Interfaces.Reports
{
    public interface IDailyDeviceService
    {
        Task<IEnumerable<DailyDevices>> GetAsync();
        Task<DailyDevices> GetAsync(int id);
        Task<DailyDevices> CreateDailyContadoresDeviceValues(string objId);
    }
}
