using ApplicationCore.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApplicationCore.Services.Interfaces
{
    public interface ISensorService
    {
        Task<SensorList> GetAllSensors();
        Task<List<DeviceApiModel>> GetAllDevices();
        Task<SensorDetails> GetSensorDetails(int objId);
        Task<SensorsData> GetContadoresData(int objId);
        Task<SensorsData> GetTonersData(int objId);
        Task<DevicesSensor> GetApiData();
        Task<DeviceApiModel> GetDeviceData(int objId);
    }
}
