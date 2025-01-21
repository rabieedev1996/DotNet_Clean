using Clean.Application.Contract.Services;
using Clean.Domain;
using Clean.Infrastructure.ServiceImpls.SmsImpl;
using Clean.Infrastructure.ServiceImpls.SMSImpl.FarazSMS;

namespace Clean.Infrastructure.Service;

public class SmsService : ISmsService
{
    private ISmsImpl _provider;

    public SmsService(Configs configs)
    {
        _provider = new FarazSmsService(configs);
    }
    public async Task Send(string dest, string text)
    {
        await _provider.Send(new List<string> { dest }, text);
    }

    public async Task SendCode(string dest, string code)
    {
        await _provider.SendCode(dest, code);
    }
}