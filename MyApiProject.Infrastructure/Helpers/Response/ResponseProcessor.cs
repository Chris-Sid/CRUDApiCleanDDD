using MyApiProject.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using MyApiProject.Infrastructure.Exceptions;
using System.Threading;
using Newtonsoft.Json;

namespace MyApiProject.Infrastructure.Helpers.Response
{
    public static class ResponseProcessor
    {
        public static async Task<T> ProcessResponseAsync<T>(HttpResponseMessage response) where T : class
        {
            if (response.IsSuccessStatusCode)
            {
                // Deserialize the response content into the specified generic type
                var data = await response.Content.ReadFromJsonAsync<T>();
                if (data == null)
                {
                    throw new ApiException("Received null response.",
                        (int)response.StatusCode,
                        await response.Content.ReadAsStringAsync(),
                        response.Headers.ToDictionary(h => h.Key, h => h.Value),
                        null);
                }
                return data; // Return the deserialized object of type T
            }

            // Handle error responses
            var responseContent = await response.Content.ReadAsStringAsync();
            var headers = response.Headers.ToDictionary(h => h.Key, h => h.Value);

            return HandleApiErrorResponse.ApiResponse<T>(response, responseContent, headers);
        }
    }

}
