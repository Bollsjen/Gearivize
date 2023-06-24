using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Reflection;
using Gerivize.Models;
using Gerivize.Repositories;
using static System.Net.Mime.MediaTypeNames;

namespace Gerivize.Managers
{
    public class InstrumentsManager
    {
        private readonly InstrumentRepository _instrumentRepository;
        private ILogger _logger;

        public InstrumentsManager(ILogger<InstrumentsManager> logger)
        {
            _instrumentRepository = new InstrumentRepository();
            _logger = logger;
        }

        public void SendCalibrationDueEmails(string _message, User user)
        {
            try
            {
                MailMessage message = new MailMessage();
                message.To.Add(user.Email);
                message.Subject = $"Calibration due for instrument(s)";
                message.Body = $"<p>Hello {user.Name},</p> <p>The following instrument(s) are due for calibration soon:<br>{_message}</p>";
                message.Body = $"<!DOCTYPE html>\r\n<html>\r\n\r\n<head>\r\n    <meta charset=\"UTF-8\">\r\n    <title>Email Template</title>\r\n    <style>\r\n        table {{\r\n            border-collapse: collapse;\r\n            width: 100%;\r\n        }}\r\n\r\n        table.outer {{\r\n            border-collapse: collapse;\r\n            width: 60%;\r\n            margin: 0 auto;\r\n        }}\r\n\r\n        th,\r\n        td {{\r\n            padding: 8px;\r\n            text-align: left;\r\n        }}\r\n\r\n        th {{\r\n            background-color: #ADD8E6;\r\n        }}\r\n    </style>\r\n</head>\r\n\r\n<body style=\"margin: 0; padding: 0;\">\r\n    <table align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" class=\"outer\">\r\n        <tr>\r\n            <td>\r\n                <center><img src=\"https://bolls.dk/wp-content/uploads/brizy/imgs/logo-276x95x0x0x276x95x1612536827.png\"\r\n                        alt=\"Image\">\r\n                    <center>\r\n                        <p style=\"font-size: 18px; margin-bottom: 10px;\">Hello {user.Name}</p>\r\n                        <p style=\"font-size: 16px; line-height: 1.5;\">The following instrument(s) are due for\r\n                            calibration soon.</p></br>\r\n                    </center>{_message}\r\n\r\n                    <p style=\"font-size: 12px;\"> You've received this email because you're either marked as one of the\r\n                        responsible people for the instruments or no action was taken in time by the responsible people.\r\n                    </p>\r\n    </table>\r\n</body>\r\n\r\n</html>";
                message.From = new MailAddress("instrumenter@bolls.dk");
                message.IsBodyHtml = true;

                SmtpClient client = new SmtpClient("manic.bolls.lan");
                client.Port = 465;
                client.Credentials = new NetworkCredential("instrumenter@bolls.dk", "Maritime-Clapped-Italicize2");
                client.EnableSsl = true;

                client.Send(message);
            }
            catch(Exception ex)
            {
                _logger.LogInformation(ex.Message);
                _logger.LogInformation(ex.StackTrace);
            }
        }

        public bool ImageUpload(IFormFile formFile, string aNumber)
        {
            string outputPath = Environment.CurrentDirectory + @"\Public\Images\Instrument\";
            try
            {
                if (!Directory.Exists(outputPath))
                {
                    Directory.CreateDirectory(outputPath);
                }

                Guid fileId = Guid.NewGuid();
                string newFileName = fileId.ToString() + "-" + formFile.FileName + ".jpeg";

                using (FileStream fileStream = System.IO.File.Create(outputPath + newFileName))
                {
                    formFile.CopyTo(fileStream);
                    fileStream.Flush();                 
                }
                _instrumentRepository.saveImageToInstrument(newFileName, aNumber);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
                _logger.LogInformation(ex.StackTrace);
                return false;
            }
        }
    }
}