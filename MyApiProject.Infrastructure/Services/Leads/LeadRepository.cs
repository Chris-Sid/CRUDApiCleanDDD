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

            /// <summary>
            /// Tells EF Core what the client *believes* the row's current
            /// concurrency token is (captured at the time the client last read
            /// this Lead), rather than trusting the value just reloaded from the
            /// database in the line above.
            ///
            /// --- SQL SERVER APPROACH ---
            /// Here, OriginalValue would be set to the raw `rowversion` bytes the
            /// client echoed back (typically base64-decoded from the API payload).
            /// SQL Server compares this against the row's actual `rowversion` in
            /// the generated UPDATE's WHERE clause.
            ///
            /// --- POSTGRESQL APPROACH (used here) ---
            /// OriginalValue is set to the `xmin` transaction ID the client last
            /// saw. EF/Npgsql generates:
            ///
            ///     UPDATE leads SET ... WHERE id = @id AND xmin = @originalXmin
            ///
            /// If another transaction already updated this row (creating a new
            /// tuple with a different xmin), zero rows match this statement, and
            /// EF throws DbUpdateConcurrencyException — caught below.
            /// </summary>
            _context.Entry(lead).Property(l => l.ConcurrencyToken).OriginalValue = request.ConcurrencyToken;

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
