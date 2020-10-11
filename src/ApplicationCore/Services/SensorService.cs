using Newtonsoft.Json;
using ApplicationCore.Models;
using ApplicationCore.Services.Interfaces;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using ApplicationCore.Models.Reports;
using System;
using System.Linq;

namespace ApplicationCore.Services
{
    public class SensorService : ISensorService
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly IContractService _contractService;

        #region ApiData
        private const string username = "prtgadmin";
        private const string password = "Si5t3m4s";
        private const string apiTable = "api/table.json";
        private const string apiSensorDetails = "api/getsensordetails.json";
        #endregion

        #region ChannelNames
        private const string CopiasBlackAndWhite = "Copias Black & White";
        private const string PrintBlackAndWhite = "Print Black & White";

        private const string CopiasFullColor = "Copias Full Color";
        private const string CopiasSingleColor = "Copias Single Color";
        private const string CopiasTwoColor = "Copias Two-color";
        private const string PrintFullColor = "Print Full Color";
        private const string PrintSingleColor = "Print Single Color";
        private const string PrintTwoColor = "Print Two-color";

        private const string Duplex = "Duplex";
        #endregion

        #region SensorDataNames
        public const string Contadores = "Contadores";
        private const string Toners = "Toners"; 
        #endregion

        public SensorService(IHttpClientFactory clientFactory, IContractService contractService)
        {
            _clientFactory = clientFactory;
            _contractService = contractService;
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

        public async Task<List<DeviceApiModel>> GetAssignedDevices(int contractId)
        {
            var devicesList = new List<DeviceApiModel>();
            
            var contract = _contractService.GetAsync(contractId);
            var devices = await GetAllDevices();
            foreach(var device in devices)
            {
                foreach(var contractDevice in contract.Result.ContractDevices)
                {
                    if(device.ObjId.ToString() == contractDevice.ObjId)
                    {
                        devicesList.Add(device);
                    }
                }
            }

            return devicesList;
        }

        public async Task<List<DeviceApiModel>> GetUnassignedDevices()
        {
            var devicesList = new List<DeviceApiModel>();

            var devices = await GetAllDevices();
            var contractDevicesRelations = _contractService.GetContractDevicesRelations().Result.ToList();
            foreach (var device in devices)
            {
                var found = false;
                foreach(var contractDeviceRelation in contractDevicesRelations)
                {
                    if(device.ObjId.ToString() == contractDeviceRelation.ObjId)
                    {
                        found = true;
                    }
                }
                if (!found)
                {
                    devicesList.Add(device);
                }
            }

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

        public async Task<int> GetChildDeviceNamedTonersAsync(int parentDeviceObjId)
        {
            var childDevices = await GetChildDevices(parentDeviceObjId);

            foreach (var childDevice in childDevices)
            {
                var sensorDetails = await GetSensorDetails(childDevice.ObjId);
                if (sensorDetails.SensorData.Name == Toners)
                    return childDevice.ObjId;
            }
            return 0;
        }

        public async Task<SensorDetails> GetSensorDetails(int objId)
        {
            var client = _clientFactory.CreateClient("prtg");
            var response = await client.GetAsync(
                $"{apiSensorDetails}?username={username}&password={password}&id={objId}"
                );
            var jsonResponse = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<SensorDetails>(jsonResponse);
        }

        public async Task<SensorsData> GetContadoresData(int objId)
        {
            var client = _clientFactory.CreateClient("prtg");
            var response = await client.GetAsync(
                $"{apiTable}?username={username}&password={password}&noraw=1&content=channels&sortby=name&columns=name%3Dtextraw%2Cinfo%3Dtreejson%2Cminimum%2Cmaximum%2Ccondition%2Clastvalue&id={objId}"
                );
            var jsonResponse = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<SensorsData>(jsonResponse);
        }

        public async Task<SensorsData> GetTonersData(int objId)
        {
            var client = _clientFactory.CreateClient("prtg");
            var response = await client.GetAsync(
                $"{apiTable}?username={username}&password={password}&noraw=1&content=channels&sortby=name&columns=name%3Dtextraw%2Cinfo%3Dtreejson%2Cminimum%2Cmaximum%2Ccondition%2Clastvalue&id={objId}"
                );
            var jsonResponse = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<SensorsData>(jsonResponse);
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
                    if (sensorDetails.SensorData.Name == Contadores)
                        device.Contadores = await GetContadoresData(childDevice.ObjId);
                    if (sensorDetails.SensorData.Name == Toners)
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
                if (sensorDetails.SensorData.Name == Contadores)
                {
                    var contadoresData = await GetContadoresData(childDevice.ObjId);
                    device.Contadores = contadoresData;
                }
                if (sensorDetails.SensorData.Name == Toners)
                {
                    var tonersData = await GetTonersData(childDevice.ObjId);
                    device.Toners = tonersData;
                }
            }
            return device;
        }
    }
}
