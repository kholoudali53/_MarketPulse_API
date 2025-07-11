using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Store.G01.Core.Entities.Order
{
    public enum OrderStatus
    {
        [EnumMember(Value ="Pending")]
        Pending,
        [EnumMember(Value = "Pending Received")]
        PaymentReceived,
        [EnumMember(Value = "Pending Failed")]
        PaymentFailed

    }
}
