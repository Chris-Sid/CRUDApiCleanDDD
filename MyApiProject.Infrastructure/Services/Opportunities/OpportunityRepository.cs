using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MyApiProject.Application.Interfaces;
using MyApiProject.Domain.Entities;
using MyApiProject.Domain.Entities.Activity;
using MyApiProject.Domain.Entities.Opportunities;
using MyApiProject.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApiProject.Infrastructure.Services.Opportunities
{
    public class OpportunityRepository : IOpportunityRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        public OpportunityRepository(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<Opportunity>> GetAllAsync()
        {
            return await _context.opportunities
                .Include(o => o.ActivityLogs) // Include related data if needed
                .ToListAsync();
        }

        public async Task<OpportunityDto> CreateAsync(OpportunityCreateDto dto)
        {
            var opportunity = new Opportunity
            {
                Id = Guid.NewGuid(),
                Title = dto.Title,
                EstimatedValue = dto.EstimatedValue,
                Stage = dto.Stage,
                RelatedCustomerId = dto.RelatedCustomerId,
                ActivityLogs = dto.ActivityLogs?.Select(a => new ActivityLog
                {
                    Id = Guid.NewGuid(),
                    Description = a.Description,
                    CreatedAt = a.CreatedAt
                }).ToList()
            };

            _context.opportunities.Add(opportunity);
            await _context.SaveChangesAsync();
            var resultDto = _mapper.Map<OpportunityDto>(opportunity);
            return resultDto;
        }
    }
}
