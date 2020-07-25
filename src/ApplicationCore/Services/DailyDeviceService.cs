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

        public async Task<DailyContadoresDataDevices> CreateDailyContadoresDeviceValues()
        {
            var devices = _sensorService.GetAllDevices().Result;
            var dailyDevices= new DailyContadoresDataDevices();
            foreach (var device in devices)
            {
                dailyDevices = _sensorService.GetDailyContadoresDevicesValues(device.ObjId);
            } 
            await _context.DailyContadores.AddAsync(dailyDevices);
            await _context.SaveChangesAsync();

            return dailyDevices;
        }

        public async Task<DailyTonersDataDevices> CreateDailyTonersDeviceValues(int objId)
        {
            var dailyPrinter = _sensorService.GetDailyTonersDevicesValues(objId);
            //crear tabla para daily toners devices
            //await _context.DailyPrinters.AddAsync(dailyPrinter);
            await _context.SaveChangesAsync();

            return dailyPrinter;
        }

        public async Task<DailyTonersDataDevices> CreateFifteenMinutesTonersDeviceValues(int objId)
        {
            var dailyPrinter = _sensorService.GetDailyTonersDevicesValues(objId);
            //crear tabla para fifteen minutes toners devices
            //await _context.DailyPrinters.AddAsync(dailyPrinter);
            await _context.SaveChangesAsync();

            return dailyPrinter;
        }
    }
}
