using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;

namespace Clean.Application.Features.Sample.Commands.ValidateToken
{
    #region Handler
    public class ValidateTokenHandler : IRequestHandler<ValidateTokenInput, ValidateTokenVm>
    {
        UserContext _userContext;

        public ValidateTokenHandler(UserContext userContext)
        {
            _userContext = userContext;
        }

        public async Task<ValidateTokenVm> Handle(ValidateTokenInput request, CancellationToken cancellationToken)
        {
            return new ValidateTokenVm
            { 
                UserId=_userContext.UserId
            };
        }
    }
    #endregion

    #region Query
    public class ValidateTokenInput : IRequest<ValidateTokenVm>
    { }
    #endregion

    #region ViewModel
    public class ValidateTokenVm
    {
        public string UserId { get;  set; }
    }
    #endregion
}