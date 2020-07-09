using WebApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebApi.Services.Interfaces
{
    public interface ISensorService
    {
        Task<Sensor> GetAllSensors();
        Task<List<Printer>> GetAllPrinters();
    }
}
