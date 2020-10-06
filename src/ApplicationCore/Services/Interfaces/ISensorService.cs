using ApplicationCore.Models;
using ApplicationCore.Models.Reports;
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
        Task<List<DeviceApiModel>> GetChildDevices(int parentDeviceObjId);
        Task<List<DeviceApiModel>> GetAssignedDevices(int contractId);
        Task<List<DeviceApiModel>> GetUnassignedDevices();

    }
}
