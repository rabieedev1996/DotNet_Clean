using Clean.Domain.Enums;

namespace Clean.Domain;

public class Configs
{

    public SQlConfigs SQlConfigs { get; set; }
    public MongoConfigs MongoConfigs { get; set; }
    public SMSConfigs SMSConfigs { get; set; }
    public MailSMTPConfigs MailSMTPConfigs { get; set; }
    public AmazonStorageConfigs AmazonStorageConfigs { get; set; }
    public string MailZilaToken { get; set; }
    public OSTYPE OSTYPE { get; set; }
    public TokenConfigs TokenConfigs { get; set; }
}

public class SQlConfigs
{
    public string ConnectionString { get; set; }
}

public class MongoConfigs
{
    public string ConnectionString { get; set; }
    public string Database { get; set; }
}

public class SMSConfigs
{
    public FarazSmsConfigs FarazSmsConfigs { get; set; }
    public SMSIRConfigs SMSIRConfigs { get; set; }
}

public class FarazSmsConfigs
{
    public string BaseUrl { get; set; }
    public string Sender { set; get; }
    public string Token { set; get; }
    public string TemplateId { set; get; }
}

public class SMSIRConfigs
{
    public string Token { get; set; }
    public string BaseUrl { get; set; }
    public long TemplateId { get; set; }
    public string Sender { get; set; }
    public string PositionVariable { set; get; }
}

public class MailSMTPConfigs
{
    public string MailAddress { set; get; }
    public string Password { set; get; }
    public string Host { set; get; }
    public int Port { set; get; }
}

public class AmazonStorageConfigs
{
    public string AmazonAccessKey { set; get; }
    public string AmazonSecretKey { set; get; }
    public string AmazonBucketName { set; get; }
    public string AmazonEndPoint { set; get; }
}
public class TokenConfigs
{
    public string Key { set; get; }
    public string Issuer { set; get; }
    public string Audience { set; get; }
    public string SecurityAlghorithm { set; get; }
}