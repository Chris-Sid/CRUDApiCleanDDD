using MyApiProject.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApiProject.Infrastructure.Helpers.Response
{
    public class HeadersHelper
    {
        /// <summary>
        /// Adds a header to the HttpRequestMessage if the header value is not null or empty.
        /// </summary>
        /// <param name="request">The HttpRequestMessage to which the header will be added.</param>
        /// <param name="headerName">The name of the header.</param>
        /// <param name="headerValue">The value of the header.</param>
        public static void AddHeaderIfNotEmpty(HttpRequestMessage request, string headerName, string headerValue)
        {
            if (!string.IsNullOrEmpty(headerValue))
            {
                request.Headers.TryAddWithoutValidation(headerName, headerValue);
            }
        }

        public static void ConfigureHeaders(HttpRequestMessage request, AddressRequestHeaders headers)
        {
            request.Headers.TryAddWithoutValidation("X-Authorization", headers.Authorization);
            request.Headers.TryAddWithoutValidation("X-RequestId", headers.RequestId);
            AddHeaderIfNotEmpty(request, "X-DimensionCompany", headers.DimensionCompany);
            AddHeaderIfNotEmpty(request, "X-DimensionLocation", headers.DimensionLocation);
            AddHeaderIfNotEmpty(request, "X-DimensionBranch", headers.DimensionBranch);
            AddHeaderIfNotEmpty(request, "Accept-Language", headers.AcceptLanguage);
            AddHeaderIfNotEmpty(request, "X-EntityOwnerUserId", headers.EntityOwnerUserId);
            AddHeaderIfNotEmpty(request, "X-EntityOwnerCompanyId", headers.EntityOwnerCompanyId);
            AddHeaderIfNotEmpty(request, "X-DimensionMake", headers.DimensionMake);
            AddHeaderIfNotEmpty(request, "X-DimensionMarketSegment", headers.DimensionMarketSegment);
            AddHeaderIfNotEmpty(request, "X-TimezoneOffset", headers.TimezoneOffset);
        }
    }
}
