using Clean.Api;
using Clean.Api.Filters;
using Clean.API.Filters;
using Clean.Application;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Clean.Infrastructure;
using Scalar.AspNetCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

//In the Debug environment, for security reasons, set these configs in the Secret Storage.
//In the Release environment, for security reasons, set these configs in the Environment Variables.
//builder.Configuration.AddJsonFile("SampleConfigs.json", optional: true, reloadOnChange: true);

bool isDevelopment = builder.Environment.IsDevelopment();

var configs = Configuration.ConfigureConfigs(builder.Configuration, isDevelopment);
builder.Services.AddSingleton(configs);


builder.Services.AddControllers(o =>
{
    o.Filters.Add(typeof(HttpResponseExceptionFilter));
    o.Filters.Add(typeof(FillUserContextFilter));
    o.Filters.Add(typeof(LoggingFilter));
});
builder.Services.Configure<ApiBehaviorOptions>(options => { options.SuppressModelStateInvalidFilter = true; });
builder.Services.AddMvc(setupAction => { }).AddJsonOptions(jsonOptions =>
{
    jsonOptions.JsonSerializerOptions.PropertyNamingPolicy = null;
});

builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration, configs);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(x =>
    {
        x.RequireHttpsMetadata = false;
        x.TokenValidationParameters = new TokenValidationParameters
        {
            RequireExpirationTime = false,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("token key")),
            ValidateAudience = false,
            // Ensure the token was issued by a trusted authorization server (default true):
            ValidateIssuer = false,
            ValidIssuer = "",
            ValidAudience = "",
        };
    })
    .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme,
        options => builder.Configuration.Bind("CookieSettings", options));

builder.Services.AddOpenApi();


var app = builder.Build();
app.MapControllers();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapScalarApiReference();
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();



app.Run();