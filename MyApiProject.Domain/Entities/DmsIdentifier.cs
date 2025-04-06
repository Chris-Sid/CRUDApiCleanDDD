using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace MyApiProject.Domain.Entities
{

    public partial class DmsIdentifier
    {
        /// <summary>
        /// Internal Id (the DMS ID)
        /// </summary>
        [JsonProperty("internalId", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string InternalId { get; set; }

        /// <summary>
        /// External Id (the DSW/NMT ID)
        /// </summary>
        [Newtonsoft.Json.JsonProperty("externalId", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string ExternalId { get; set; }

    }
}
