using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApiProject.Domain.Enums
{
    public enum MappingTypesEnum
    {

        [System.Runtime.Serialization.EnumMember(Value = @"CustomerId")]
        CustomerId = 0,

        [System.Runtime.Serialization.EnumMember(Value = @"AddressId")]
        AddressId = 1,

        [System.Runtime.Serialization.EnumMember(Value = @"ContactPersonId")]
        ContactPersonId = 2,

        [System.Runtime.Serialization.EnumMember(Value = @"VehicleId")]
        VehicleId = 3,

        [System.Runtime.Serialization.EnumMember(Value = @"SalesDocumentId")]
        SalesDocumentId = 4,

        [System.Runtime.Serialization.EnumMember(Value = @"PurchaseDocumentId")]
        PurchaseDocumentId = 5,

        [System.Runtime.Serialization.EnumMember(Value = @"InquiryId")]
        InquiryId = 6,

        [System.Runtime.Serialization.EnumMember(Value = @"ReservationId")]
        ReservationId = 7,

        [System.Runtime.Serialization.EnumMember(Value = @"ServiceDealId")]
        ServiceDealId = 8,

        [System.Runtime.Serialization.EnumMember(Value = @"ServiceJobId")]
        ServiceJobId = 9,

        [System.Runtime.Serialization.EnumMember(Value = @"Country")]
        Country = 10,

        [System.Runtime.Serialization.EnumMember(Value = @"Language")]
        Language = 11,

        [System.Runtime.Serialization.EnumMember(Value = @"Currency")]
        Currency = 12,

        [System.Runtime.Serialization.EnumMember(Value = @"VatGroup")]
        VatGroup = 13,

        [System.Runtime.Serialization.EnumMember(Value = @"TaxGroup")]
        TaxGroup = 14,

        [System.Runtime.Serialization.EnumMember(Value = @"Make")]
        Make = 15,

        [System.Runtime.Serialization.EnumMember(Value = @"Company")]
        Company = 16,

        [System.Runtime.Serialization.EnumMember(Value = @"Branch")]
        Branch = 17,

        [System.Runtime.Serialization.EnumMember(Value = @"Location")]
        Location = 18,

        [System.Runtime.Serialization.EnumMember(Value = @"Gender")]
        Gender = 19,

        [System.Runtime.Serialization.EnumMember(Value = @"BpClassification")]
        BpClassification = 20,

        [System.Runtime.Serialization.EnumMember(Value = @"AcademicTitle")]
        AcademicTitle = 21,

        [System.Runtime.Serialization.EnumMember(Value = @"VehicleStatus")]
        VehicleStatus = 22,

        [System.Runtime.Serialization.EnumMember(Value = @"VehicleClassificationStatus")]
        VehicleClassificationStatus = 23,

        [System.Runtime.Serialization.EnumMember(Value = @"VehicleAvailabilityStatus")]
        VehicleAvailabilityStatus = 24,

        [System.Runtime.Serialization.EnumMember(Value = @"VehicleType")]
        VehicleType = 25,

        [System.Runtime.Serialization.EnumMember(Value = @"VehicleFamily")]
        VehicleFamily = 26,

        [System.Runtime.Serialization.EnumMember(Value = @"VehicleBodyStyle")]
        VehicleBodyStyle = 27,

        [System.Runtime.Serialization.EnumMember(Value = @"VehicleFuelType")]
        VehicleFuelType = 28,

        [System.Runtime.Serialization.EnumMember(Value = @"VehicleExteriorColorGroup")]
        VehicleExteriorColorGroup = 29,

        [System.Runtime.Serialization.EnumMember(Value = @"VehicleInteriorTypeGroup")]
        VehicleInteriorTypeGroup = 30,

        [System.Runtime.Serialization.EnumMember(Value = @"VehicleInteriorColorGroup")]
        VehicleInteriorColorGroup = 31,

        [System.Runtime.Serialization.EnumMember(Value = @"VehicleGearType")]
        VehicleGearType = 32,

        [System.Runtime.Serialization.EnumMember(Value = @"VehicleEmissionClass")]
        VehicleEmissionClass = 33,

        [System.Runtime.Serialization.EnumMember(Value = @"VehicleEmissionSticker")]
        VehicleEmissionSticker = 34,

        [System.Runtime.Serialization.EnumMember(Value = @"VehicleEngineType")]
        VehicleEngineType = 35,

        [System.Runtime.Serialization.EnumMember(Value = @"PriceList")]
        PriceList = 36,

        [System.Runtime.Serialization.EnumMember(Value = @"PaymentMethod")]
        PaymentMethod = 37,

        [System.Runtime.Serialization.EnumMember(Value = @"PaymentTerms")]
        PaymentTerms = 38,

        [System.Runtime.Serialization.EnumMember(Value = @"MaritalStatus")]
        MaritalStatus = 39,

        [System.Runtime.Serialization.EnumMember(Value = @"DocumentType")]
        DocumentType = 40,

        [System.Runtime.Serialization.EnumMember(Value = @"DocumentStatus")]
        DocumentStatus = 41,

        [System.Runtime.Serialization.EnumMember(Value = @"SalesSegment")]
        SalesSegment = 42,

        [System.Runtime.Serialization.EnumMember(Value = @"InquirySource")]
        InquirySource = 43,

        [System.Runtime.Serialization.EnumMember(Value = @"InquiryChannel")]
        InquiryChannel = 44,

        [System.Runtime.Serialization.EnumMember(Value = @"User")]
        User = 45,

        [System.Runtime.Serialization.EnumMember(Value = @"MarketSegment")]
        MarketSegment = 46,

        [System.Runtime.Serialization.EnumMember(Value = @"SalesPerson")]
        SalesPerson = 47,

        [System.Runtime.Serialization.EnumMember(Value = @"FinancialGroup")]
        FinancialGroup = 48,

        [System.Runtime.Serialization.EnumMember(Value = @"FleetType")]
        FleetType = 49,

        [System.Runtime.Serialization.EnumMember(Value = @"FleetSegment")]
        FleetSegment = 50,

        [System.Runtime.Serialization.EnumMember(Value = @"ConsentCode")]
        ConsentCode = 51,

        [System.Runtime.Serialization.EnumMember(Value = @"FinancingType")]
        FinancingType = 52,

        [System.Runtime.Serialization.EnumMember(Value = @"ClosingReason")]
        ClosingReason = 53,

        [System.Runtime.Serialization.EnumMember(Value = @"ItemGroup")]
        ItemGroup = 54,

        [System.Runtime.Serialization.EnumMember(Value = @"LeadStatus")]
        LeadStatus = 55,

        [System.Runtime.Serialization.EnumMember(Value = @"Profession")]
        Profession = 56,

        [System.Runtime.Serialization.EnumMember(Value = @"Salutation")]
        Salutation = 57,

        [System.Runtime.Serialization.EnumMember(Value = @"SalesType")]
        SalesType = 58,

        [System.Runtime.Serialization.EnumMember(Value = @"ServiceContractSource")]
        ServiceContractSource = 59,

        [System.Runtime.Serialization.EnumMember(Value = @"ServiceContractType")]
        ServiceContractType = 60,

        [System.Runtime.Serialization.EnumMember(Value = @"IndustrialGroup")]
        IndustrialGroup = 61,

        [System.Runtime.Serialization.EnumMember(Value = @"JobCardType")]
        JobCardType = 62,

        [System.Runtime.Serialization.EnumMember(Value = @"JobCardSource")]
        JobCardSource = 63,

        [System.Runtime.Serialization.EnumMember(Value = @"JobCardChannel")]
        JobCardChannel = 64,

        [System.Runtime.Serialization.EnumMember(Value = @"JobCardStatus")]
        JobCardStatus = 65,

        [System.Runtime.Serialization.EnumMember(Value = @"JobLineSource")]
        JobLineSource = 66,

        [System.Runtime.Serialization.EnumMember(Value = @"JobLineStatus")]
        JobLineStatus = 67,

        [System.Runtime.Serialization.EnumMember(Value = @"Department")]
        Department = 68,

        [System.Runtime.Serialization.EnumMember(Value = @"CancellationReason")]
        CancellationReason = 69,

        [System.Runtime.Serialization.EnumMember(Value = @"JobQualification")]
        JobQualification = 70,

        [System.Runtime.Serialization.EnumMember(Value = @"ItemUoM")]
        ItemUoM = 71,

        [System.Runtime.Serialization.EnumMember(Value = @"ServiceAdvisor")]
        ServiceAdvisor = 72,

        [System.Runtime.Serialization.EnumMember(Value = @"Technician")]
        Technician = 73,

        [System.Runtime.Serialization.EnumMember(Value = @"BpLaborPriceGroup")]
        BpLaborPriceGroup = 74,

        [System.Runtime.Serialization.EnumMember(Value = @"VatBusinessGroup")]
        VatBusinessGroup = 75,

        [System.Runtime.Serialization.EnumMember(Value = @"VatProductGroup")]
        VatProductGroup = 76,

        [System.Runtime.Serialization.EnumMember(Value = @"BillingIndicatorCode")]
        BillingIndicatorCode = 77,

        [System.Runtime.Serialization.EnumMember(Value = @"ServiceJobLineCode")]
        ServiceJobLineCode = 78,

        [System.Runtime.Serialization.EnumMember(Value = @"LaborCondition")]
        LaborCondition = 79,

        [System.Runtime.Serialization.EnumMember(Value = @"ServiceJobTechnicianAllocationId")]
        ServiceJobTechnicianAllocationId = 80,

        [System.Runtime.Serialization.EnumMember(Value = @"SalesArea")]
        SalesArea = 81,

        [System.Runtime.Serialization.EnumMember(Value = @"Division")]
        Division = 82,

        [System.Runtime.Serialization.EnumMember(Value = @"SalesOrganization")]
        SalesOrganization = 83,

        [System.Runtime.Serialization.EnumMember(Value = @"PurchaseOrganization")]
        PurchaseOrganization = 84,

        [System.Runtime.Serialization.EnumMember(Value = @"SalesDistrict")]
        SalesDistrict = 85,

        [System.Runtime.Serialization.EnumMember(Value = @"SalesOffice")]
        SalesOffice = 86,

        [System.Runtime.Serialization.EnumMember(Value = @"SalesGroup")]
        SalesGroup = 87,

        [System.Runtime.Serialization.EnumMember(Value = @"CustomerGroup")]
        CustomerGroup = 88,

        [System.Runtime.Serialization.EnumMember(Value = @"CustomerProfile")]
        CustomerProfile = 89,

        [System.Runtime.Serialization.EnumMember(Value = @"ShippingCondition")]
        ShippingCondition = 90,

        [System.Runtime.Serialization.EnumMember(Value = @"SalesServiceOrderStatus")]
        SalesServiceOrderStatus = 91,

        [System.Runtime.Serialization.EnumMember(Value = @"SalesServiceJobStatus")]
        SalesServiceJobStatus = 92,

        [System.Runtime.Serialization.EnumMember(Value = @"SalesServiceType")]
        SalesServiceType = 93,

        [System.Runtime.Serialization.EnumMember(Value = @"CustomsCategory")]
        CustomsCategory = 94,

        [System.Runtime.Serialization.EnumMember(Value = @"GroupId")]
        GroupId = 95,

        [System.Runtime.Serialization.EnumMember(Value = @"ParentGroupId")]
        ParentGroupId = 96,

        [System.Runtime.Serialization.EnumMember(Value = @"CalendarCode")]
        CalendarCode = 97,

        [System.Runtime.Serialization.EnumMember(Value = @"InvoiceId")]
        InvoiceId = 98,

        [System.Runtime.Serialization.EnumMember(Value = @"BPIdentifier")]
        BPIdentifier = 99,

        [System.Runtime.Serialization.EnumMember(Value = @"BPTaxNumber")]
        BPTaxNumber = 100,

    }
}
