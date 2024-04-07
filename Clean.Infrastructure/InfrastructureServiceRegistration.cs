using AutoMapper;
using Clean.Application;
using Clean.Application.Common;
using Clean.Application.Contract.Services;
using Clean.Application.Contract.SQLDB;
using Clean.Domain.Enums;
using Clean.Infrastructure.Persistence;
using Clean.Infrastructure.Service;
using Clean.Infrastructure.ServiceImpls.MessagingImpl;
using Clean.Infrastructure.SQLRepositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
namespace Clean.Infrastructure;

public static class InfrastructureServiceRegistration
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,
        ConfigurationManager configurationManager)
    {
        //PostgreSql
       /* services.AddEntityFrameworkNpgsql().AddDbContext<CleanContext>(opt =>
            opt.UseNpgsql(configurationManager.GetConnectionString("CleanPostgresDB")));*/

        //Sql Server
        services.AddDbContext<CleanContext>(options => options.UseSqlServer(configurationManager.GetConnectionString("CleanSqlServer")));

        services.AddScoped(typeof(IEmailService), typeof(EmailService));
        services.AddScoped(typeof(ISmsService), typeof(SmsService));
        services.AddScoped(typeof(IReportService), typeof(ReportService));
        services.AddScoped(typeof(ILogService), typeof(LogService));
        services.AddScoped(typeof(ISqlSampleEntityRepository), typeof(SqlSampleEntityRepository));
        var osType = (OSTYPE)Enum.Parse(typeof(OSTYPE), configurationManager.GetSection("OSType").Value);
        services.AddTransient<IFileService>(s => new FileService(osType));
        services.AddTransient<IImageService>(s => new ImageService(osType));
        services.AddScoped(typeof(UserContext));
        services.AddScoped(typeof(DapperContext));
        services.AddScoped(typeof(ResponseGenerator));

        services.AddScoped(typeof(IMessageService), typeof(MessageService));
        services.AddScoped(typeof(En_MessagesImpl));
        services.AddScoped(typeof(Fa_MessagesImpl));


        return services;
    }
}