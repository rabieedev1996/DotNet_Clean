using Newtonsoft.Json;

namespace Clean.Infrastructure.ServiceImpls.SMSImpl.SMSIR;


public partial class SMSIRSendTemplateRequest
{
    [JsonProperty("mobile")]
    public string Mobile { get; set; }

    [JsonProperty("templateId")]
    public long TemplateId { get; set; }

    [JsonProperty("parameters")]
    public SMSIRSendTemplateParameter[] Parameters { get; set; }
}

public partial class SMSIRSendTemplateParameter
{
    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("value")]
    public string Value { get; set; }
}

public partial class SMSIRSendResponse
{
    [JsonProperty("status")]
    public long Status { get; set; }

    [JsonProperty("message")]
    public string Message { get; set; }

    [JsonProperty("data")]
    public SMSIRSendReponseData Data { get; set; }
}

public partial class SMSIRSendReponseData
{
    [JsonProperty("messageId")]
    public long MessageId { get; set; }

    [JsonProperty("cost")]
    public long Cost { get; set; }
}
public partial class SMSIRSendRequest
{
    [JsonProperty("lineNumber")]
    public string LineNumber { get; set; }

    [JsonProperty("messageText")]
    public string MessageText { get; set; }

    [JsonProperty("mobiles")]
    public string[] Mobiles { get; set; }
}