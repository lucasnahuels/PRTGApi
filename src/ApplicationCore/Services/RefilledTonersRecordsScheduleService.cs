using ApplicationCore.EntityFramework;
using ApplicationCore.Models.Reports;
using ApplicationCore.Services.Interfaces;
using ApplicationCore.Services.Interfaces.Reports;
using Coravel.Invocable;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class RefilledTonersRecordsScheduleService : IRefilledTonersRecordsScheduleService, IInvocable
    {
        private readonly IDailyDeviceService _dailyDeviceService;
        private readonly ISensorService _sensorService;
        private readonly PrtgDbContext _context;

        public RefilledTonersRecordsScheduleService(IDailyDeviceService dailyDeviceService, ISensorService sensorService, PrtgDbContext prtgDbContext)
        {
            _dailyDeviceService = dailyDeviceService;
            _sensorService = sensorService;
            _context = prtgDbContext;
        }

        #region SensorDataNames
        private const string Toners = "Toners";
        #endregion

        public async Task Invoke()
        {
            await CreateRefilledTonersRecord();
        }
        public async Task CreateRefilledTonersRecord()
        {
            var devices = _sensorService.GetAllDevices().Result;
            foreach (var device in devices)
            {
                DailyTonersDataDevices lastTonerValues = null;
                
                var childDevices = await _sensorService.GetChildDevices(device.ObjId);
                foreach (var childDevice in childDevices)
                {
                    var sensorDetails = await _sensorService.GetSensorDetails(childDevice.ObjId);
                    if (sensorDetails.SensorData.Name == Toners)
                    {
                        lastTonerValues = _dailyDeviceService.GetLastRecordOfDailyTonersByDeviceIdAsync(device.ObjId).Result; //check if it gets last value
                        
                        #region CreateCurrentTonerValuesRecord 
                        var currentTonerValues = _dailyDeviceService.GetCurrentTonersDevicesValues(childDevice.ObjId, device.ObjId).Result;
                        _context.DailyToners.Add(currentTonerValues);
                        await _context.SaveChangesAsync();
                        #endregion

                        if (lastTonerValues != null && currentTonerValues != null)
                        { 
                            bool blackRefilled = currentTonerValues.BlackTonersUsed > lastTonerValues.BlackTonersUsed && currentTonerValues.BlackTonersUsed > 85 ? true : false;
                            bool cyanRefilled = currentTonerValues.CyanTonersUsed > lastTonerValues.CyanTonersUsed && currentTonerValues.CyanTonersUsed > 85 ? true : false;
                            bool yellowRefilled = currentTonerValues.YellowTonersUsed > lastTonerValues.YellowTonersUsed && currentTonerValues.YellowTonersUsed > 85 ? true : false;
                            bool magentaRefilled = currentTonerValues.MagentaTonersUsed > lastTonerValues.MagentaTonersUsed && currentTonerValues.MagentaTonersUsed > 85 ? true : false;

                            var tonersUsed = new TonersUsed
                            {
                                DeviceId = device.ObjId,
                                DateAndTime = DateTime.Now,
                                BlackTonersUsed = blackRefilled ? 1 : 0,
                                CyanTonersUsed = cyanRefilled ? 1 : 0,
                                YellowTonersUsed = yellowRefilled ? 1 : 0,
                                MagentaTonersUsed = magentaRefilled ? 1 : 0
                            };
                            _context.TonersUsed.Add(tonersUsed);
                            await _context.SaveChangesAsync();
                        }
                    }
                }
            }
        }
    }
}
