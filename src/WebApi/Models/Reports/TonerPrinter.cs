using System;

namespace WebApi.Models.Reports
{
    public class TonerPrinter : BaseModel
    {
        public Printer Printer { get; set; }
        public DateTime Date { get; set; }
        
//-Cyan | Porcentaje
//-Yellow | Porcentaje
//-Magenta | Porcentaje
//-Negro | Porcentaje

    }
}
