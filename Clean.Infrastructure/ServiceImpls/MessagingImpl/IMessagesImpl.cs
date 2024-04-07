using Clean.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clean.Infrastructure.ServiceImpls.MessagingImpl
{
    public interface IMessagesImpl
    {
        string GetMessage(MessageCodes code);
       
    }
}
