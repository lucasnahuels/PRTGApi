using Newtonsoft.Json;
using PRTG_Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace PRTG_Api.Services
{
    public class SensorService : ISensorService
    {
        private readonly IHttpClientFactory _clientFactory;

        public SensorService(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<Sensor> GetAllSensors()
        {

            // Pass the handler to httpclient(from you are calling api)

            var client = _clientFactory.CreateClient("prtg");

            var response = await client.GetAsync("api/table.json?noraw=0&content=channels&sortby=name&columns=name=textraw,info=treejson,minimum,maximum,condition,lastvalue&id=2350&_=1581974404523&username=prtgadmin&password=Si5t3m4s");

            //response.EnsureSuccessStatusCode();

            var jsonResponse = await response.Content.ReadAsStringAsync();
            var jsonConverted = JsonConvert.DeserializeObject<Sensor>(jsonResponse);
            return jsonConverted;

        }

    }
}
