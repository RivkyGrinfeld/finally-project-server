using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Bl.Services
{


public class EmailService
    {
        private string _smtpServer = "smtp.gmail.com"; // כתובת ה-SMTP של ספק המייל שלך
        private int _smtpPort = 587;  // פורט SMTP
        private string _smtpUser = "pnina7560@gmail.com";  // כתובת המייל של המחשב
        private string _smtpPass = "p213350044";  // הסיסמה למייל

        public void SendVerificationEmail(string toEmail, string verificationLink)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("MyApp", _smtpUser));
            message.To.Add(new MailboxAddress("", toEmail));
            message.Subject = "אימות חשבון";

            var bodyBuilder = new BodyBuilder();
            bodyBuilder.HtmlBody = $"שלום,<br> אנא לחץ על הקישור הבא כדי לאמת את חשבונך: <a href='{verificationLink}'>אמת את החשבון</a>";

            message.Body = bodyBuilder.ToMessageBody();

            using (var client = new SmtpClient())
            {
                client.Connect(_smtpServer, _smtpPort, SecureSocketOptions.None); // אפשר לשים true אם תומכים ב-SSL
                client.Authenticate(_smtpUser, _smtpPass);
                client.Send(message);
                client.Disconnect(true);
            }
        }
    }
}
