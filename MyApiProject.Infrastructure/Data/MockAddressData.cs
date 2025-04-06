using MyApiProject.Application.DTOs;
using MyApiProject.Domain.Entities;
using MyApiProject.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApiProject.Infrastructure.Data
{
    public static class MockAddressData
    {
        public static readonly List<AddressDto> Addresses = new List<AddressDto>
    {
        new AddressDto
        {
            AddressId = new DmsIdentifier { InternalId = "int1", ExternalId = "ext1" },
            BusinessPartnerId = new DmsIdentifier { InternalId = Guid.NewGuid().ToString(), ExternalId = Guid.NewGuid().ToString() },
            ContactPersonId = new DmsIdentifier {InternalId = Guid.NewGuid().ToString(), ExternalId = Guid.NewGuid().ToString()},
            Street = "123 Mock St",
            Block = "Block A",
            ZipCode = "12345",
            City = "Mock City",
            County = "Mock County",
            State = "Mock State",
            StreetNo = "12",
            Longitude = "12.34",
            Latitude = "-56.78",
            FederalTaxId = "FT123",
            TaxCode = "TC456",
            BuildingFloorRoom = "Floor 3, Room 12",
            CountryId = new DmsIdentifier {InternalId = Guid.NewGuid().ToString(), ExternalId = Guid.NewGuid().ToString()},
            PlaceType = PlaceTypeEnum.Residential,
            AddressType = AddressTypeEnum.Primary,
            IsDefault = true,
            AltLanguageStreet = "Alt Mock St",
            AltLanguageCity = "Alt Mock City",
            AltLanguageStateDescription = "Alt Mock State Desc",
            AltLanguageBlock = "Alt Block A",
            AltLanguageBuildingFloorRoom = "Alt Floor 3, Room 12"
        },
        new AddressDto
        {
            AddressId = new DmsIdentifier {InternalId = "int2", ExternalId = "ext2"},
            BusinessPartnerId = new DmsIdentifier {InternalId = Guid.NewGuid().ToString(), ExternalId = Guid.NewGuid().ToString()},
            ContactPersonId = new DmsIdentifier {InternalId = Guid.NewGuid().ToString(), ExternalId = Guid.NewGuid().ToString()},
            Street = "456 Example Ave",
            Block = "Block B",
            ZipCode = "67890",
            City = "Example City",
            County = "Example County",
            State = "Example State",
            StreetNo = "45",
            Longitude = "98.76",
            Latitude = "-54.32",
            FederalTaxId = "FT456",
            TaxCode = "TC789",
            BuildingFloorRoom = "Floor 1, Room 5",
            CountryId = new DmsIdentifier {InternalId = Guid.NewGuid().ToString(), ExternalId = Guid.NewGuid().ToString()},
            PlaceType = PlaceTypeEnum.Commercial,
            AddressType = AddressTypeEnum.BillTo,
            IsDefault = false,
            AltLanguageStreet = "Alt Example Ave",
            AltLanguageCity = "Alt Example City",
            AltLanguageStateDescription = "Alt Example State Desc",
            AltLanguageBlock = "Alt Block B",
            AltLanguageBuildingFloorRoom = "Alt Floor 1, Room 5"
        },
        new AddressDto
        {
            AddressId = new DmsIdentifier {InternalId = "int3", ExternalId = "ext3"},
            BusinessPartnerId = new DmsIdentifier {InternalId = Guid.NewGuid().ToString(), ExternalId = Guid.NewGuid().ToString()},
            ContactPersonId = new DmsIdentifier {InternalId = Guid.NewGuid().ToString(), ExternalId = Guid.NewGuid().ToString()},
            Street = "789 Test Blvd",
            Block = "Block C",
            ZipCode = "54321",
            City = "Test City",
            County = "Test County",
            State = "Test State",
            StreetNo = "78",
            Longitude = "65.43",
            Latitude = "-12.34",
            FederalTaxId = "FT789",
            TaxCode = "TC012",
            BuildingFloorRoom = "Floor 2, Room 8",
            CountryId = new DmsIdentifier {InternalId = Guid.NewGuid().ToString(), ExternalId = Guid.NewGuid().ToString()},
            PlaceType = PlaceTypeEnum.Residential,
            AddressType = AddressTypeEnum.ShipTo,
            IsDefault = false,
            AltLanguageStreet = "Alt Test Blvd",
            AltLanguageCity = "Alt Test City",
            AltLanguageStateDescription = "Alt Test State Desc",
            AltLanguageBlock = "Alt Block C",
            AltLanguageBuildingFloorRoom = "Alt Floor 2, Room 8"
        }
    };
    }
}