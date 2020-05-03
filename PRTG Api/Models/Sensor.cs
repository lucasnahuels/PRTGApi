﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PRTG_Api.Models
{
    public class Sensor
    {
        public string PrtgVersion { get; set; }
        public int TreeSize { get; set; }
        public List<Channel> Channels { get; set; }
    }
}