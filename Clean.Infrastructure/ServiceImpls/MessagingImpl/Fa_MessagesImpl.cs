using Clean.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clean.Infrastructure.ServiceImpls.MessagingImpl
{
    public class Fa_MessagesImpl : IMessagesImpl
    {
        public string GetMessage(MessageCodes code)
        {
            switch (code)
            {
                case MessageCodes.STATUS_SUCCESS: return "عملیات با موفقیت انجام شد."; break;
                case MessageCodes.STATUS_EXCEPTION: return "عملیات با خطا مواجه شد."; break;
                case MessageCodes.MESSAGE_REQUIRED_PARAM: return "فیلد اجباری وارد نشده است."; break;
                case MessageCodes.STATUS_VALIDATION_ERROR: return "داده های ورودی صحیح نیست."; break;
                default: return "وضعیت نامشخص";
            }
        }
    }
}
