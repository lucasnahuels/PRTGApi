using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ApplicationCore.Models.Reports;

namespace ApplicationCore.Services.Interfaces.Reports
{
    public interface IDailyDeviceService
    {
        Task<IEnumerable<DailyContadoresDataDevices>> GetAsync();
        Task<DailyContadoresDataDevices> GetAsync(int id);
        Task CreateDailyContadoresDeviceValues();
        Task<DailyContadoresDataDevices> GetCurrentContadoresDevicesValues(int objId);
        Task<DailyTonersDataDevices> GetCurrentTonersDevicesValues(int objId);
        Task<DailyContadoresDataDevices> GetContadoresDataFromSelectedRangeDate(int deviceId, DateTime date1, DateTime date2);
        Task<DailyContadoresDataDevices> GetContadoresDataFromActualMonth(int deviceId);
        Task<DailyTonersDataDevices> GetTonersDataFromSelectedRangeDate(int deviceId, DateTime date1, DateTime date2);
        Task<DailyTonersDataDevices> GetTonersDataFromActualMonth(int deviceId);
    }
}
