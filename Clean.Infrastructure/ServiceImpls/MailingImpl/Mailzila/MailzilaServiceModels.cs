namespace Clean.Infrastructure.ServiceImpls.SmsImpl.Mailzila;

public class MailzilaResponseData
{
    public string transactionid { get; set; }
    public string messageid { get; set; }
}

public class MailzilaResponse
{
    public bool success { get; set; }
    public string error { get; set; }
    public MailzilaResponseData data { get; set; }
}