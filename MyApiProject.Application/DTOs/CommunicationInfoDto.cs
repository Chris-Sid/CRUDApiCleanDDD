using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApiProject.Application.DTOs
{

    public partial class CommunicationInfoDto
    {

        [Newtonsoft.Json.JsonProperty("addresses", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public ICollection<AddressDto> Addresses { get; set; }

        [Newtonsoft.Json.JsonProperty("email", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string Email { get; set; }

        [Newtonsoft.Json.JsonProperty("mobileNum", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string MobileNum { get; set; }

        [Newtonsoft.Json.JsonProperty("fax", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string Fax { get; set; }

        [Newtonsoft.Json.JsonProperty("phoneNum", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string PhoneNum { get; set; }

        [Newtonsoft.Json.JsonProperty("phoneNum1", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string PhoneNum1 { get; set; }

        [Newtonsoft.Json.JsonProperty("phoneNum2", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string PhoneNum2 { get; set; }

        [Newtonsoft.Json.JsonProperty("homeFax", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string HomeFax { get; set; }

        [Newtonsoft.Json.JsonProperty("home", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string Home { get; set; }

        [Newtonsoft.Json.JsonProperty("homeMobile", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string HomeMobile { get; set; }

        [Newtonsoft.Json.JsonProperty("homeEmail", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string HomeEmail { get; set; }

        /// <summary>
        /// ### Home Website
        /// </summary>
        [Newtonsoft.Json.JsonProperty("homeWebsite", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string HomeWebsite { get; set; }

        /// <summary>
        /// ### Work
        /// </summary>
        [Newtonsoft.Json.JsonProperty("work", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string Work { get; set; }

        /// <summary>
        /// ### Work Fax
        /// </summary>
        [Newtonsoft.Json.JsonProperty("workFax", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string WorkFax { get; set; }

        /// <summary>
        /// ### Work Mobile
        /// </summary>
        [Newtonsoft.Json.JsonProperty("workMobile", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string WorkMobile { get; set; }

        /// <summary>
        /// ### Work Email
        /// </summary>
        [Newtonsoft.Json.JsonProperty("workEmail", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string WorkEmail { get; set; }

        /// <summary>
        /// ### Work Website
        /// </summary>
        [Newtonsoft.Json.JsonProperty("workWebsite", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string WorkWebsite { get; set; }

        /// <summary>
        /// ### Work Company Name
        /// </summary>
        [Newtonsoft.Json.JsonProperty("workCompanyName", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string WorkCompanyName { get; set; }

        /// <summary>
        /// ### Work Phone Assistant
        /// </summary>
        [Newtonsoft.Json.JsonProperty("workPhoneAssistant", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string WorkPhoneAssistant { get; set; }

        /// <summary>
        /// ### Work Phone Reception
        /// </summary>
        [Newtonsoft.Json.JsonProperty("workPhoneReception", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string WorkPhoneReception { get; set; }

        /// <summary>
        /// ### Facebook URL
        /// </summary>
        [Newtonsoft.Json.JsonProperty("facebookUrl", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string FacebookUrl { get; set; }

        /// <summary>
        /// ### LinkedIn URL
        /// </summary>
        [Newtonsoft.Json.JsonProperty("linkedinUrl", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string LinkedinUrl { get; set; }

        /// <summary>
        /// ### Twitter URL
        /// </summary>
        [Newtonsoft.Json.JsonProperty("twitterUrl", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string TwitterUrl { get; set; }

        /// <summary>
        /// ### Social No.
        /// </summary>
        [Newtonsoft.Json.JsonProperty("socialNo", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string SocialNo { get; set; }

    }
}
