using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;

namespace Clean.Application.Features.Sample.Commands.FirstService
{
    public class FirstServiceCommand:IRequest<FirstServiceVm>
    {
        public int Param1 { set; get; }
        public string RequiredParam { set; get; }
    }
}