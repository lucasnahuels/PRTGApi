using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using ApplicationCore.EntityFramework;
using ApplicationCore.Models.Reports;
using ApplicationCore.Services.Interfaces.Reports;
using ApplicationCore.Services.Interfaces;
using System;
using System.Linq;
using ApplicationCore.Models.Constants;

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
        private const string Black = "Black";
        private const string Cyan = "Cyan";
        private const string Magenta = "Magenta";
        private const string Yellow = "Yellow";

        private const string Duplex = "Duplex";
        #endregion

        #region SensorDataNames
        public const string Contadores = "Contadores";
        private const string Toners = "Toners";
        #endregion


        public async Task<IEnumerable<DailyContadoresDataDevices>> GetDailyContadoresAsync()
        {
            return await _context.DailyContadores.ToListAsync();
        }

        public async Task<DailyContadoresDataDevices> GetDailyContadoresByDeviceIdAsync(int id)
        {
            return await _context.DailyContadores.FirstOrDefaultAsync(dailyDevice => dailyDevice.DeviceId == id);
        }

        public async Task<IEnumerable<DailyTonersDataDevices>> GetDailyTonersAsync()
        {
            return await _context.DailyToners.ToListAsync();
        }

        public async Task<DailyTonersDataDevices> GetDailyTonersByDeviceIdAsync(int id)
        {
            return await _context.DailyToners.FirstOrDefaultAsync(dailyDevice => dailyDevice.DeviceId == id);
        }

        public async Task<DailyContadoresDataDevices> GetCurrentContadoresDevicesValues(int objId, int parentObjId)
        {
            var contadores = await _sensorService.GetContadoresData(objId);
            if (contadores.Channels.FirstOrDefault(c => c.Name == CopiasFullColor).LastValue == "No hay datos"
                || contadores.Channels.FirstOrDefault(c => c.Name == PrintFullColor).LastValue == "No hay datos"
                || contadores.Channels.FirstOrDefault(c => c.Name == PrintSingleColor).LastValue == "No hay datos"
                || contadores.Channels.FirstOrDefault(c => c.Name == PrintTwoColor).LastValue == "No hay datos"
                || contadores.Channels.FirstOrDefault(c => c.Name == CopiasSingleColor).LastValue == "No hay datos"
                || contadores.Channels.FirstOrDefault(c => c.Name == CopiasTwoColor).LastValue == "No hay datos"
                || contadores.Channels.FirstOrDefault(c => c.Name == CopiasBlackAndWhite).LastValue == "No hay datos"
                || contadores.Channels.FirstOrDefault(c => c.Name == PrintBlackAndWhite).LastValue == "No hay datos"
                )
                return null;

            int blackAndWhiteCopies = 0;

            if (!int.TryParse(contadores.Channels.FirstOrDefault(c => c.Name == CopiasFullColor)?.LastValue, out int colorCopies))
            {
                return null;
            }
            colorCopies += int.Parse(contadores.Channels.FirstOrDefault(c => c.Name == PrintFullColor).LastValue.Replace(".", string.Empty));
            colorCopies += int.Parse(contadores.Channels.FirstOrDefault(c => c.Name == PrintSingleColor).LastValue.Replace(".", string.Empty));
            colorCopies += int.Parse(contadores.Channels.FirstOrDefault(c => c.Name == PrintTwoColor).LastValue.Replace(".", string.Empty));
            colorCopies += int.Parse(contadores.Channels.FirstOrDefault(c => c.Name == CopiasSingleColor).LastValue.Replace(".", string.Empty));
            colorCopies += int.Parse(contadores.Channels.FirstOrDefault(c => c.Name == CopiasTwoColor).LastValue.Replace(".", string.Empty));
            blackAndWhiteCopies += int.Parse(contadores.Channels.FirstOrDefault(c => c.Name == CopiasBlackAndWhite).LastValue.Replace(".", string.Empty));
            blackAndWhiteCopies += int.Parse(contadores.Channels.FirstOrDefault(c => c.Name == PrintBlackAndWhite).LastValue.Replace(".", string.Empty));
            //blackAndWhiteCopies += int.Parse(contadores.Channels.FirstOrDefault(c => c.Name == Duplex).LastValue);

            return new DailyContadoresDataDevices
            {
                ColorCopies = colorCopies,
                BlackAndWhiteCopies = blackAndWhiteCopies,
                DeviceId = parentObjId,
                DateToday = DateTime.Now
            };
        }

        public async Task<DailyTonersDataDevices> GetCurrentTonersDevicesValues(int objId, int parentObjId)
        {
            var toners = await _sensorService.GetTonersData(objId);
            
            if (toners.Channels.Count == 0) return null;
            if (toners.Channels.FirstOrDefault(c => c.Name == Black).LastValue == "No hay datos"
                || toners.Channels.FirstOrDefault(c => c.Name == Cyan).LastValue == "No hay datos"
                || toners.Channels.FirstOrDefault(c => c.Name == Magenta).LastValue == "No hay datos"
                || toners.Channels.FirstOrDefault(c => c.Name == Yellow).LastValue == "No hay datos"
                )
                return null;
            return new DailyTonersDataDevices
            {
                BlackTonersUsed = int.Parse(toners.Channels.FirstOrDefault(c => c.Name == Black).LastValue.Substring(0, 3)),
                CyanTonersUsed = int.Parse(toners.Channels.FirstOrDefault(c => c.Name == Cyan).LastValue.Substring(0, 3)),
                MagentaTonersUsed = int.Parse(toners.Channels.FirstOrDefault(c => c.Name == Magenta).LastValue.Substring(0, 3)),
                YellowTonersUsed = int.Parse(toners.Channels.FirstOrDefault(c => c.Name == Yellow).LastValue.Substring(0, 3)),
                DeviceId = parentObjId,
                DateToday = DateTime.Now
            };
        }

        public Task<DailyContadoresDataDevices> GetContadoresDataFromSelectedRangeDate(int deviceId, DateTime date1, DateTime date2)
        {
            var deviceDate1ContadoresValues = _context.DailyContadores.FirstOrDefaultAsync(dailyPrinter => dailyPrinter.DateToday.Date == date1.Date &&
                                                                        dailyPrinter.DeviceId == deviceId).Result;

            var deviceDate2ContadoresValues = _context.DailyContadores.FirstOrDefaultAsync(dailyPrinter => dailyPrinter.DateToday.Date == date2.Date &&
                                                                        dailyPrinter.DeviceId == deviceId).Result;

            #region Initialize
            if (deviceDate1ContadoresValues == null)
            {
                deviceDate1ContadoresValues = new DailyContadoresDataDevices
                {
                    DeviceId = deviceId,
                    BlackAndWhiteCopies = 0,
                    ColorCopies = 0,
                    DateToday = new DateTime(2020, 10, 1)
                };
            }
            if (deviceDate2ContadoresValues == null)
            {
                deviceDate2ContadoresValues = new DailyContadoresDataDevices
                {
                    DeviceId = deviceId,
                    BlackAndWhiteCopies = 0,
                    ColorCopies = 0,
                    DateToday = new DateTime(2020, 10, 1)
                };
            }
            #endregion

            var contadoresValuesToReturn = new DailyContadoresDataDevices
            {
                BlackAndWhiteCopies = deviceDate2ContadoresValues.BlackAndWhiteCopies - deviceDate1ContadoresValues.BlackAndWhiteCopies,
                ColorCopies = deviceDate2ContadoresValues.ColorCopies - deviceDate1ContadoresValues.ColorCopies
            };
            return Task.FromResult(contadoresValuesToReturn);
        }

        public Task<DailyContadoresDataDevices> GetContadoresDataFromActualOrPreviousMonth(int deviceId, bool actualMonth)
        {
            bool afterSix = DateTime.Now.Hour < Constants.TimeRecordsAreTriggered ? false : true;
            var dateToday = afterSix ? DateTime.Now : DateTime.Now.AddDays(-1);
            var firstDayOfMonth = actualMonth? new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1) : new DateTime(DateTime.Now.Year, DateTime.Now.Month -1, 1);

            var deviceLastContadoresValues = _context.DailyContadores.FirstOrDefaultAsync(dailyPrinter => dailyPrinter.DateToday.Date == dateToday.Date &&
                                                                        dailyPrinter.DeviceId == deviceId).Result;

            var deviceFirstOfMonthContadoresValues = _context.DailyContadores.FirstOrDefaultAsync(dailyPrinter => dailyPrinter.DateToday.Date == firstDayOfMonth.Date &&
                                                                        dailyPrinter.DeviceId == deviceId).Result;

            #region Initialize
            if(DateTime.Now.Year == 2020 && DateTime.Now.Month == 10)
            deviceFirstOfMonthContadoresValues = new DailyContadoresDataDevices
            {
                DeviceId = deviceId,
                BlackAndWhiteCopies = 0,
                ColorCopies= 0,
                DateToday = new DateTime(2020, 10, 1)
            };
            if (deviceFirstOfMonthContadoresValues == null)
            {
                deviceFirstOfMonthContadoresValues = new DailyContadoresDataDevices
                {
                    DeviceId = deviceId,
                    BlackAndWhiteCopies = 0,
                    ColorCopies = 0,
                    DateToday = new DateTime(2020, 10, 1)
                };
            }
            if (deviceLastContadoresValues == null)
            {
                deviceLastContadoresValues = new DailyContadoresDataDevices
                {
                    DeviceId = deviceId,
                    BlackAndWhiteCopies = 0,
                    ColorCopies = 0,
                    DateToday = new DateTime(2020, 10, 1)
                };
            }
            #endregion

            var contadoresValuesToReturn = new DailyContadoresDataDevices
            {
                BlackAndWhiteCopies = deviceLastContadoresValues.BlackAndWhiteCopies - deviceFirstOfMonthContadoresValues.BlackAndWhiteCopies,
                ColorCopies = deviceLastContadoresValues.ColorCopies - deviceFirstOfMonthContadoresValues.ColorCopies,
                DeviceId = deviceId
            };

            #region NoDataInTheMonthYet
            if (!afterSix && DateTime.Now.Date == new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1) && actualMonth)
            {
                contadoresValuesToReturn = new DailyContadoresDataDevices
                {
                    BlackAndWhiteCopies = 0,
                    ColorCopies = 0,
                    DeviceId = deviceId
                };
            }
            #endregion

            return Task.FromResult(contadoresValuesToReturn);
        }


        public Task<TonersUsed> GetTonersDataFromSelectedRangeDate(int deviceId, DateTime date1, DateTime date2)
        {
            var deviceDateTonersValues = _context.TonersUsed.Where(x => x.DateAndTime.Date >= date1.Date && x.DateAndTime.Date <= date2.Date &&
                                                                        x.DeviceId == deviceId).ToList();
      
            #region Initialize
            if (deviceDateTonersValues == null)
            {
                deviceDateTonersValues = new List<TonersUsed>();
            }
            #endregion

            int black = 0; int cyan = 0; int yellow = 0; int magenta = 0;
            foreach(var device in deviceDateTonersValues)
            {
                black = device.BlackTonersUsed == 1 ? +1 : +0;
                cyan = device.CyanTonersUsed == 1 ? +1 : +0;
                yellow = device.YellowTonersUsed == 1 ? +1 : +0;
                magenta = device.MagentaTonersUsed == 1 ? +1 : +0;
            }

            var tonersValuesToReturn = new TonersUsed
            {
                BlackTonersUsed =black,
                CyanTonersUsed = cyan,
                MagentaTonersUsed = magenta,
                YellowTonersUsed = yellow,
                DeviceId = deviceId
            };
            return Task.FromResult(tonersValuesToReturn);
        }

        public Task<TonersUsed> GetTonersDataFromActualOrPreviousMonth(int deviceId, bool actualMonth)
        {
            bool afterSix = DateTime.Now.Hour < Constants.TimeRecordsAreTriggered ? false : true;
            var dateToday = afterSix ? DateTime.Now : DateTime.Now.AddDays(-1);
            var firstDayOfMonth = actualMonth ? new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1) : new DateTime(DateTime.Now.Year, DateTime.Now.Month - 1, 1);

            var deviceDateTonersValues = _context.TonersUsed.Where(x => x.DateAndTime.Date >= firstDayOfMonth.Date && x.DateAndTime.Date <= dateToday.Date &&
                                                                        x.DeviceId == deviceId).ToList();
       
            #region Initialize
            if (deviceDateTonersValues == null)
            {
                deviceDateTonersValues = new List<TonersUsed>();
            }
            #endregion

            int black = 0; int cyan = 0; int yellow = 0; int magenta = 0;
            foreach (var device in deviceDateTonersValues)
            {
                black = device.BlackTonersUsed == 1 ? black + 1 : black + 0;
                cyan = device.CyanTonersUsed == 1 ? cyan + 1 : cyan + 0;
                yellow = device.YellowTonersUsed == 1 ? yellow + 1 : yellow + 0;
                magenta = device.MagentaTonersUsed == 1 ? magenta + 1 : magenta + 0;
            }

            var tonersValuesToReturn = new TonersUsed
            {
                BlackTonersUsed = black,
                CyanTonersUsed = cyan,
                MagentaTonersUsed = magenta,
                YellowTonersUsed = yellow,
                DeviceId = deviceId
            };

            #region NoDataInTheMonthYet
            if (!afterSix && DateTime.Now.Date == new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1) && actualMonth)
            {
                tonersValuesToReturn = new TonersUsed
                {
                    BlackTonersUsed = 0,
                    CyanTonersUsed = 0,
                    MagentaTonersUsed = 0,
                    YellowTonersUsed = 0,
                    DeviceId = deviceId
                };
            }
            #endregion

            return Task.FromResult(tonersValuesToReturn);
        }
    }
}
 