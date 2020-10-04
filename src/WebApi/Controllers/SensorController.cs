using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ApplicationCore.Models;
using ApplicationCore.Services.Interfaces;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class SensorController : ControllerBase
    {
        private readonly ISensorService _prtgService;

        public SensorController(ISensorService prtgService)
        {
            _prtgService = prtgService;
        }

        [HttpGet]
        [Route("GetAllChannels")]
        public async Task<ActionResult<SensorList>> GetAllChannels()
        {
            var sensors = await _prtgService.GetAllSensors();
            return Ok(sensors);
        }

        [HttpGet]
        [Route("GetAllDevices")]
        public async Task<ActionResult<List<DeviceApiModel>>> GetAllDevices()
        {
            var devices = await _prtgService.GetAllDevices();
            return Ok(devices);
        }

        [HttpGet("GetAssignedDevices/{contractId}")]
        public async Task<ActionResult<List<DeviceApiModel>>> GetAssignedDevices(int contractId)
        {
            var devices = await _prtgService.GetAssignedDevices(contractId);
            return Ok(devices);
        }

        [HttpGet("GetUnassignedDevices")]
        public async Task<ActionResult<List<DeviceApiModel>>> GetUnassignedDevices()
        {
            var devices = await _prtgService.GetUnassignedDevices();
            return Ok(devices);
        }

        [HttpGet("{objId}")]
        [Route("GetSensorDetails")]
        public async Task<ActionResult<SensorDetails>> GetSensorDetails(int objId)
        {
            var sensorDetails = await _prtgService.GetSensorDetails(objId);
            return Ok(sensorDetails);
        }

        [HttpGet("{objId}")]
        [Route("GetContadoresData")]
        public async Task<ActionResult<SensorsData>> GetContadoresData(int objId)
        {
            var contadores = await _prtgService.GetContadoresData(objId);
            return Ok(contadores);
        }

        [HttpGet("{objId}")]
        [Route("GetTonersData")]
        public async Task<ActionResult<SensorsData>> GetTonersData(int objId)
        {
            var tonersData = await _prtgService.GetTonersData(objId);
            return Ok(tonersData);
        }

        [HttpGet]
        [Route("GetApiData")]
        public async Task<ActionResult<DevicesSensor>> GetApiData()
        {
            var apiData = await _prtgService.GetApiData();
            return Ok(apiData);
        }

        [HttpGet("{objId}")]
        [Route("GetDeviceData/{objId}")]
        public async Task<ActionResult<DeviceApiModel>> GetDeviceData(int objId)
        {
            var deviceData = await _prtgService.GetDeviceData(objId);
            return Ok(deviceData);
        }
    }
}
