using PRTG_Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace PRTG_Api.Services
{
    public interface IPRTGService
    {
        Task<Sensor> GetAllSensors();
    }
}
