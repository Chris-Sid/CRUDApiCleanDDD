using MyApiProject.Domain.Entities;
using MyApiProject.Domain.Entities.Opportunities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApiProject.Application.Interfaces
{
    public interface IOpportunityRepository
    {
        Task<List<Opportunity>> GetAllAsync();
        Task<OpportunityDto> CreateAsync(OpportunityCreateDto dto);
    }
}
