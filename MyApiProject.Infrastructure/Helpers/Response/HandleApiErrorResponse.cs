using MyApiProject.Application.DTOs;
using MyApiProject.Infrastructure.Exceptions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MyApiProject.Infrastructure.Helpers.Response
{
    public class HandleApiErrorResponse
    {
        public static T ApiResponse<T>(HttpResponseMessage response, string responseContent, IDictionary<string, IEnumerable<string>> headers) where T : class
        {
            var problemDetails = JsonConvert.DeserializeObject<ProblemDetails>(responseContent);

            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    var successResponse = JsonConvert.DeserializeObject<T>(responseContent);
                    if (successResponse == null)
                    {
                        throw new ApiException("Successful response but the content was null.",
                            (int)response.StatusCode,
                            responseContent,
                            headers,
                            null);
                    }
                    return successResponse;

                case HttpStatusCode.BadRequest:
                    var validationDetails = JsonConvert.DeserializeObject<ValidationProblemDetails>(responseContent);
                    if (validationDetails == null)
                    {
                        throw new ApiException("Bad Request",
                            (int)response.StatusCode,
                            responseContent,
                            headers,
                            null);
                    }
                    throw new ApiException<ValidationProblemDetails>("Bad Request",
                        (int)response.StatusCode,
                        responseContent,
                        headers,
                        validationDetails,
                        null);

                case HttpStatusCode.Unauthorized:
                    if (problemDetails == null)
                    {
                        throw new ApiException("Unauthorized",
                            (int)response.StatusCode,
                            responseContent,
                            headers,
                            null);
                    }
                    throw new ApiException<ProblemDetails>("Unauthorized",
                        (int)response.StatusCode,
                        responseContent,
                        headers,
                        problemDetails,
                        null);

                case HttpStatusCode.Forbidden:
                    if (problemDetails == null)
                    {
                        throw new ApiException("Forbidden",
                            (int)response.StatusCode,
                            responseContent,
                            headers,
                            null);
                    }
                    throw new ApiException<ProblemDetails>("Forbidden",
                        (int)response.StatusCode,
                        responseContent,
                        headers,
                        problemDetails,
                        null);

                case HttpStatusCode.InternalServerError:
                    if (problemDetails == null)
                    {
                        throw new ApiException("Server Error",
                            (int)response.StatusCode,
                            responseContent,
                            headers,
                            null);
                    }
                    throw new ApiException<ProblemDetails>("Server Error",
                        (int)response.StatusCode,
                        responseContent,
                        headers,
                        problemDetails,
                        null);

                default:
                    throw new ApiException($"Unexpected HTTP status code: {(int)response.StatusCode}.",
                        (int)response.StatusCode,
                        responseContent,
                        headers,
                        null);
            }
        }
    }
}
