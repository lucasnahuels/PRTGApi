using Newtonsoft.Json;
using ApplicationCore.Models;
using ApplicationCore.Services.Interfaces;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using ApplicationCore.Models.Reports;
using System;

namespace ApplicationCore.Services
{
    public class SensorService : ISensorService
    {
        private readonly IHttpClientFactory _clientFactory;
        private const string username = "prtgadmin";
        private const string password = "Si5t3m4s";
        private const string apiTable = "api/table.json";
        private const string apiSensorDetails = "api/getsensordetails.json";

        public SensorService(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<SensorList> GetAllSensors()
        {
            var client = _clientFactory.CreateClient("prtg");
            var response = await client.GetAsync(
                $"{apiTable}?username={username}&password={password}&noraw=0&content=channel&columns=group,device,objid&filter_group=IMPRESORAS"
                );
            var jsonResponse = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<SensorList>(jsonResponse);
        }

        public async Task<List<DeviceApiModel>> GetAllDevices()
        {
            var client = _clientFactory.CreateClient("prtg");

            var response = await client.GetAsync($"{apiTable}?username={username}&password={password}&noraw=0&content=devices&columns=group,device,objid&filter_group=IMPRESORAS");
            var jsonResponse = await response.Content.ReadAsStringAsync();

            var devicesSensor = JsonConvert.DeserializeObject<DevicesSensor>(jsonResponse);

            var devicesList = new List<DeviceApiModel>();
            devicesSensor.Devices.ForEach(device => devicesList.Add(device));

            return devicesList;
        }

        public async Task<List<DeviceApiModel>> GetChildDevices(int parentDeviceObjId)
        {
            var client = _clientFactory.CreateClient("prtg");
            var response = await client.GetAsync(
                $"{apiTable}?username={username}&password={password}&noraw=0&content=device&columns=group,device,objid&filter_group=IMPRESORAS&id={parentDeviceObjId}"
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
                $"{apiSensorDetails}?username={username}&password={password}&id={objId}"
                );
            var jsonResponse = await response.Content.ReadAsStringAsync();

            var sensorData = JsonConvert.DeserializeObject<SensorDetails>(jsonResponse);

            return sensorData;
        }

        public async Task<SensorsData> GetContadoresData(int objId)
        {
            var client = _clientFactory.CreateClient("prtg");
            var response = await client.GetAsync(
                $"{apiTable}?username={username}&password={password}&noraw=1&content=channels&sortby=name&columns=name%3Dtextraw%2Cinfo%3Dtreejson%2Cminimum%2Cmaximum%2Ccondition%2Clastvalue&id={objId}"
                );
            var jsonResponse = await response.Content.ReadAsStringAsync();

            var contadoresData = JsonConvert.DeserializeObject<SensorsData>(jsonResponse);

            return contadoresData;
        }

        public async Task<SensorsData> GetTonersData(int objId)
        {
            var client = _clientFactory.CreateClient("prtg");
            var response = await client.GetAsync(
                $"{apiTable}?username={username}&password={password}&noraw=1&content=channels&sortby=name&columns=name%3Dtextraw%2Cinfo%3Dtreejson%2Cminimum%2Cmaximum%2Ccondition%2Clastvalue&id={objId}"
                );
            var jsonResponse = await response.Content.ReadAsStringAsync();

            var toners = JsonConvert.DeserializeObject<SensorsData>(jsonResponse);

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
                var childDevices = await GetChildDevices(device.ObjId);

                foreach (var childDevice in childDevices)
                {
                    var sensorDetails = await GetSensorDetails(childDevice.ObjId);
                    if (sensorDetails.SensorData.Name == "Contadores")
                        device.Contadores = await GetContadoresData(childDevice.ObjId);
                    if (sensorDetails.SensorData.Name == "Toners")
                        device.Toners = await GetTonersData(childDevice.ObjId);
                }
            }
            return apiData;
        }

        public async Task<DeviceApiModel> GetDeviceData(int objId)
        {
            var device = new DeviceApiModel
            {
                ObjId = objId,
                Device = (await GetSensorDetails(objId)).SensorData.Name
            };

            var childDevices = await GetChildDevices(device.ObjId);

            foreach (var childDevice in childDevices)
            {
                var sensorDetails = await GetSensorDetails(childDevice.ObjId);
                if (sensorDetails.SensorData.Name == "Contadores")
                {
                    var contadoresData = await GetContadoresData(childDevice.ObjId);
                    device.Contadores = contadoresData;
                }
                if (sensorDetails.SensorData.Name == "Toners")
                {
                    var tonersData = await GetTonersData(childDevice.ObjId);
                    device.Toners = tonersData;
                }
            }
            return device;
        }

        public async Task<DailyContadoresDataDevices> GetDailyContadoresDevicesValues(int objId)
        {
            var contadores = await GetContadoresData(objId);
            DateTime localDate = DateTime.Now;
            //falta restarle el valor de ayer a los siguientes valores
            var dailyDevice = new DailyContadoresDataDevices
            {
                ColorCopies = int.Parse(contadores.Channels[1].LastValue) + int.Parse(contadores.Channels[6].LastValue),
                BlackAndWhiteCopies = int.Parse(contadores.Channels[0].LastValue) + int.Parse(contadores.Channels[5].LastValue),
                Device = new Device { ObjId = objId },
                Date = localDate
            };
            return dailyDevice;
        }
        public async Task<DailyTonersDataDevices> GetDailyTonersDevicesValues(int objId)
        {
            var tonersUsed = await GetQuantityTonersToday(objId);
            DateTime localDate = DateTime.Now;
            var dailyDevice = new DailyTonersDataDevices
            {
                BlackTonersUsed = tonersUsed.BlackTonersUsed,
                CyanTonersUsed = tonersUsed.CyanTonersUsed,
                MagentaTonersUsed = tonersUsed.MagentaTonersUsed,
                YellowTonersUsed = tonersUsed.YellowTonersUsed,
                Device = new Device { ObjId = objId },
                Date = localDate
            };
            return dailyDevice;
        }

        public Task<DailyTonersDataDevices> GetQuantityTonersToday(int objId)
        {
            //get from database the values from toners used today
            var tonersUsedToday = new DailyTonersDataDevices();
            return Task.FromResult(tonersUsedToday);
        }
    }
}
