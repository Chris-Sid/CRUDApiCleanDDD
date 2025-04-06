using MyApiProject.Domain.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApiProject.Application.DTOs
{
    /// <summary>
    /// ### Payment Info DTO
    /// </summary>

    public partial class PaymentInfoDto
    {
        [JsonProperty("preferredCurrency", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public DmsIdentifier PreferredCurrency { get; set; }

        /// <summary>
        /// ### Federal Tax ID
        /// </summary>
        [Newtonsoft.Json.JsonProperty("federalTaxId", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string FederalTaxId { get; set; }

        /// <summary>
        /// ### Unified Federal Tax Id
        /// </summary>
        [Newtonsoft.Json.JsonProperty("unifiedFederalTaxId", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string UnifiedFederalTaxId { get; set; }

        /// <summary>
        /// ### Company Registration Number
        /// </summary>
        [Newtonsoft.Json.JsonProperty("companyRegistrationNumber", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string CompanyRegistrationNumber { get; set; }

        /// <summary>
        /// ### Tax Office
        /// </summary>
        [Newtonsoft.Json.JsonProperty("taxOffice", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string TaxOffice { get; set; }

        [Newtonsoft.Json.JsonProperty("vatGroup", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public DmsIdentifier VatGroup { get; set; }

        [Newtonsoft.Json.JsonProperty("paymentTermsCode", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public DmsIdentifier PaymentTermsCode { get; set; }

        [Newtonsoft.Json.JsonProperty("paymentTypeCode", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public DmsIdentifier PaymentTypeCode { get; set; }

        /// <summary>
        /// ###
        /// </summary>
        [Newtonsoft.Json.JsonProperty("customerTaxNumberList", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public ICollection<CustomerTaxNumber> CustomerTaxNumberList { get; set; }

    }
}
