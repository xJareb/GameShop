using System.Net.Mail;
using System.Net;
using GameShop.Data.Models;

namespace GameShop.Helper.Services
{
    public class EmailSender
    {
        public void Posalji(string to,List<Data.Models.Igrice> igrice)
        {
            string fromMail = "gameshop840@gmail.com";
            string fromPassword = "txejlvuoqunzzjaz";

            MailMessage message = new MailMessage();
            message.From = new MailAddress(fromMail);
            message.Subject = "Uživajte u kupnji! Ne zaboravite provjeriti ove dodatke!";
            message.To.Add(new MailAddress(to));
            string bodyContent = "<html><body>Dragi korisniče,<br>" +
            "<br>Hvala vam što ste izvršili kupovinu kod nas! Vaša narudžba je uspešno završena, a u nastavku se nalaze aktivacijski kodovi za kupljene igre:<br><br>";

            for(int i = 0; i < igrice.Count; i++)
            {
                bodyContent += $"<strong>{igrice[i].Naziv}: </strong>{TokenGenerator.Generate(7)}<br>";
            }
            bodyContent += "<br>Molimo vas da proverite svoj inbox (ili spam folder) kako biste pronašli kodove potrebne za aktivaciju.<br><br>" +
                   "Uživajte u igranju!<br><br>" +
                   "Ako imate bilo kakvih pitanja, slobodno nas kontaktirajte.<br></body></html>";
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
