using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationCore.Models.Reports;
using ApplicationCore.Services.Interfaces;
using ApplicationCore.Services.Interfaces.Reports;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class DailyRecordController : ControllerBase 
    {
        private readonly IDailyDeviceService _dailyDeviceService;

        public DailyRecordController(IDailyDeviceService dailyDeviceService)
        {
            _dailyDeviceService = dailyDeviceService;
        }


        [HttpGet("GetContadoresDataFromSelectedRangeDate/{deviceId}/{date1}/{date2}")]
        public async Task<ActionResult<DailyContadoresDataDevices>> GetContadoresDataFromSelectedRangeDate(int deviceId, DateTime date1, DateTime date2) 
        {
            return Ok(await _dailyDeviceService.GetContadoresDataFromSelectedRangeDate(deviceId, date1, date2));
        }

        [HttpGet("GetContadoresDataFromActualOrPreviousMonth/{deviceId}/{actualMonth}")]
        public async Task<ActionResult<DailyContadoresDataDevices>> GetContadoresDataFromActualOrPreviousMonth(int deviceId, bool actualMonth)
        {
            return Ok(await _dailyDeviceService.GetContadoresDataFromActualOrPreviousMonth(deviceId, actualMonth));
        }

        [HttpGet("GetTonersDataFromSelectedRangeDate/{deviceId}/{date1}/{date2}")]
        public async Task<ActionResult<TonersUsed>> GetTonersDataFromSelectedRangeDate(int deviceId, DateTime date1, DateTime date2) 
        {
            return Ok(await _dailyDeviceService.GetTonersDataFromSelectedRangeDate(deviceId, date1, date2));
        }

        [HttpGet("GetTonersDataFromActualOrPreviousMonth/{deviceId}/{actualMonth}")]
        public async Task<ActionResult<TonersUsed>> GetTonersDataFromActualOrPreviousMonth(int deviceId, bool actualMonth)
        {
            return Ok(await _dailyDeviceService.GetTonersDataFromActualOrPreviousMonth(deviceId, actualMonth));
        }
    }
}
