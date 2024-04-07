namespace Clean.Application.Contract.Services;

public interface ISmsService
{
     Task Send(string dest,string text);
     Task SendCode(string dest,string code);
}