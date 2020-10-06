using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using ApplicationCore.EntityFramework;
using ApplicationCore.Models.Reports;
using ApplicationCore.Services.Interfaces.Reports;
using ApplicationCore.Services.Interfaces;
using System;
using System.Linq;

namespace ApplicationCore.Services
{
    public class DailyDeviceService : IDailyDeviceService
    {
        private readonly PrtgDbContext _context;
        private readonly ISensorService _sensorService;

        public DailyDeviceService(PrtgDbContext context, ISensorService sensorService)
        {
            _context = context;
            _sensorService = sensorService;
        }


        #region ApiData
        private const string username = "prtgadmin";
        private const string password = "Si5t3m4s";
        private const string apiTable = "api/table.json";
        private const string apiSensorDetails = "api/getsensordetails.json";
        #endregion

        #region ChannelNames
        private const string CopiasBlackAndWhite = "Copias Black & White";
        private const string PrintBlackAndWhite = "Print Black & White";

        private const string CopiasFullColor = "Copias Full Color";
        private const string CopiasSingleColor = "Copias Single Color";
        private const string CopiasTwoColor = "Copias Two-color";
        private const string PrintFullColor = "Print Full Color";
        private const string PrintSingleColor = "Print Single Color";
        private const string PrintTwoColor = "Print Two-color";

        private const string Duplex = "Duplex";
        #endregion

        #region SensorDataNames
        public const string Contadores = "Contadores";
        private const string Toners = "Toners";
        #endregion


        public async Task<IEnumerable<DailyContadoresDataDevices>> GetAsync()
        {
            return await _context.DailyContadores.ToListAsync();
        }

        public async Task<DailyContadoresDataDevices> GetAsync(int id)
        {
            return await _context.DailyContadores.FirstOrDefaultAsync(dailyPrinter => dailyPrinter.Id == id);
        }

        public async Task CreateDailyContadoresDeviceValues()
        {
            var devices = await _sensorService.GetAllDevices(); //can´t be getAllSensors??
            var dailyDevices = new List<DailyContadoresDataDevices>();

            foreach(var device in devices)
            {
                var childDevices = await _sensorService.GetChildDevices(device.ObjId);
                foreach(var childDevice in childDevices)
                {
                    var sensorDetails = await _sensorService.GetSensorDetails(childDevice.ObjId);
                    if (sensorDetails.SensorData.Name == "Contadores")
                    { 
                        var dailyContadoresDataDevice = await GetCurrentContadoresDevicesValues(childDevice.ObjId);
                        if (dailyContadoresDataDevice != null)
                        {
                            //Replace contadores ObjId with real device ObjId
                            dailyContadoresDataDevice.Id = device.ObjId;
                            dailyDevices.Add(dailyContadoresDataDevice);
                        }
                    }
                }
            }
            await _context.DailyContadores.AddRangeAsync(dailyDevices);
            await _context.SaveChangesAsync();
        }

        public async Task<DailyTonersDataDevices> CreateDailyTonersDeviceValues(int objId)
        {
            var dailyPrinter = await GetCurrentTonersDevicesValues(objId);
            //crear tabla para daily toners devices
            //await _context.DailyPrinters.AddAsync(dailyPrinter);
            await _context.SaveChangesAsync();

            return dailyPrinter;
        }

        public async Task<DailyTonersDataDevices> CreateFifteenMinutesTonersDeviceValues(int objId)
        {
            var dailyPrinter = await GetCurrentTonersDevicesValues(objId);
            //crear tabla para fifteen minutes toners devices
            //await _context.DailyPrinters.AddAsync(dailyPrinter);
            await _context.SaveChangesAsync();

            return dailyPrinter;
        }

        public async Task<DailyContadoresDataDevices> GetCurrentContadoresDevicesValues(int objId)
        {
            var contadores = await _sensorService.GetContadoresData(objId);
            //falta restarle el valor de ayer a los siguientes valores

            int blackAndWhiteCopies = 0;

            if (!int.TryParse(contadores.Channels.FirstOrDefault(c => c.Name == CopiasFullColor)?.LastValue, out int colorCopies))
            {
                return null;
            }
            colorCopies += int.Parse(contadores.Channels.FirstOrDefault(c => c.Name == PrintFullColor).LastValue);
            colorCopies += int.Parse(contadores.Channels.FirstOrDefault(c => c.Name == PrintSingleColor).LastValue);
            colorCopies += int.Parse(contadores.Channels.FirstOrDefault(c => c.Name == PrintTwoColor).LastValue);
            colorCopies += int.Parse(contadores.Channels.FirstOrDefault(c => c.Name == CopiasSingleColor).LastValue);
            colorCopies += int.Parse(contadores.Channels.FirstOrDefault(c => c.Name == CopiasTwoColor).LastValue);
            blackAndWhiteCopies += int.Parse(contadores.Channels.FirstOrDefault(c => c.Name == CopiasBlackAndWhite).LastValue);
            blackAndWhiteCopies += int.Parse(contadores.Channels.FirstOrDefault(c => c.Name == PrintBlackAndWhite).LastValue);
            //blackAndWhiteCopies += int.Parse(contadores.Channels.FirstOrDefault(c => c.Name == Duplex).LastValue);

            return new DailyContadoresDataDevices
            {
                ColorCopies = colorCopies,
                BlackAndWhiteCopies = blackAndWhiteCopies,
                DeviceId = objId,
                DateToday = DateTime.Now
            };
        }
        public async Task<DailyTonersDataDevices> GetCurrentTonersDevicesValues(int objId)
        {
            var tonersUsed = await GetQuantityTonersToday(objId);
            return new DailyTonersDataDevices
            {
                BlackTonersUsed = tonersUsed.BlackTonersUsed,
                CyanTonersUsed = tonersUsed.CyanTonersUsed,
                MagentaTonersUsed = tonersUsed.MagentaTonersUsed,
                YellowTonersUsed = tonersUsed.YellowTonersUsed,
                DeviceId = objId,
                DateToday = DateTime.Now
            };
        }

