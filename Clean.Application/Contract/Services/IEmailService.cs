namespace Clean.Application.Contract.Services;

public interface IEmailService
{
    Task SendCode(string destination, string code);
    Task Send(string destination, string html,string subject);
}