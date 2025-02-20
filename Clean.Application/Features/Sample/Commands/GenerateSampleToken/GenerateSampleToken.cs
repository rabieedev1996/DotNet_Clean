using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Clean.Domain;
using Clean.Identity;
using FluentValidation;
using MediatR;

namespace Clean.Application.Features.Sample.Commands.GenerateSampleToken
{
    #region Handler
    public class GenerateSampleTokenHandler : IRequestHandler<GenerateSampleTokenInput, GenerateSampleTokenVm>
    {
        public Configs Configs;

        public GenerateSampleTokenHandler(Configs configs)
        {
            Configs = configs;
        }

        public async Task<GenerateSampleTokenVm> Handle(GenerateSampleTokenInput request, CancellationToken cancellationToken)
        {
            var token = IdentityUtility.GenerateToken(new TokenParams
            {
                SecurityAlgorithm=Configs.TokenConfigs.SecurityAlghorithm,
                Audience=Configs.TokenConfigs.Audience,
                Key=Configs.TokenConfigs.Key,
                Issuer=Configs.TokenConfigs.Issuer,
                UserId=Guid.NewGuid().ToString(),   
                TokenId=Guid.NewGuid().ToString(),  
                ExpireTime=TimeSpan.FromMinutes(30),
                Roles=new List<string> { "USER"},
                OtherClaims=new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("FULL_NAME","Mohammad Rabiee"),
                },
                Reason="ON_REGISTER"             
            });
            return new GenerateSampleTokenVm
            { 
             Token=token
            };
        }
    }
    #endregion

    #region Query
    public class GenerateSampleTokenInput : IRequest<GenerateSampleTokenVm>
    { }
    #endregion

    #region ViewModel
    public class GenerateSampleTokenVm
    {
        public string Token { get; set; }
    }
    #endregion
}