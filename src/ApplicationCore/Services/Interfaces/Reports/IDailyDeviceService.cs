using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ApplicationCore.Models;
using ApplicationCore.Models.Reports;

namespace ApplicationCore.Services.Interfaces.Reports
{
    public interface IDailyDeviceService
    {
        Task<IEnumerable<DailyContadoresDataDevices>> GetDailyContadoresAsync();
        Task<DailyContadoresDataDevices> GetDailyContadoresByDeviceIdAsync(int id);
        Task<IEnumerable<DailyTonersDataDevices>> GetDailyTonersAsync();
        Task<DailyTonersDataDevices> GetDailyTonersByDeviceIdAsync(int id);
        Task<DailyContadoresDataDevices> GetCurrentContadoresDevicesValues(int objId, int parentObjId);
        Task<DailyTonersDataDevices> GetCurrentTonersDevicesValues(int objId, int parentObjId);
        Task<DailyContadoresDataDevices> GetContadoresDataFromSelectedRangeDate(int deviceId, DateTime date1, DateTime date2);
        Task<DailyContadoresDataDevices> GetContadoresDataFromActualOrPreviousMonth(int deviceId, bool actualMonth);
        Task<TonersUsed> GetTonersDataFromSelectedRangeDate(int deviceId, DateTime date1, DateTime date2);
        Task<TonersUsed> GetTonersDataFromActualOrPreviousMonth(int deviceId, bool actualMonth);
        Task<ContractPrices> CalculatePrices(int contractId, int deviceId);
    }
}
