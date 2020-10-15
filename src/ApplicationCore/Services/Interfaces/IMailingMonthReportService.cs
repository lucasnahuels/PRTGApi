using ApplicationCore.Models;
using ApplicationCore.Models.Reports;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApplicationCore.Services.Interfaces
{
    public interface IMailingMonthReportService
    {
        Task<IEnumerable<MailingMonthReport>> GetAsync();
        Task<MailingMonthReport> GetByReportAsync(MailingMonthReport mailingMonthReport);
        Task<MailingMonthReport> CreateAsync(MailingMonthReport mailingMonthReport);
    }
}
