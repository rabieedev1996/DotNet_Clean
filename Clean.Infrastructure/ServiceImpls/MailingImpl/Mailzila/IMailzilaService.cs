namespace Clean.Infrastructure.ServiceImpls.SmsImpl.Mailzila;

public interface IMailzilaService
{
    MailzilaResponse MailZilaSendMail(string from, List<string> to, string templateId, string subject = "",
        List<KeyValuePair<string, string>> Values = null, bool isTransactional = true);
}