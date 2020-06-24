using PRTG_Api.Models;
using System.Threading.Tasks;

namespace PRTG_Api.Services.Interfaces
{
    public interface ISensorService
    {
        Task<Sensor> GetAllSensors();
    }
}
