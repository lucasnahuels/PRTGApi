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
        public async Task<ActionResult<DailyContadoresDataDevices>> GetContadoresDataFromSelectedRangeDate(int deviceId, DateTime date1, DateTime date2) //fromQuery??
        {
            return Ok(await _dailyDeviceService.GetContadoresDataFromSelectedRangeDate(deviceId, date1, date2));
        }

        [HttpGet("GetContadoresDataFromActualMonth/{deviceId}")]
        public async Task<ActionResult<DailyContadoresDataDevices>> GetContadoresDataFromActualMonth(int deviceId)
        {
            return Ok(await _dailyDeviceService.GetContadoresDataFromActualMonth(deviceId));
        }

        [HttpGet("GetTonersDataFromSelectedRangeDate/{deviceId}/{date1}/{date2}")]
        public async Task<ActionResult<DailyTonersDataDevices>> GetTonersDataFromSelectedRangeDate(int deviceId, DateTime date1, DateTime date2) //fromQuery??
        {
            return Ok(await _dailyDeviceService.GetTonersDataFromSelectedRangeDate(deviceId, date1, date2));
        }

        [HttpGet("GetTonersDataFromActualMonth/{deviceId}")]
        public async Task<ActionResult<DailyContadoresDataDevices>> GetTonersDataFromActualMonth(int deviceId)
        {
            return Ok(await _dailyDeviceService.GetTonersDataFromActualMonth(deviceId));
        }
    }
}
