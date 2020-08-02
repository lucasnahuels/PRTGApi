using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ApplicationCore.Services.Interfaces.Reports;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScheduleController : Controller
    {
        private readonly IDailyDeviceService _dailyDeviceService;

        public ScheduleController(IDailyDeviceService dailyDeviceService)
        {
            _dailyDeviceService = dailyDeviceService;
        }

        [HttpGet("contadores")]
        public async Task<ActionResult> DailyContadores()
        {
            try
            {
                await _dailyDeviceService.CreateDailyContadoresDeviceValues();
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpGet]
        public async Task<ActionResult> ExcedenteCopias()
        {
            try
            {
                await _dailyDeviceService.CreateDailyContadoresDeviceValues();
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
    }
}
