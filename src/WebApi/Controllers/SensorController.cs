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
        private readonly IHttpClientFactory clientFactory;

        public SensorController(ISensorService prtgService, IHttpClientFactory clientFactory)
        {
            _prtgService = prtgService;
            this.clientFactory = clientFactory;
        }

        [HttpGet]
        [Route("GetAllSensors")]
        public async Task<Sensor> GetAllSensors()
        {
            return await _prtgService.GetAllSensors();
        }
        
        [HttpGet]
        public async Task<string> Get()
        {
            var client = clientFactory.CreateClient("test");
            var response = await client.GetAsync("");
            return await response.Content.ReadAsStringAsync();
        }

        [HttpGet]
        [Route("GetAllPrinters")]
        public async Task<List<Printer>> GetAllPrinters()
        {
            return await _prtgService.GetAllPrinters();
        }
    }
}
