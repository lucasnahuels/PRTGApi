using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;
using WebApi.Services.Interfaces;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SensorController : ControllerBase
    {
        private readonly ISensorService _prtgService;

        public SensorController(ISensorService prtgService)
        {
            _prtgService = prtgService;
        }

        [HttpGet]
        [Route("GetAllSensors")]
        public async Task<Sensor> GetAllSensors()
        {
            return await _prtgService.GetAllSensors();
        }


        [HttpGet]
        [Route("GetAllPrinters")]
        public async Task<List<Printer>> GetAllPrinters()
        {
            return await _prtgService.GetAllPrinters();
        }
    }
}
