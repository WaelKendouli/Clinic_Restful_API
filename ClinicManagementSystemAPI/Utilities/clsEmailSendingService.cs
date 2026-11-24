using ClinicManagementSystemAPI.Interfaces;
using DTOsLayer;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace ClinicManagementSystemAPI.Utilities
{
    public class clsEmailSendingService : ISendEmail
    {
        private readonly IConfiguration _config;
        public clsEmailSendingService(IConfiguration configuration)
        {
            _config = configuration;
        }

        public async Task SendAsync(string To, string subject, string htmlBody)
        {
            var message = new MimeMessage();
            message.From.Add(MailboxAddress.Parse(_config["Smtp:From"]));
            message.To.Add(MailboxAddress.Parse(To));
            message.Subject = subject;
            message.Body = new TextPart("html") { Text = htmlBody };

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync(_config["Smtp:Host"],
                    int.Parse(_config["Smtp:Port"]), true);
                await client.AuthenticateAsync(_config["Smtp:User"], _config["Smtp:Password"]);
                await client.SendAsync(message);
                await client.DisconnectAsync(true);
            }
        }

        public async Task SendAsync(EmailSenderDTO dto)
        {
            if (dto is null) throw new ArgumentNullException(nameof(dto));

            var message = new MimeMessage();

            
            var fromAddress = string.IsNullOrWhiteSpace(dto.from)
                ? _config["Smtp:From"]
                : dto.from;

            message.From.Add(MailboxAddress.Parse(fromAddress));
            message.To.Add(MailboxAddress.Parse(dto.to));
            message.Subject = dto.subject ?? string.Empty;

            
            message.Body = new TextPart("plain") { Text = dto.Body ?? string.Empty };

            using var client = new SmtpClient();
            await client.ConnectAsync(
                _config["Smtp:Host"],
                int.Parse(_config["Smtp:Port"]!),
                true);
            await client.AuthenticateAsync(_config["Smtp:User"], _config["Smtp:Password"]);
            await client.SendAsync(message);
            await client.DisconnectAsync(true);
        }
    }
}
