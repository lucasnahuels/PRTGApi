using System.Collections.Generic;
using System.Threading.Tasks;
using ApplicationCore.Models.Reports;

namespace ApplicationCore.Services.Interfaces.Reports
{
    public interface IDailyDeviceService
    {
        Task<IEnumerable<DailyContadoresDataDevices>> GetAsync();
        Task<DailyContadoresDataDevices> GetAsync(int id);
        Task<DailyContadoresDataDevices> CreateDailyContadoresDeviceValues(string objId);
    }
}
