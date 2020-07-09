using Newtonsoft.Json;
using WebApi.Models;
using WebApi.Services.Interfaces;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace WebApi.Services
{
    public class SensorService : ISensorService
    {
        private readonly IHttpClientFactory _clientFactory;

        public SensorService(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        //Change Method name, it's confusing
        public async Task<Sensor> GetAllSensors()
        {
            var client = _clientFactory.CreateClient("prtg");
            var response = await client.GetAsync("api/table.json?noraw=0&content=channels&sortby=name&columns=name=textraw,info=treejson,minimum,maximum,condition,lastvalue&id=2350&_=1581974404523&username=prtgadmin&password=Si5t3m4s");
            var jsonResponse = await response.Content.ReadAsStringAsync();
            
            return JsonConvert.DeserializeObject<Sensor>(jsonResponse);
        }

        public async Task<List<Printer>> GetAllPrinters()
        {
            var client = _clientFactory.CreateClient("prtg");
            var response = await client.GetAsync("api/table.json?noraw=0&content=devices&columns=group,device&filter_group=IMPRESORAS&username=prtgadmin&password=Si5t3m4s");
            var jsonResponse = await response.Content.ReadAsStringAsync();

            var devicesSensor = JsonConvert.DeserializeObject<DevicesSensor>(jsonResponse);
            
            var printerList= new List<Printer>();
            devicesSensor.Devices.ForEach(device => printerList.Add(device));

            return printerList;
        }
    }
}
