namespace Clean.Infrastructure.ServiceImpls.SmsImpl.SMTP;

public interface ISMTPService
{
    Task Send(string destination,string html,string subject);
}