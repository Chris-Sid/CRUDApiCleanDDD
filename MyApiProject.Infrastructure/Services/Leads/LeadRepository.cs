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

        public async Task<Lead?> GetByIdAsync(Guid id)
        {
            return await _context.leads
                .AsNoTracking()
                .FirstOrDefaultAsync(l => l.Id == id);
        }

        public async Task<Lead?> UpdateAsync(Guid id, UpdateLeadRequest request)
        {
            var lead = await _context.leads.FirstOrDefaultAsync(l => l.Id == id);
            if (lead is null) return null;

            // Compare against the version the client read, not the one just loaded
            _context.Entry(lead).Property(l => l.RowVersion).OriginalValue = request.RowVersion;

            lead.Source = request.Source;
            lead.ContactInfo = request.ContactInfo;
            lead.AssignedTo = request.AssignedTo;
            lead.Status = request.Status;

            try
            {
                await _context.SaveChangesAsync();
                return lead;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                var entry = ex.Entries.Single();
                var databaseValues = await entry.GetDatabaseValuesAsync();

                if (databaseValues is null)
                {
                    // Row was deleted by another user/process in the meantime
                    throw new LeadConcurrencyException(
                        "The lead no longer exists. It may have been deleted by another user.",
                        currentState: null,
                        attemptedChange: request);
                }

                var currentLead = (Lead)databaseValues.ToObject();

                throw new LeadConcurrencyException(
                    "The lead was modified by another user before your changes were saved.",
                    currentState: currentLead,
                    attemptedChange: request);
            }
        }
    }
}
