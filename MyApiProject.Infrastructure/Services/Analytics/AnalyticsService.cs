using MyApiProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApiProject.Infrastructure.Services.Analytics
{
    public class AnalyticsService
    {
        //private readonly ICustomerRepository _customerRepo;
        //private readonly ILeadRepository _leadRepo;
        //private readonly IOpportunityRepository _opportunityRepo;

        //public AnalyticsService(ICustomerRepository customerRepo, ILeadRepository leadRepo, IOpportunityRepository opportunityRepo)
        //{
        //    _customerRepo = customerRepo;
        //    _leadRepo = leadRepo;
        //    _opportunityRepo = opportunityRepo;
        //}

        //public async Task<AnalyticsSummaryDto> GetSummaryAsync()
        //{
        //    var customers = await _customerRepo.GetAllAsync();
        //    var leads = await _leadRepo.GetAllAsync();
        //    var opportunities = await _opportunityRepo.GetAllAsync();

        //    var topSalesRep = leads
        //        .GroupBy(l => l.AssignedTo)
        //        .OrderByDescending(g => g.Count())
        //        .FirstOrDefault()?.Key;

        //    var inactiveCustomers = customers
        //        .Where(c => (DateTime.UtcNow - c.CreatedDate).Days > 180)
        //        .Select(c => c.Name)
        //        .ToList();

        //    return new AnalyticsSummaryDto
        //    {
        //        TotalCustomers = customers.Count(),
        //        ActiveLeads = leads.Count(l => l.Status != "Converted" && l.Status != "Lost"),
        //        DealsInNegotiation = opportunities.Count(o => o.Stage == "Negotiation"),
        //        WonDealsThisMonth = opportunities.Count(o => o.Stage == "Won" && o.ActivityLogs.Any(a => a.Timestamp.Month == DateTime.UtcNow.Month)),
        //        TopPerformingSalesRep = topSalesRep,
        //        InactiveCustomers = inactiveCustomers
        //    };
        //}
    }
}
