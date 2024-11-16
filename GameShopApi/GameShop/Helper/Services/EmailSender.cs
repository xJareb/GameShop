using System.Net.Mail;
using System.Net;
using GameShop.Data.Models;

namespace GameShop.Helper.Services
{
    public class EmailSender
    {
        public void SendEmail(string to,List<Data.Models.Games> games)
        {
            string fromMail = "gameshop840@gmail.com";
            string fromPassword = "txejlvuoqunzzjaz";

            MailMessage message = new MailMessage();
            message.From = new MailAddress(fromMail);
            message.Subject = "Enjoy your shopping! Don't forget to check out these plugins!";
            message.To.Add(new MailAddress(to));
            string bodyContent = "<html><body>Dear user,<br>" +
            "<br>Thank you for shopping with us! Your order has been successfully completed, and below are the activation codes for the purchased games:<br><br>";

            for(int i = 0; i < games.Count; i++)
            {
                bodyContent += $"<strong>{games[i].Name}: </strong>{TokenGenerator.Generate(7)}<br>";
            }
            bodyContent += "<br>Please check your inbox (or spam folder) to find the codes needed for activation.<br><br>" +
                   "Enjoy playing!<br><br>" +
                   "If you have any questions, feel free to contact us.<br></body></html>";
            message.Body = bodyContent;
            message.IsBodyHtml = true;

            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential(fromMail, fromPassword),
                EnableSsl = true
            };

            smtpClient.Send(message);
        }
    }
}
