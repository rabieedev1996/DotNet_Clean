using System.Net;
using System.Net.Mail;
using Clean.Domain;

namespace Clean.Infrastructure.ServiceImpls.SmsImpl.SMTP;

public class SMTPService : ISMTPService
{
    string _mailAddress;
    string _mailPassword;
    string _host;
    int _port;

    public SMTPService(Configs configs)
    {
        _mailAddress = configs.MailSMTPConfigs.MailAddress;
        _mailPassword = configs.MailSMTPConfigs.Password;
        _port = configs.MailSMTPConfigs.Port;
        _host = configs.MailSMTPConfigs.Host;
    }

    public async Task Send(string destination, string html, string subject)
    {
        /// https://myaccount.google.com/lesssecureapps
        var mailAddress = _mailAddress;
        var mailPassword = mailAddress;
        MailMessage mail = new MailMessage(mailAddress, destination, subject, html);
        mail.IsBodyHtml = true;
        SmtpClient client = new SmtpClient();
        client.Host = _host;
        client.Port = _port;
        client.UseDefaultCredentials = false;
        client.DeliveryMethod = SmtpDeliveryMethod.Network;
        client.EnableSsl = true;
        client.Credentials = new NetworkCredential(mailAddress, mailPassword);
        try
        {
            client.Send(mail);
        }
        catch (Exception ex)
        {
        }
    }
}