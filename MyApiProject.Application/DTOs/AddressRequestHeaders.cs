using Microsoft.AspNetCore.Mvc;
using MyApiProject.Application.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApiProject.Application.DTOs
{
    [ModelBinder(BinderType = typeof(AddressRequestHeadersBinder))]
    public class AddressRequestHeaders
    {
        [FromHeader(Name = "X-Authorization")]
        public string Authorization { get; set; }

        [FromHeader(Name = "X-RequestId")]
        public string RequestId { get; set; }

        [FromHeader(Name = "Accept-Language")]
        public string AcceptLanguage { get; set; }

        [FromHeader(Name = "X-EntityOwnerUserId")]
        public string EntityOwnerUserId { get; set; }

        [FromHeader(Name = "X-EntityOwnerCompanyId")]
        public string EntityOwnerCompanyId { get; set; }

        [FromHeader(Name = "X-DimensionCompany")]
        public string DimensionCompany { get; set; }

        [FromHeader(Name = "X-DimensionLocation")]
        public string DimensionLocation { get; set; }

        [FromHeader(Name = "X-DimensionBranch")]
        public string DimensionBranch { get; set; }

        [FromHeader(Name = "X-DimensionMake")]
        public string DimensionMake { get; set; }

        [FromHeader(Name = "X-DimensionMarketSegment")]
        public string DimensionMarketSegment { get; set; }

        [FromHeader(Name = "X-TimezoneOffset")]
        public string TimezoneOffset { get; set; } = "0";
    }
}
