using ClinicLogicLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MimeKit;
using System.Configuration;
using Microsoft.Extensions.Configuration;
namespace ClinicLogicLayer
{
    public class clsEmailSendingService : IEmailSender
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

    }
}
