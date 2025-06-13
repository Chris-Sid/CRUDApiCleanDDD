using AutoMapper;
using MyApiProject.Domain.Entities.Activity;
using MyApiProject.Domain.Entities.Customers.GetCustomerDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApiProject.Domain.Entities.Customers
{
    public class CustomerProfile : Profile
    {
        public CustomerProfile()
        {
            CreateMap<Customer, CustomerGetDto>();
            CreateMap<ContactPerson, ContactPersonGetDto>();
            CreateMap<Note, NoteGetDto>();
            CreateMap<CommunicationLog, CommunicationLogGetDto>();  
            CreateMap<Opportunity, OpportunityGetDto>();      
            CreateMap<ActivityLog, ActivityLogGetDto>();
        }
    }
}
