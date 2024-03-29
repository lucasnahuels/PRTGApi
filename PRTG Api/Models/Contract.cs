﻿namespace PRTG_Api.Models
{
    public class Contract
    {
        public int ContractId { get; set; }
        public string NameOfCompany { get; set; }
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
