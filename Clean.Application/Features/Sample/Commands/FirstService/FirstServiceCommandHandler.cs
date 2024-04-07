using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;

namespace Clean.Application.Features.Sample.Commands.FirstService
{
    public class FirstServiceCommandHandler : IRequestHandler<FirstServiceCommand, FirstServiceVm>
    {
        public async Task<FirstServiceVm> Handle(FirstServiceCommand request, CancellationToken cancellationToken)
        {
            return new FirstServiceVm
            {
                Param1 = "Hello"
            };
        }
    }
}