using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApiProject.Domain.Enums
{
    /// <summary>
    /// ### Address Type  * BillTo = 0  * ShipTo = 1
    /// </summary>

    public enum AddressTypeEnum
    {

        [System.Runtime.Serialization.EnumMember(Value = @"BillTo")]
        BillTo = 0,

        [System.Runtime.Serialization.EnumMember(Value = @"ShipTo")]
        ShipTo = 1,
        Primary = 2,
    }
}
