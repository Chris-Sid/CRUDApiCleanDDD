using AutoMapper;
using MyApiProject.Domain.Entities.GetCustomerDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApiProject.Domain.Entities
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
