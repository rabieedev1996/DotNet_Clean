using System.Reflection;
using AutoMapper;
using Clean.Application.Behaviours;
using Clean.Application.Common;
using Clean.Application.ExceptionHandler;
using Clean.Application.Features.Sample.Commands.FirstService;
using Clean.Application.Mapping;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace Clean.Application;

public static class ApplicationServiceRegistration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
        services.AddMediatR(Assembly.GetExecutingAssembly());
        services.AddScoped(typeof(Logging));
        services.AddValidatorsFromAssemblyContaining<FirstServiceCommandValidator>();
        services.AddScoped<ApiResponseException>();
        services.AddScoped(provider =>
          new MapperConfiguration(cfg => { cfg.AddProfile(new MappingProfile()); }).CreateMapper());
        return services;
    }
}