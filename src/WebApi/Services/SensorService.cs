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

        public async Task<SensorList> GetAllSensors()
        {
            var client = _clientFactory.CreateClient("prtg");
            var response = await client.GetAsync(
                "api/table.json?username=prtgadmin&password=Si5t3m4s&noraw=0&content=channel&columns=group,device,objid&filter_group=IMPRESORAS"
                );
            var jsonResponse = await response.Content.ReadAsStringAsync();
            
            return JsonConvert.DeserializeObject<SensorList>(jsonResponse);
        }

        public async Task<List<DeviceApiModel>> GetAllDevices()
        {
            var client = _clientFactory.CreateClient("prtg");
                
            var response = await client.GetAsync("api/table.json?username=prtgadmin&password=Si5t3m4s&noraw=0&content=devices&columns=group,device,objid&filter_group=IMPRESORAS");
            var jsonResponse = await response.Content.ReadAsStringAsync();

            var devicesSensor = JsonConvert.DeserializeObject<DevicesSensor>(jsonResponse);
            
            var devicesList= new List<DeviceApiModel>();
            devicesSensor.Devices.ForEach(device => devicesList.Add(device));

            return devicesList;
        }

        public async Task<List<DeviceApiModel>> GetChildDevices(int parentDeviceObjId)
        {
            var client = _clientFactory.CreateClient("prtg");
            var response = await client.GetAsync(
                $"api/table.json?username=prtgadmin&password=Si5t3m4s&noraw=0&content=device&columns=group,device,objid&filter_group=IMPRESORAS&id={parentDeviceObjId}"
                );
            var jsonResponse = await response.Content.ReadAsStringAsync();

            var devicesSensor = JsonConvert.DeserializeObject<DeviceSensor>(jsonResponse);

            var devicesList = new List<DeviceApiModel>();
            devicesSensor.Device.ForEach(device => devicesList.Add(device));

            return devicesList;
        }

        public async Task<SensorDetails> GetSensorDetails(int objId)
        {
            var client = _clientFactory.CreateClient("prtg");
            var response = await client.GetAsync(
                $"api/getsensordetails.json?username=prtgadmin&password=Si5t3m4s&id={objId}"
                );
            var jsonResponse = await response.Content.ReadAsStringAsync();

            var sensorData = JsonConvert.DeserializeObject<SensorDetails>(jsonResponse);

            return sensorData;
        }

        public async Task<SensorsData> GetContadoresData(int objId)
        {
            var client = _clientFactory.CreateClient("prtg");
            var response = await client.GetAsync(
                $"/api/table.json?username=prtgadmin&password=Si5t3m4s&noraw=1&content=channels&sortby=name&columns=name%3Dtextraw%2Cinfo%3Dtreejson%2Cminimum%2Cmaximum%2Ccondition%2Clastvalue&id={objId}"
                );
            var jsonResponse = await response.Content.ReadAsStringAsync();

            var contadoresData = JsonConvert.DeserializeObject<SensorsData>(jsonResponse);

            contadoresData.SensorName = "Contadores";

            return contadoresData;
        }

        public async Task<SensorsData> GetTonersData(int objId)
        {
            var client = _clientFactory.CreateClient("prtg");
            var response = await client.GetAsync(
                $"/api/table.json?username=prtgadmin&password=Si5t3m4s&noraw=1&content=channels&sortby=name&columns=name%3Dtextraw%2Cinfo%3Dtreejson%2Cminimum%2Cmaximum%2Ccondition%2Clastvalue&id={objId}"
                );
            var jsonResponse = await response.Content.ReadAsStringAsync();

            var toners = JsonConvert.DeserializeObject<SensorsData>(jsonResponse);

            toners.SensorName = "Toners";

            return toners;
        }

        public async Task<DevicesSensor> GetApiData()
        {
            var apiData = new DevicesSensor
            {
                Devices = await GetAllDevices()
            };

            foreach (var device in apiData.Devices)
            {
                var sensorsData = new List<SensorsData>();
                var childDevices = GetChildDevices(device.ObjId).Result;

                foreach (var childDevice in childDevices)
                {
                    var sensorDetails = GetSensorDetails(childDevice.ObjId).Result;
                    if (sensorDetails.SensorData.Name == "Contadores")
                        sensorsData.Add(GetContadoresData(childDevice.ObjId).Result);
                    if (sensorDetails.SensorData.Name == "Toners")
                        sensorsData.Add(GetTonersData(childDevice.ObjId).Result);
                }
                device.SensorList = sensorsData;
            }
            return apiData;
        }

        public async Task<DeviceApiModel> GetDeviceData(int objId)
        {
            var device = new DeviceApiModel();
            device.ObjId = objId;
            device.Device = GetSensorDetails(objId).Result.SensorData.Name;

            var sensorsData = new List<SensorsData>();
            var childDevices = await GetChildDevices(device.ObjId);

            foreach (var childDevice in childDevices)
            {
                var sensorDetails = GetSensorDetails(childDevice.ObjId).Result;
                var parentDeviceId = sensorDetails.SensorData.ParentDeviceId;
                if (sensorDetails.SensorData.ParentDeviceName == "Contadores")
                {
                    var contadoresData = GetContadoresData(childDevice.ObjId).Result;
                    sensorsData.Add(contadoresData);
                }
                if (sensorDetails.SensorData.ParentDeviceName == "Toners")
                {
                    var tonersData = GetTonersData(childDevice.ObjId).Result;
                    sensorsData.Add(tonersData);
                }
            }
            device.SensorList = sensorsData;
            return device;
        }
    }
}
