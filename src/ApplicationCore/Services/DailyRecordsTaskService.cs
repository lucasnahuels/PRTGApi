using ApplicationCore.Services.Interfaces.Reports;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Mail;
using System.Threading;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class DailyRecordsTaskService : IDailyRecordsTaskService
    {
        public void CreateDailyReport()
        {
            Debug.WriteLine("Hola");
        }
    }
}
