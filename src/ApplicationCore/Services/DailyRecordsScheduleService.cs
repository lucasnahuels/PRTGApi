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
        private readonly IContractService _contractService;
        private readonly IMailingMonthReportService _mailingMonthReportService;
        private readonly IMailerService _mailerService;

        public DailyRecordsScheduleService(
            IDailyDeviceService dailyDeviceService, 
            ISensorService sensorService,
            PrtgDbContext prtgDbContext, 
            IContractService contractService,
            IMailingMonthReportService mailingMonthReportService,
            IMailerService mailerService
            )
        {
            _dailyDeviceService = dailyDeviceService;
            _sensorService = sensorService;
            _context = prtgDbContext;
            _contractService = contractService;
            _mailingMonthReportService = mailingMonthReportService;
            _mailerService = mailerService;
        }


        #region SensorDataNames
        public const string Contadores = "Contadores";
        private const string Toners = "Toners";
        #endregion

        public async Task Invoke()
        {
            await ManageReportsIfExceeded();
            await CreateDailyReport();
        }

        public async Task ManageReportsIfExceeded()
        {
            var contracts = _contractService.GetAsync().Result;
            foreach(var contract in contracts)
            {
                DailyContadoresDataDevices actualMonthData;
                foreach (var contractDevice in contract.ContractDevices)
                {
                    actualMonthData = _dailyDeviceService.GetContadoresDataFromActualOrPreviousMonth(int.Parse(contractDevice.ObjId), true).Result;
                    
                    int bAWCopiesExceeded = actualMonthData.BlackAndWhiteCopies > contract.BlackAndWhiteLimitSet ? actualMonthData.BlackAndWhiteCopies - contract.BlackAndWhiteLimitSet : 0;
                    int colorCopiesExceeded = actualMonthData.ColorCopies > contract.ColorLimitSet ? actualMonthData.ColorCopies - contract.ColorLimitSet : 0;

                    if (bAWCopiesExceeded > 0)
                    {
                        var newMailingMonthReport = new MailingMonthReport
                        {
                            ContractId = contractDevice.ContractId,
                            DeviceId = contractDevice.ObjId,
                            Year = DateTime.Now.Year,
                            Month = DateTime.Now.Month,
                            IsColor = false
                        };
                        var mailingMonthReport = await _mailingMonthReportService.GetByReportAsync(newMailingMonthReport);
                        if(mailingMonthReport == null)
                        {
                            await _mailingMonthReportService.CreateAsync(newMailingMonthReport);
                            foreach (var contractEmployee in contract.ContractEmployees)
                            {
                                await _mailerService.SendEmailAsync(contractEmployee.Employee.Email,
                                    $"Exceeded from {DateTime.Now.Year} and {DateTime.Now.Month}",
                                    $"The device {contractDevice.ObjId} exceeded its limits for black and white sheet copies as respect the contracr establised");
                            }
                        }
                    }
                    if (colorCopiesExceeded > 0)
                    {
                        var newMailingMonthReport = new MailingMonthReport
                        {
                            ContractId = contractDevice.ContractId,
                            DeviceId = contractDevice.ObjId,
                            Year = DateTime.Now.Year,
                            Month = DateTime.Now.Month,
                            IsColor = true
                        };
                        var mailingMonthReport = await _mailingMonthReportService.GetByReportAsync(newMailingMonthReport);
                        if (mailingMonthReport == null)
                        {
                            await _mailingMonthReportService.CreateAsync(newMailingMonthReport);
                            foreach (var contractEmployee in contract.ContractEmployees)
                            {
                                await _mailerService.SendEmailAsync(contractEmployee.Employee.Email,
                                    $"Exceeded from {DateTime.Now.Year} and {DateTime.Now.Month}",
                                    $"The device {contractDevice.ObjId} exceeded its limits for color sheet copies as respect the contracr establised");
                            }
                        }
                    }
                }
            }
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
