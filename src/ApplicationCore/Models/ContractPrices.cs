using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Models
{
    public class ContractPrices
    {
        public long DeviceId { get; set; }
        public float BlackAndWhiteCopiesPrices { get; set; }
        public float ColorCopiesPrices { get; set; }
        public float TotalCopiesPrices { get; set; }
    }
}
