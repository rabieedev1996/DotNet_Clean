using Newtonsoft.Json;

namespace Clean.Infrastructure.ServiceImpls.SMSImpl.FarazSMS;

public class FarazSmsSendRequest
{
    [JsonProperty("recipient")]
    public string[] Recipient { get; set; }

    [JsonProperty("sender")]
    public string Sender { get; set; }

    [JsonProperty("message")]
    public string Message { get; set; }
}

public partial class FarazSmsResponse
{
    [JsonProperty("status")]
    public string Status { get; set; }

    [JsonProperty("code")]
    public long Code { get; set; }

    [JsonProperty("error_message")]
    public string ErrorMessage { get; set; }

    [JsonProperty("data")]
    public FarazSmsResponseData Data { get; set; }
}

public partial class FarazSmsResponseData
{
    [JsonProperty("message_id")]
    public long MessageId { get; set; }
}

public partial class FarazSmsSendTemplateRequest
{
    [JsonProperty("code")]
    public string Code { get; set; }

    [JsonProperty("sender")]
    public string Sender { get; set; }

    [JsonProperty("recipient")]
    public string Recipient { get; set; }

    [JsonProperty("variable")]
    public FarazSmsSendTemplateVariable Variable { get; set; }
}

public partial class FarazSmsSendTemplateVariable
{
    [JsonProperty("verification-code")]
    public string VerificationCode { get; set; }
}