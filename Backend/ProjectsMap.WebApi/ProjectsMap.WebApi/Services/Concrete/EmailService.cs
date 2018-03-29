using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;

namespace ProjectsMap.WebApi.Services.Concrete
{
    public class EmailService : IIdentityMessageService
    {
        public async Task SendAsync(IdentityMessage message)
        {
            await configSendGridasync(message);
        }

        // Use NuGet to install SendGrid (Basic C# client lib) 
        private async Task configSendGridasync(IdentityMessage message)
        {   
            MailMessage mail = new MailMessage("projectsmapapp@gmail.com", message.Destination);
            SmtpClient client = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Credentials = new System.Net.NetworkCredential("projectsmapapp@gmail.com", "projectsmap"),
                Timeout = 10000,
            };
            mail.Subject = message.Subject;
            mail.Body = message.Body;

            using(client)
            {
                await client.SendMailAsync(mail);
            }
        }
    }
}