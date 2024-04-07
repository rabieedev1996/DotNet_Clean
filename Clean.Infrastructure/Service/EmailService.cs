using Clean.Application.Contract.Services;
using Clean.Infrastructure.ServiceImpls.SmsImpl.Mailzila;
using Clean.Infrastructure.ServiceImpls.SmsImpl.SMTP;

namespace Clean.Infrastructure.Service;

public class EmailService : IEmailService
{
    private ISMTPService _smtpService;
    private IMailzilaService _mailzilaService;

    public EmailService()
    {
        _smtpService = new SMTPService();
        _mailzilaService = new MailzilaService();
    }

    public async Task SendCode(string destination, string code)
    {
        string templateId = "1178086";
        _mailzilaService.MailZilaSendMail(destination, new List<string>() { destination }, templateId, "RegisterCode",
            new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("code", code)
            });
    }

    public async Task Send(string destination, string html, string subject)
    {
        await _smtpService.Send(destination, html, subject);
    }
}