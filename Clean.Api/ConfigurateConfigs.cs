using Clean.Domain;
using Clean.Domain.Enums;

namespace Clean.Api;

public class Configuration
{
    public static Configs ConfigureConfigs(IConfiguration configuration, bool isDevelopment)
    {
        var configs = new Configs();

        if (isDevelopment)
        {
            // خواندن تنظیمات از secret storage
            configuration.GetSection("Configs").Bind(configs);
            
        }
        else
        {
            // خواندن تنظیمات از Environment Variables
            configs.JwtSecretKey = Environment.GetEnvironmentVariable("JWT_SECRET_KEY");
            configs.SQlConfigs = new SQlConfigs
            {
                ConnectionString = Environment.GetEnvironmentVariable("SQL_CONNECTION_STRING")
            };
            configs.MongoConfigs = new MongoConfigs
            {
                ConnectionString = Environment.GetEnvironmentVariable("MONGO_CONNECTION_STRING"),
                Database = Environment.GetEnvironmentVariable("MONGO_DATABASE")
            };
            configs.SMSConfigs = new SMSConfigs
            {
                FarazSmsConfigs = new FarazSmsConfigs
                {
                    BaseUrl = Environment.GetEnvironmentVariable("FARAZ_SMS_BASE_URL"),
                    Sender = Environment.GetEnvironmentVariable("FARAZ_SMS_SENDER"),
                    Token = Environment.GetEnvironmentVariable("FARAZ_SMS_TOKEN"),
                    TemplateId = Environment.GetEnvironmentVariable("FARAZ_SMS_TEMPLATE_ID")
                },
                SMSIRConfigs = new SMSIRConfigs
                {
                    Token = Environment.GetEnvironmentVariable("SMSIR_TOKEN"),
                    BaseUrl = Environment.GetEnvironmentVariable("SMSIR_BASE_URL"),
                    TemplateId = long.Parse(Environment.GetEnvironmentVariable("SMSIR_TEMPLATE_ID") ?? "0"),
                    Sender = Environment.GetEnvironmentVariable("SMSIR_SENDER"),
                    PositionVariable = Environment.GetEnvironmentVariable("SMSIR_POSITION_VARIABLE")
                }
            };
            configs.MailSMTPConfigs = new MailSMTPConfigs
            {
                MailAddress = Environment.GetEnvironmentVariable("MAIL_SMTP_MAIL_ADDRESS"),
                Password = Environment.GetEnvironmentVariable("MAIL_SMTP_PASSWORD"),
                Host = Environment.GetEnvironmentVariable("MAIL_SMTP_HOST"),
                Port = int.Parse(Environment.GetEnvironmentVariable("MAIL_SMTP_PORT") ?? "0")
            };
            configs.AmazonStorageConfigs = new AmazonStorageConfigs
            {
                AmazonAccessKey = Environment.GetEnvironmentVariable("AMAZON_ACCESS_KEY"),
                AmazonSecretKey = Environment.GetEnvironmentVariable("AMAZON_SECRET_KEY"),
                AmazonBucketName = Environment.GetEnvironmentVariable("AMAZON_BUCKET_NAME"),
                AmazonEndPoint = Environment.GetEnvironmentVariable("AMAZON_END_POINT")
            };
            configs.MailZilaToken = Environment.GetEnvironmentVariable("MAIL_ZILA_TOKEN");
            configs.OSTYPE = Enum.TryParse(Environment.GetEnvironmentVariable("OSTYPE"), out OSTYPE osType)
                ? osType
                : OSTYPE.LINUX;
        }

        return configs;
    }
}