        public Task<DailyTonersDataDevices> GetQuantityTonersToday(int objId)
        {
            //get from database the values from toners used today
            var tonersUsedToday = new DailyTonersDataDevices();
            return Task.FromResult(tonersUsedToday);
        }

        public Task<DailyContadoresDataDevices> GetContadoresDataFromSelectedRangeDate(int deviceId, DateTime date1, DateTime date2)
        {
            var deviceDate1ContadoresValues = _context.DailyContadores.FirstOrDefaultAsync(dailyPrinter => dailyPrinter.DateToday == date1 &&
                                                                        dailyPrinter.DeviceId == deviceId).Result;

            var deviceDate2ContadoresValues = _context.DailyContadores.FirstOrDefaultAsync(dailyPrinter => dailyPrinter.DateToday == date2 &&
                                                                        dailyPrinter.DeviceId == deviceId).Result;

            var contadoresValuesToReturn = new DailyContadoresDataDevices
            {
                BlackAndWhiteCopies = deviceDate2ContadoresValues.BlackAndWhiteCopies - deviceDate1ContadoresValues.BlackAndWhiteCopies,
                ColorCopies = deviceDate2ContadoresValues.ColorCopies - deviceDate1ContadoresValues.ColorCopies
            };
            return Task.FromResult(contadoresValuesToReturn);
        }

        public Task<DailyContadoresDataDevices> GetContadoresDataFromActualMonth(int deviceId)
        {
            bool afterSix = DateTime.Now.Hour < 18 ? false : true;
            var dateToday = afterSix ? DateTime.Now : DateTime.Now.AddDays(-1);
            var firstDayOfMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);

            var deviceLastContadoresValues = _context.DailyContadores.FirstOrDefaultAsync(dailyPrinter => dailyPrinter.DateToday == dateToday &&
                                                                        dailyPrinter.DeviceId == deviceId).Result;

            var deviceFirstOfMonthContadoresValues = _context.DailyContadores.FirstOrDefaultAsync(dailyPrinter => dailyPrinter.DateToday == firstDayOfMonth &&
                                                                        dailyPrinter.DeviceId == deviceId).Result;

            var contadoresValuesToReturn = new DailyContadoresDataDevices
            {
                BlackAndWhiteCopies = deviceLastContadoresValues.BlackAndWhiteCopies - deviceFirstOfMonthContadoresValues.BlackAndWhiteCopies,
                ColorCopies = deviceLastContadoresValues.ColorCopies - deviceFirstOfMonthContadoresValues.ColorCopies
            };
            return Task.FromResult(contadoresValuesToReturn);
        }


        public Task<DailyTonersDataDevices> GetTonersDataFromSelectedRangeDate(int deviceId, DateTime date1, DateTime date2)
        {
            var deviceDate1TonersValues = _context.DailyToners.FirstOrDefaultAsync(dailyPrinter => dailyPrinter.DateToday == date1 &&
                                                                        dailyPrinter.DeviceId == deviceId).Result;

            var deviceDate2TonersValues = _context.DailyToners.FirstOrDefaultAsync(dailyPrinter => dailyPrinter.DateToday == date2 &&
                                                                        dailyPrinter.DeviceId == deviceId).Result;

            var tonersValuesToReturn = new DailyTonersDataDevices
            {
             
            };
            return Task.FromResult(tonersValuesToReturn);
        }

        public Task<DailyTonersDataDevices> GetTonersDataFromActualMonth(int deviceId)
        {
            bool afterSix = DateTime.Now.Hour < 18 ? false : true;
            var dateToday = afterSix ? DateTime.Now : DateTime.Now.AddDays(-1);
            var firstDayOfMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);

            var deviceLastTonersValues = _context.DailyToners.FirstOrDefaultAsync(dailyPrinter => dailyPrinter.DateToday == dateToday &&
                                                                        dailyPrinter.DeviceId == deviceId).Result;

            var deviceFirstOfMonthTonersValues = _context.DailyToners.FirstOrDefaultAsync(dailyPrinter => dailyPrinter.DateToday == firstDayOfMonth &&
                                                                        dailyPrinter.DeviceId == deviceId).Result;

            var tonersValuesToReturn = new DailyTonersDataDevices
            {
                BlackTonersUsed = deviceLastTonersValues.BlackTonersUsed - deviceFirstOfMonthTonersValues.BlackTonersUsed,
                CyanTonersUsed = deviceLastTonersValues.CyanTonersUsed - deviceFirstOfMonthTonersValues.BlackTonersUsed,
                MagentaTonersUsed = deviceLastTonersValues.MagentaTonersUsed - deviceFirstOfMonthTonersValues.MagentaTonersUsed,
                YellowTonersUsed = deviceLastTonersValues.YellowTonersUsed - deviceFirstOfMonthTonersValues.YellowTonersUsed
            };
            return Task.FromResult(tonersValuesToReturn);
        }
    }
}
