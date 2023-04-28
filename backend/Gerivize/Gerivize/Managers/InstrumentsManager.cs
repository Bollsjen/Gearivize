using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using Gerivize.Models;
using Gerivize.Repositories;

namespace Gerivize.Managers
{
    public class InstrumentsManager
    {
        private readonly InstrumentRepository _instrumentRepository;

        public InstrumentsManager()
        {
            _instrumentRepository = new InstrumentRepository();
        }

        public void SendCalibrationDueEmails(string _message, User user)
        {

            MailMessage message = new MailMessage();
            message.To.Add(user.Email);
            message.Subject = $"Calibration due for instrument(s)";
            message.Body = $"<p>Hello {user.Name},</p> <p>The following instrument(s) are due for calibration soon:<br>{_message}</p>";
            message.From = new MailAddress("gearivize@gmail.com");
            message.IsBodyHtml = true;

            SmtpClient client = new SmtpClient("smtp.gmail.com");
            client.Port = 587;
            client.Credentials = new NetworkCredential("gearivize@gmail.com", "hvxqpuybmydpzemb");
            client.EnableSsl = true;

            client.Send(message);
        }
    }
}