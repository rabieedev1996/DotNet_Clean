using Clean.Domain;
using Clean.Infrastructure.ServiceImpls.SmsImpl;
using Newtonsoft.Json;
using RestSharp;

namespace Clean.Infrastructure.ServiceImpls.SMSImpl.FarazSMS;

public class FarazSmsService : ISmsImpl
{
    private string BaseUrl = "****";
    private string Sender = "****";
    private string Token = "****";
    private string TemplateId = "****";

    public FarazSmsService(Configs configs)
    {
        BaseUrl=configs.SMSConfigs.FarazSmsConfigs.BaseUrl;
        Sender = configs.SMSConfigs.FarazSmsConfigs.Sender;
        Token = configs.SMSConfigs.FarazSmsConfigs.Token;
        TemplateId = configs.SMSConfigs.FarazSmsConfigs.TemplateId;
    }
    public async Task Send(List<string> dest, string message)
    {
        var client = new RestClient();
        var request = new RestRequest($"{BaseUrl}/send/webservice/single", Method.Post);
        request.AddHeader("accept", "application/json");
        request.AddHeader("apikey", Token);
        request.AddHeader("Content-Type", "application/json");
        var body = new FarazSmsSendRequest()
        {
            Recipient = dest.ToArray(),
            Sender = "+983000505",
            Message = message
        };
        request.AddBody(body, ContentType.Json);
        RestResponse response = await client.ExecuteAsync(request);
        return;
    }

    public async Task SendCode(string dest, string code)
    {
        var client = new RestClient();
        var request = new RestRequest($"{BaseUrl}/pattern/normal/send", Method.Post);
        request.AddHeader("accept", "*/*");
        request.AddHeader("apikey", Token);
        request.AddHeader("Content-Type", "application/json");
        var body = new FarazSmsSendTemplateRequest
        {
            Code = code,
            Sender = Sender,
            Recipient = dest,
            Variable =new FarazSmsSendTemplateVariable
            {
                VerificationCode = code
            }
        };
        request.AddBody(body, ContentType.Json);
        RestResponse response = await client.ExecuteAsync(request);
        return;
    }
}