using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class TimedService : BackgroundService
    {
        //static int tablaDelCinco = 5;

        //protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        //{
        //    while (!stoppingToken.IsCancellationRequested)
        //    {
        //        Debug.WriteLine($"Han pasado {tablaDelCinco} segundos desde que se ejecutó la app");
        //        tablaDelCinco = tablaDelCinco + 5;
        //        await Task.Delay(5000, stoppingToken); //se ejecuta cada cinco segundos
        //    }
        //}


        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(TimeSpan.FromDays(1) , stoppingToken); 
            }
        }
    }
}
