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

        public void SendCalibrationDueEmails()
        {
            List<Instrument> instruments = _instrumentRepository.GetInstrumentsDueForCalibration(DateTime.Today);

            foreach (Instrument instrument in instruments)
            {
                MailMessage message = new MailMessage();
                message.To.Add("gearivize@gmail.com");
                message.Subject = $"Calibration due for instrument {instrument.ANumber}";
                message.Body = $"The calibration for instrument {instrument.ANumber} is due today.";

                SmtpClient client = new SmtpClient("smtp.gmail.com");
                client.Port = 587;
                client.Credentials = new NetworkCredential("gearivize@gmail.com", "hvxqpuybmydpzemb");
                client.EnableSsl = true;

                client.Send(message);
            }
        }
    }
}