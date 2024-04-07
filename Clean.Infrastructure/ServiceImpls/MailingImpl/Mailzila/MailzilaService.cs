using Newtonsoft.Json;
using RestSharp;

namespace Clean.Infrastructure.ServiceImpls.SmsImpl.Mailzila;

public class MailzilaService:IMailzilaService
{
    private string _mailZilaApiKey;

    public MailzilaService()
    {
        _mailZilaApiKey = "*****";
    }
    public MailzilaResponse MailZilaSendMail(string from, List<string> to, string templateId, string subject = "", List<KeyValuePair<string, string>> Values = null, bool isTransactional = true)
    {
        string url = "https://api.elasticemail.com/v2/email/send?" +
                     $"apikey={_mailZilaApiKey}&" +
                     $"From={from}&" +
                     $"FromName=IPLA&" +
                     $"To={string.Join(",", to)}&" +
                     $"template={templateId}&" +
                     $"subject={subject}&" +
                     $"isTransactional={isTransactional}&";
        foreach (var item in Values)
        {
            url += $"merge_{item.Key}={item.Value}&";
        }
        url = url.TrimEnd('&');
        var request = new RestRequest(url,Method.Get);
        RestResponse response = request.Execute();
        var jsonResponse = response.Content;
        return JsonConvert.DeserializeObject<MailzilaResponse>(jsonResponse);
    }
}