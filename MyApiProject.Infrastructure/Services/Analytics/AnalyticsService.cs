using MyApiProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyApiProject.Application.Interfaces;

namespace MyApiProject.Infrastructure.Services.Analytics
{
    public class AnalyticsService : IAnalyticsService
    {
        private readonly ICustomerRepository _customerService;
        private readonly ILeadRepository _leadRepo;
        private readonly IOpportunityRepository _opportunityRepo;

        public AnalyticsService(
            ICustomerRepository customerService,
            ILeadRepository leadRepo,
            IOpportunityRepository opportunityRepo)
        {
            _customerService = customerService;
            _leadRepo = leadRepo;
            _opportunityRepo = opportunityRepo;
        }

        public async Task<AnalyticsSummaryDto> GetSummaryAsync(DateTime? from, DateTime? to)
        {
            // Get all customers as DTOs
            var customers = await _customerService.GetCustomersAsync();
            // Get all leads and opportunities as entities
            var leads = await _leadRepo.GetAllAsync();
            var opportunities = await _opportunityRepo.GetAllAsync();

            // Top performing sales rep by number of leads assigned
            var topSalesRep = leads
                .GroupBy(l => l.AssignedTo)
                .OrderByDescending(g => g.Count())
                .FirstOrDefault()?.Key;

            // Inactive customers: created more than 180 days ago
            var inactiveCustomers = customers
                .Where(c => (DateTime.UtcNow - c.CreatedDate).TotalDays > 180)
                .Select(c => c.Name)
                .ToList();

            // Deals in negotiation stage
            var dealsInNegotiation = opportunities.Count(o => o.Stage == "Negotiation");

            // Won deals this month
            var now = DateTime.UtcNow;
            var wonDealsThisMonth = opportunities.Count(o =>
                o.Stage == "Won" &&
                o.ActivityLogs != null &&
                o.ActivityLogs.Any(a => a.CreatedAt.Month == now.Month && a.CreatedAt.Year == now.Year)
            );

            // Active leads: not converted or lost
            var activeLeads = leads.Count(l => l.Status != "Converted" && l.Status != "Lost");

            return new AnalyticsSummaryDto
            {
                TotalCustomers = customers.Count(),
                ActiveLeads = activeLeads,
                DealsInNegotiation = dealsInNegotiation,
                WonDealsThisMonth = wonDealsThisMonth,
                TopPerformingSalesRep = topSalesRep,
                InactiveCustomers = inactiveCustomers
            };
        }
    }
}
