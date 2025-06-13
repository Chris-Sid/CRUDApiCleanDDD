using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MyApiProject.Application.Interfaces;
using MyApiProject.Domain.Entities;
using MyApiProject.Domain.Entities.FollowUps;
using MyApiProject.Domain.Entities.Leads;
using MyApiProject.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApiProject.Infrastructure.Services.Leads
{
    public class LeadRepository : ILeadRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        public LeadRepository(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<Lead>> GetAllAsync()
        {
            return await _context.leads
                .Include(l => l.FollowUps) // Include related data if needed
                .ToListAsync();
        }

        public async Task<Lead> CreateAsync(LeadCreateDto dto)
        {
            var lead = new Lead
            {
                Id = Guid.NewGuid(),
                Source = dto.Source,
                ContactInfo = dto.ContactInfo,
                AssignedTo = dto.AssignedTo,
                Status = dto.Status,
                CreatedDate = dto.CreatedDate
            };
    
            _context.leads.Add(lead);
            await _context.SaveChangesAsync();
            var result = _mapper.Map<Lead>(lead);
            return result;
        }
    }
}
