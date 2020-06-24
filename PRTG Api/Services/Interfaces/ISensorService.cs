using PRTG_Api.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PRTG_Api.Services.Interfaces
{
    public interface ISensorService
    {
        Task<Sensor> GetAllSensors();
        Task<List<Printer>> GetAllPrinters();
    }
}
