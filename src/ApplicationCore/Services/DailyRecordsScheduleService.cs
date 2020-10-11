using ApplicationCore.EntityFramework;
using ApplicationCore.Models;
using ApplicationCore.Models.Reports;
using ApplicationCore.Services.Interfaces;
using ApplicationCore.Services.Interfaces.Reports;
using Coravel.Invocable;
using Coravel.Scheduling.Schedule.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Mail;
using System.Threading;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class DailyRecordsScheduleService : IDailyRecordsTaskService, IInvocable
    {
        private readonly IDailyDeviceService _dailyDeviceService;
        private readonly ISensorService _sensorService;
        private readonly PrtgDbContext _context;

        public DailyRecordsScheduleService(IDailyDeviceService dailyDeviceService, ISensorService sensorService, PrtgDbContext prtgDbContext)
        {
            _dailyDeviceService = dailyDeviceService;
            _sensorService = sensorService;
            _context = prtgDbContext;
        }


        #region SensorDataNames
        public const string Contadores = "Contadores";
        private const string Toners = "Toners";
        #endregion

        public async Task Invoke()
        {
            //check if limits are exceeded
            await CreateDailyReport();
        }

        public async Task CreateDailyReport()
        {
            var devices = _sensorService.GetAllDevices().Result;
            DailyContadoresDataDevices newDailyContadores = null;
            DailyTonersDataDevices newDailyToners = null;

            foreach (var device in devices)
            {
                var childDevices = await _sensorService.GetChildDevices(device.ObjId);
                foreach (var childDevice in childDevices)
                {
                    var sensorDetails = await _sensorService.GetSensorDetails(childDevice.ObjId);
                    if (sensorDetails.SensorData.Name == Contadores)
                        newDailyContadores = _dailyDeviceService.GetCurrentContadoresDevicesValues(childDevice.ObjId, device.ObjId).Result;
                    if (sensorDetails.SensorData.Name == Toners)
                        newDailyToners = _dailyDeviceService.GetCurrentTonersDevicesValues(childDevice.ObjId, device.ObjId).Result;
                }

                if (newDailyContadores != null)
                {
                    _context.DailyContadores.Add(newDailyContadores);
                    await _context.SaveChangesAsync();
                }

                if (newDailyToners != null)
                {
                    _context.DailyToners.Add(newDailyToners);
                    await _context.SaveChangesAsync();
                }

            }
        }
    }
}
