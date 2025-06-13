using AutoMapper;
using MyApiProject.Domain.Entities;
using MyApiProject.Domain.Entities.Activity;
using MyApiProject.Domain.Entities.FollowUps;
using MyApiProject.Domain.Entities.Leads;
using MyApiProject.Domain.Entities.Opportunities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApiProject.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Opportunity, OpportunityDto>();
            CreateMap<FollowUp, FollowUpDto>();
            CreateMap<FollowUpCreateDto, FollowUp>();
            CreateMap<ActivityLogCreateDto, ActivityLog>();
            CreateMap<ActivityLog, ActivityLogDto>();

           
            CreateMap<OpportunityCreateDto, Opportunity>();
            CreateMap<Lead, LeadDto>();
            CreateMap<LeadCreateDto, Lead>();
        }
    }
}
