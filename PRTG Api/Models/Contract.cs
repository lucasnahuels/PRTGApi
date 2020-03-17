using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PRTG_Api.Models
{
    public class Contract
    {
        public int ContractId { get; set; }
        public string NameOfCompañy { get; set; }
        public string Printer { get; set; }
        public int BlackAndWhiteSheets { get; set; }
        public int ColorSheets { get; set; }
        public int BlackToner { get; set; }
        public int CyanToner { get; set; }
        public int YellowToner { get; set; }
        public int MagentaToner { get; set; }
        public int Month { get; set; }
    }
}
