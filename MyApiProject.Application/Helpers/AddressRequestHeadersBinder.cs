using Microsoft.AspNetCore.Mvc.ModelBinding;
using MyApiProject.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApiProject.Application.Helpers
{
    public class AddressRequestHeadersBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            var headers = bindingContext.HttpContext.Request.Headers;
            var model = new AddressRequestHeaders
            {
                Authorization = headers["X-Authorization"],
                RequestId = headers["X-RequestId"],
                AcceptLanguage = headers["Accept-Language"],
                EntityOwnerUserId = headers["X-EntityOwnerUserId"],
                EntityOwnerCompanyId = headers["X-EntityOwnerCompanyId"],
                DimensionCompany = headers["X-DimensionCompany"],
                DimensionLocation = headers["X-DimensionLocation"],
                DimensionBranch = headers["X-DimensionBranch"],
                DimensionMake = headers["X-DimensionMake"],
                DimensionMarketSegment = headers["X-DimensionMarketSegment"],
                TimezoneOffset = headers["X-TimezoneOffset"].FirstOrDefault() ?? "0"
            };

            bindingContext.Result = ModelBindingResult.Success(model);
            return Task.CompletedTask;
        }
    }

}
