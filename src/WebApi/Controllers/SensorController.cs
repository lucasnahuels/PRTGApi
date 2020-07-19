using System.Collections.Generic;
using System.Net.Http;
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
        [Route("GetAllChannels")]
        public async Task<SensorList> GetAllChannels()
        {
            return await _prtgService.GetAllSensors();
        }

        [HttpGet]
        [Route("GetAllDevices")]
        public async Task<List<DeviceApiModel>> GetAllDevices()
        {
            return await _prtgService.GetAllDevices();
        }

        [HttpGet("{objId}")]
        [Route("GetSensorDetails")]
        public async Task<SensorDetails> GetSensorDetails(int objId)
        {
            return await _prtgService.GetSensorDetails(objId);
        }

        [HttpGet("{objId}")]
        [Route("GetContadoresData")]
        public async Task<SensorsData> GetContadoresData(int objId)
        {
            return await _prtgService.GetContadoresData(objId);
        }

        [HttpGet("{objId}")]
        [Route("GetTonersData")]
        public async Task<SensorsData> GetTonersData(int objId)
        {
            return await _prtgService.GetTonersData(objId);
        }

        [HttpGet]
        [Route("GetApiData")]
        public DevicesSensor GetApiData()
        {
            return _prtgService.GetApiData();
        }
    }
}
