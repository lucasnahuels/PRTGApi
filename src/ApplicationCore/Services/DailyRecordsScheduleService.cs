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

        public Task Invoke()
        {
            CreateDailyReport();
            return Task.CompletedTask;
        }

        public void CreateDailyReport()
        {
            var devices = _sensorService.GetAllDevices().Result; 
            foreach (var device in devices)
            {
               var newDailyContadores = _dailyDeviceService.GetCurrentContadoresDevicesValues(device.ObjId).Result;
                _context.DailyContadores.Add(newDailyContadores);

                var newDailyToners = _dailyDeviceService.GetCurrentTonersDevicesValues(device.ObjId).Result;
                _context.DailyToners.Add(newDailyToners);
            }
        }
    }
}
