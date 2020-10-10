using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services.Interfaces.Reports
{
    public interface IRefilledTonersRecordsScheduleService
    {
        Task CreateRefilledTonersRecord();
    }
}
