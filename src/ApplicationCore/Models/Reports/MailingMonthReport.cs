using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Models.Reports
{
    public class MailingMonthReport : BaseModel
    {
        public string DeviceId { get; set; }
        public long ContractId { get; set; }
        public bool IsColor { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
    }
}
