using Clean.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clean.Infrastructure.ServiceImpls.MessagingImpl
{
    public class En_MessagesImpl : IMessagesImpl
    {
        public string GetMessage(MessageCodes code)
        {
            switch (code)
            {
                case MessageCodes.STATUS_SUCCESS: return "Success Operation.";
                case MessageCodes.STATUS_EXCEPTION: return "An Exception occurred.";
                case MessageCodes.MESSAGE_REQUIRED_PARAM: return "Required Field Is Empety.";
                case MessageCodes.STATUS_VALIDATION_ERROR: return "Entered Data Is Not Valid.";

                default: return "Unknown Status";
            }
        }
    }
}
