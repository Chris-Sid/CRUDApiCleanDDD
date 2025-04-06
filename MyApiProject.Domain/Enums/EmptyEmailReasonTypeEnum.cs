using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApiProject.Domain.Enums
{
    /// <summary>
    /// Gets or Sets EmptyEmailReasonType
    /// </summary>

    public enum EmptyEmailReasonTypeEnum
    {

        [System.Runtime.Serialization.EnumMember(Value = @"Unset")]
        Unset = 0,

        [System.Runtime.Serialization.EnumMember(Value = @"NotAvailable")]
        NotAvailable = 1,

        [System.Runtime.Serialization.EnumMember(Value = @"NotProvided")]
        NotProvided = 2,

    }
}
