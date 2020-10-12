using Microsoft.EntityFrameworkCore;
using ApplicationCore.EntityFramework;
using ApplicationCore.Models;
using ApplicationCore.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using ApplicationCore.Models.Reports;
using System.Linq;

namespace ApplicationCore.Services
{
    public class MailingMonthReportService : IMailingMonthReportService
    {
        private readonly PrtgDbContext _context;

        public MailingMonthReportService(PrtgDbContext context)
        {
            _context = context;
        }

        public async Task<MailingMonthReport> CreateAsync(MailingMonthReport mailingMonthReport)
        {
            await _context.MailingMonthReport.AddAsync(mailingMonthReport);
            await _context.SaveChangesAsync();
      
            return mailingMonthReport;
        }

        public async Task<IEnumerable<MailingMonthReport>> GetAsync()
        {
            return await _context.MailingMonthReport.ToListAsync();
        }

        public async Task<MailingMonthReport> GetByReportAsync(MailingMonthReport mailingMonthReport)
        {
            return await _context.MailingMonthReport
                .FirstOrDefaultAsync(x => x.DeviceId == mailingMonthReport.DeviceId &&
                x.ContractId == mailingMonthReport.ContractId && 
                x.Year == mailingMonthReport.Year &&
                x.Month == mailingMonthReport.Month &&
                x.IsColor == mailingMonthReport.IsColor);
        }
    }
}
