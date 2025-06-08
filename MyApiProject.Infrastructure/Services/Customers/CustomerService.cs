using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using MyApiProject.Application.Interfaces;
using MyApiProject.Domain.Entities;
using MyApiProject.Domain.Entities.GetCustomerDTOs;
using MyApiProject.Infrastructure.Persistence;

namespace MyApiProject.Infrastructure.Services.Customers
{
    public class CustomerService : ICustomerService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        public CustomerService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CustomerGetDto>> GetCustomersAsync()
        {
            var customers = await _context.customers
            .ProjectTo<CustomerGetDto>(_mapper.ConfigurationProvider)
            .ToListAsync();

            return customers;
        }

        public async Task<Customer?> GetCustomerAsync(Guid id)
        {
            return await _context.customers
                .Include(c => c.contacts)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Customer?> CreateCustomerAsync(CreateCustomerDto dto)
        {
            var customerId = Guid.NewGuid();

            var existingContactEmails = await _context.contactpersons
                .Where(cp => cp.CustomerId == customerId)
                .Select(cp => cp.Email.ToLower())
                .ToListAsync();

            var newContacts = dto.Contacts
                .Where(c => !existingContactEmails.Contains(c.Email.ToLower()))
                .Select(c => new ContactPerson
                {
                    Id = Guid.NewGuid(),
                    CustomerId = customerId,
                    Name = c.Name,
                    Email = c.Email
                })
                .ToList();

            var customer = new Customer
            {
                Id = customerId,
                name = dto.Name,
                email = dto.Email,
                phone = dto.Phone,
                status = dto.Status,
                createddate = DateTime.UtcNow,
                contacts = newContacts
            };

            _context.customers.Add(customer);
            await _context.SaveChangesAsync();

            return customer;
        }

        public async Task<bool> UpdateCustomerAsync(Guid id, CustomerUpdateDto dto)
        {
            var customer = await _context.customers
                .Include(c => c.contacts)
                .Include(c => c.notes)
                .Include(c => c.communicationlogs)
                .Include(c => c.opportunities)
                    .ThenInclude(o => o.ActivityLogs)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (customer == null)
                return false;

            customer.name = dto.Name;
            customer.email = dto.Email;
            customer.phone = dto.Phone;
            customer.status = dto.Status;

            // Update Contacts
            var existingContacts = customer.contacts.ToList();
            _context.contactpersons.RemoveRange(existingContacts);
            if (dto.Contacts != null)
            {
                var newContacts = dto.Contacts.Select(c => new ContactPerson
                {
                    Id = Guid.NewGuid(),
                    Name = c.Name,
                    Email = c.Email,
                    CustomerId = id
                }).ToList();
                _context.contactpersons.AddRange(newContacts);
                customer.contacts = newContacts;
            }
            else
            {
                customer.contacts = new List<ContactPerson>();
            }

            // Update Notes
            var existingNotes = customer.notes.ToList();
            _context.notes.RemoveRange(existingNotes);
            if (dto.Notes != null)
            {
                var newNotes = dto.Notes.Select(n => new Note
                {
                    Id = Guid.NewGuid(),
                    Content = n.Content,
                    CreatedBy = n.CreatedBy,
                    CreatedAt = DateTime.UtcNow,
                    RelatedCustomerId = id
                }).ToList();
                _context.notes.AddRange(newNotes);
                customer.notes = newNotes;
            }
            else
            {
                customer.notes = new List<Note>();
            }

            // Update Communication Logs
            var existingLogs = customer.communicationlogs.ToList();
            _context.communicationlogs.RemoveRange(existingLogs);
            if (dto.CommunicationLogs != null)
            {
                var newLogs = dto.CommunicationLogs.Select(log => new CommunicationLog
                {
                    Id = Guid.NewGuid(),
                    Type = log.Type,
                    Date = log.Date,
                    Summary = log.Summary,
                    CustomerId = id
                }).ToList();
                _context.communicationlogs.AddRange(newLogs);
                customer.communicationlogs = newLogs;
            }
            else
            {
                customer.communicationlogs = new List<CommunicationLog>();
            }

            // Update Opportunities
            var existingOpportunities = customer.opportunities.ToList();
            _context.opportunities.RemoveRange(existingOpportunities);
            if (dto.Opportunities != null)
            {
                var newOpportunities = dto.Opportunities.Select(o => new Opportunity
                {
                    Id = Guid.NewGuid(),
                    Title = o.Title,
                    Stage = o.Stage,
                    EstimatedValue = o.EstimatedValue,
                    RelatedCustomerId = id,
                    ActivityLogs = o.ActivityLogs?.Select(a => new ActivityLog
                    {
                        Id = Guid.NewGuid(),
                        CreatedAt = a.CreatedAt,
                        Description = a.Description,
                        OpportunityId = o.Id
                    }).ToList() ?? new List<ActivityLog>()
                }).ToList();
                _context.opportunities.AddRange(newOpportunities);
                customer.opportunities = newOpportunities;
            }
            else
            {
                customer.opportunities = new List<Opportunity>();
            }

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteCustomerAsync(Guid id)
        {
            var customer = await _context.customers.FindAsync(id);
            if (customer == null) return false;

            _context.customers.Remove(customer);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}