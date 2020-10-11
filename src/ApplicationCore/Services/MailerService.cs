using ApplicationCore.Services.Interfaces;
using System;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using ApplicationCore.Configuration;

namespace ApplicationCore.Services
{
    public class MailerService : IMailerService
    {
        private readonly SmtpSettings _smtpSettings;

        public MailerService(IOptions<SmtpSettings> smtpSettings)
        {
            _smtpSettings = smtpSettings.Value;
        }

        public async Task SendEmailAsync(string email, string subject, string body)
        {
            try
            {
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress(_smtpSettings.SenderName, _smtpSettings.SenderEmail));
                message.To.Add(MailboxAddress.Parse(email));
                message.Subject = subject;
                message.Body = new TextPart("html")
                {
                    Text = body
                };

                using var client = new SmtpClient
                {
                    ServerCertificateValidationCallback = (s, c, h, e) => true
                };

                await client.ConnectAsync(_smtpSettings.Server, _smtpSettings.Port, false);

                await client.AuthenticateAsync(_smtpSettings.Username, _smtpSettings.Password);
                await client.SendAsync(message);
                await client.DisconnectAsync(true);
            }
            catch (Exception e)
            {
                throw new InvalidOperationException(e.Message);
            }
        }
    }
}
