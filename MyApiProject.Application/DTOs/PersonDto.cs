using MyApiProject.Domain.Entities;
using MyApiProject.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApiProject.Application.DTOs
{
    /// <summary>
    /// ## Person DTO
    /// </summary>

    public partial class PersonDto
    {
        [Newtonsoft.Json.JsonProperty("id", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public DmsIdentifier Id { get; set; }

        [Newtonsoft.Json.JsonProperty("businessPartner", Required = Newtonsoft.Json.Required.Always)]
        [System.ComponentModel.DataAnnotations.Required]
        public DmsIdentifier BusinessPartner { get; set; } = new DmsIdentifier();

        /// <summary>
        /// ### First Name
        /// </summary>
        [Newtonsoft.Json.JsonProperty("firstName", Required = Newtonsoft.Json.Required.Always)]
        [System.ComponentModel.DataAnnotations.Required]
        public string FirstName { get; set; }

        /// <summary>
        /// ### Middle Name
        /// </summary>
        [Newtonsoft.Json.JsonProperty("middleName", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string MiddleName { get; set; }

        /// <summary>
        /// ### Last Name
        /// </summary>
        [Newtonsoft.Json.JsonProperty("surname", Required = Newtonsoft.Json.Required.Always)]
        [System.ComponentModel.DataAnnotations.Required]
        public string Surname { get; set; }

        [Newtonsoft.Json.JsonProperty("title", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public DmsIdentifier Title { get; set; }

        [Newtonsoft.Json.JsonProperty("communicationInfo", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public CommunicationInfoDto CommunicationInfo { get; set; }

        [Newtonsoft.Json.JsonProperty("paymentInfo", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public PaymentInfoDto PaymentInfo { get; set; }

        /// <summary>
        /// ### Age Range
        /// </summary>
        [Newtonsoft.Json.JsonProperty("ageRange", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string AgeRange { get; set; }

        /// <summary>
        /// ### City of Birth
        /// </summary>
        [Newtonsoft.Json.JsonProperty("cityOfBirth", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string CityOfBirth { get; set; }

        [Newtonsoft.Json.JsonProperty("gender", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public DmsIdentifier Gender { get; set; }

        [Newtonsoft.Json.JsonProperty("languageCode", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public DmsIdentifier LanguageCode { get; set; }

        [Newtonsoft.Json.JsonProperty("maritalStatus", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public DmsIdentifier MaritalStatus { get; set; }

        /// <summary>
        /// ### No Of Children
        /// </summary>
        [Newtonsoft.Json.JsonProperty("noOfChildren", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public int? NoOfChildren { get; set; }

        /// <summary>
        /// ### Pets
        /// </summary>
        [Newtonsoft.Json.JsonProperty("pets", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public int? Pets { get; set; }

        /// <summary>
        /// ### Position
        /// </summary>
        [Newtonsoft.Json.JsonProperty("position", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string Position { get; set; }

        [Newtonsoft.Json.JsonProperty("profession", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public DmsIdentifier Profession { get; set; }

        /// <summary>
        /// ### Salutation
        /// </summary>
        [Newtonsoft.Json.JsonProperty("salutation", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string Salutation { get; set; }

        /// <summary>
        /// ### Letter Salutation
        /// </summary>
        [Newtonsoft.Json.JsonProperty("letterSalutation", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string LetterSalutation { get; set; }

        /// <summary>
        /// ### Social No.
        /// </summary>
        [Newtonsoft.Json.JsonProperty("socialNo", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string SocialNo { get; set; }

        /// <summary>
        /// ### Vocative FirstName
        /// </summary>
        [Newtonsoft.Json.JsonProperty("vocativeFirstName", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string VocativeFirstName { get; set; }

        /// <summary>
        /// ### Vocative LastName
        /// </summary>
        [Newtonsoft.Json.JsonProperty("vocativeLastName", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string VocativeLastName { get; set; }

        /// <summary>
        /// ### Wedding Anniversary
        /// </summary>
        [Newtonsoft.Json.JsonProperty("weddingAnniversary", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public System.DateTimeOffset? WeddingAnniversary { get; set; }

        /// <summary>
        /// ### Birth Date
        /// </summary>
        [Newtonsoft.Json.JsonProperty("birthDate", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public System.DateTimeOffset? BirthDate { get; set; }

        /// <summary>
        /// ### Is Default
        /// </summary>
        [Newtonsoft.Json.JsonProperty("isDefault", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public bool? IsDefault { get; set; }

        [Newtonsoft.Json.JsonProperty("salesPersonCode", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public DmsIdentifier SalesPersonCode { get; set; }

        [Newtonsoft.Json.JsonProperty("emptyEmailReason", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [Newtonsoft.Json.JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
        public EmptyEmailReasonTypeEnum EmptyEmailReason { get; set; }

    }
}
