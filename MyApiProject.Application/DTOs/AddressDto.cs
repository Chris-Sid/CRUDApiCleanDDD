using MyApiProject.Domain.Entities;
using MyApiProject.Domain.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApiProject.Application.DTOs
{
    public partial class AddressDto
    {
        [JsonProperty("addressId", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public DmsIdentifier AddressId { get; set; }

        [JsonProperty("businessPartnerId", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public DmsIdentifier BusinessPartnerId { get; set; }

        [JsonProperty("contactPersonId", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public DmsIdentifier ContactPersonId { get; set; }

        [JsonProperty("street", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public string Street { get; set; }

        [JsonProperty("block", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public string Block { get; set; }

        [JsonProperty("zipCode", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public string ZipCode { get; set; }

        [JsonProperty("city", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public string City { get; set; }

        [JsonProperty("county", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public string County { get; set; }

        [JsonProperty("state", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public string State { get; set; }

        [JsonProperty("streetNo", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public string StreetNo { get; set; }

        [JsonProperty("longitude", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public string Longitude { get; set; }

        [JsonProperty("latitude", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public string Latitude { get; set; }

        [JsonProperty("federalTaxId", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public string FederalTaxId { get; set; }

        [JsonProperty("taxCode", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public string TaxCode { get; set; }

        [JsonProperty("buildingFloorRoom", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public string BuildingFloorRoom { get; set; }

        [JsonProperty("countryId", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public DmsIdentifier CountryId { get; set; }

        [JsonProperty("placeType", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
        public PlaceTypeEnum PlaceType { get; set; }

        [JsonProperty("addressType", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
        public AddressTypeEnum AddressType { get; set; }

        [JsonProperty("isDefault", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public bool? IsDefault { get; set; }

        [JsonProperty("altLanguageStreet", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public string AltLanguageStreet { get; set; }

        [JsonProperty("altLanguageCity", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public string AltLanguageCity { get; set; }

        [JsonProperty("altLanguageStateDescription", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public string AltLanguageStateDescription { get; set; }

        [JsonProperty("altLanguageBlock", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public string AltLanguageBlock { get; set; }

        [JsonProperty("altLanguageBuildingFloorRoom", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public string AltLanguageBuildingFloorRoom { get; set; }

    }
}
