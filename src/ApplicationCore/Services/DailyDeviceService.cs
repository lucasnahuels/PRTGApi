using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using ApplicationCore.EntityFramework;
using ApplicationCore.Models.Reports;
using ApplicationCore.Services.Interfaces.Reports;
using ApplicationCore.Services.Interfaces;

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
        //public async Task<DailyDevices> CreateAsync(DailyDevices dailyPrinter)
        //{
        //    await _context.DailyPrinters.AddAsync(dailyPrinter);
        //    await _context.SaveChangesAsync();

        //    return dailyPrinter;
        //}

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
            var devices = await _sensorService.GetAllDevices();
            var dailyDevices = new List<DailyContadoresDataDevices>();

            foreach(var device in devices)
            {
                var childDevices = await _sensorService.GetChildDevices(device.ObjId);
                foreach(var childDevice in childDevices)
                {
                    var sensorDetails = await _sensorService.GetSensorDetails(childDevice.ObjId);
                    if (sensorDetails.SensorData.Name == "Contadores")
                    { 
                        var dailyContadoresDataDevice = await _sensorService.GetDailyContadoresDevicesValues(childDevice.ObjId);
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
            var dailyPrinter = await _sensorService.GetDailyTonersDevicesValues(objId);
            //crear tabla para daily toners devices
            //await _context.DailyPrinters.AddAsync(dailyPrinter);
            await _context.SaveChangesAsync();

            return dailyPrinter;
        }

        public async Task<DailyTonersDataDevices> CreateFifteenMinutesTonersDeviceValues(int objId)
        {
            var dailyPrinter = await _sensorService.GetDailyTonersDevicesValues(objId);
            //crear tabla para fifteen minutes toners devices
            //await _context.DailyPrinters.AddAsync(dailyPrinter);
            await _context.SaveChangesAsync();

            return dailyPrinter;
        }
    }
}
