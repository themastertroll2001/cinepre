using System.Net;
using System.Net.Mail;

namespace cine1.Data
{
    public class Email2
    {
        public void Enviar(string correo, string token)
        {
            Correo(correo, token);
        }

        void Correo(string correo_receptor, string token)
        {
            string correo_emisor = "barberialevy@gmail.com";
            string clave_emisor = "malima3018";

            MailAddress receptor = new(correo_receptor);
            MailAddress emisor = new(correo_emisor);

            MailMessage email = new MailMessage(emisor, receptor);
            email.Subject = "CinePre Recuperacion Cuenta";
            email.Body = "Para Recuperar su cuenta, haga clic en el siguiente enlace: https://localhost:44361/Cuenta/Token2?valor=" + token;

            SmtpClient smtp = new();
            smtp.Host = "smtp.office365.com";
            smtp.Port = 587;
            smtp.Credentials = new NetworkCredential(correo_emisor, clave_emisor);
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.EnableSsl = true;

            try
            {
                smtp.Send(email);
            }
            catch (System.Exception)
            {
                throw;
            }
        }
    }
}
