using PRTG_Api.Models;
using System.Threading.Tasks;

namespace PRTG_Api.Services
{
    public interface ISensorService
    {
        Task<Sensor> GetAllSensors();
    }
}
