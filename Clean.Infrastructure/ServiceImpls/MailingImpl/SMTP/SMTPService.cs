using System.Net;
using System.Net.Mail;

namespace Clean.Infrastructure.ServiceImpls.SmsImpl.SMTP;

public class SMTPService : ISMTPService
{
    public async Task Send(string destination, string html,string subject)
    {
        /// https://myaccount.google.com/lesssecureapps
        var mailAddress = "sample@gmail.com";
        var mailPassword = "****";
        MailMessage mail = new MailMessage(mailAddress, destination, subject, html);
        mail.IsBodyHtml = true;
        SmtpClient client = new SmtpClient();
        client.Host = "smtp.googlemail.com";
        client.Port = 587;
        client.UseDefaultCredentials = false;
        client.DeliveryMethod = SmtpDeliveryMethod.Network;
        client.EnableSsl = true;
        client.Credentials = new NetworkCredential(mailAddress, mailPassword);
        try
        {
            client.Send(mail);
        }
        catch (Exception ex)
        { }
    }
}