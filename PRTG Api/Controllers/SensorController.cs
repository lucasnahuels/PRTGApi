using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PRTG_Api.Models;
using PRTG_Api.Services;

namespace PRTG_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SensorController : ControllerBase
    {
        private readonly DataBaseContext _context;
        private readonly ISensorService _prtgService;

        public SensorController(DataBaseContext context, ISensorService prtgService)
        {
            _context = context;
            _prtgService = prtgService;
        }


        // GET: api/Sensor
        [HttpGet]
        public async Task<Sensor> GetAllSensors()       
        {
            return await _prtgService.GetAllSensors();
        }

        // GET: api/TodoItems/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<Sensor>> GetSensor(long id)
        //{
            //var todoItem = await _context.TodoItems.FindAsync(id);

            //if (todoItem == null)
            //{
            //    return NotFound();
            //}

            //return todoItem;
        //}
    }
}
