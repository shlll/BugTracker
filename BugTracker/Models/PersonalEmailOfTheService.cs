﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;

namespace BugTracker.Models
{
    public class PersonalEmailOfTheService
    {
        public async Task SendAsync(MailMessage message)
        {
            var account = WebConfigurationManager.AppSettings["username"];
            var password = WebConfigurationManager.AppSettings["password"];
            var host = WebConfigurationManager.AppSettings["host"];
            int port = Convert.ToInt32(WebConfigurationManager.AppSettings["port"]);


            using (var smtp = new SmtpClient()
            {
                Host = host,
                Port = port,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(account, password)
            })
            {
                try
                {
                    await smtp.SendMailAsync(message);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    await Task.FromResult(0);

                }
            };
        }

        public void Send(MailMessage message)
        {
            var account = WebConfigurationManager.AppSettings["username"];
            var password = WebConfigurationManager.AppSettings["password"];
            var host = WebConfigurationManager.AppSettings["host"];
            int port = Convert.ToInt32(WebConfigurationManager.AppSettings["port"]);
            using (var smtp = new SmtpClient()
            {
                Host = host,
                Port = port,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(account, password)
            })
            {
                try
                {
                    smtp.Send(message);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            };
        }
    }
}