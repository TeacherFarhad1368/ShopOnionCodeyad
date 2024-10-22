using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Domain.Enum
{
    public enum OrderStatus
    {
        پرداخت_نشده,
        پرداخت_شده,
        لغو_شده_توسط_مشتری,
        لغو_شده_توسط_ادمین,
        ارسال_شده
    }
}
