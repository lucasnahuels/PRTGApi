using System.Threading.Tasks;

namespace ApplicationCore.Services.Interfaces
{
    public interface IMailerService
    {
        Task SendEmailAsync(string email, string subject, string body);
    }
}
