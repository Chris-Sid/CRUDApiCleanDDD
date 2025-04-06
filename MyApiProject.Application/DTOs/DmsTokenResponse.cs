using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApiProject.Application.DTOs
{
    /// <summary>
    /// ### DMS Session Response
    /// </summary>

    public partial class DmsTokenResponse
    {
        /// <summary>
        /// ### Session Token
        /// </summary>
        [Newtonsoft.Json.JsonProperty("accessToken", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string AccessToken { get; set; }

        /// <summary>
        /// ### Expires At
        /// </summary>
        [Newtonsoft.Json.JsonProperty("expiresAt", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public System.DateTimeOffset? ExpiresAt { get; set; }

    }
}
