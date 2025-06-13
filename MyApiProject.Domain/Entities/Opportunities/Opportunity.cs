using MyApiProject.Domain.Entities.Activity;
using MyApiProject.Domain.Entities.Customers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApiProject.Domain.Entities
{
    public class Opportunity
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Title { get; set; }
        public decimal EstimatedValue { get; set; }
        public string Stage { get; set; } // Negotiation, Proposal Sent, Won, Lost
        public Guid RelatedCustomerId { get; set; }
        public Customer? RelatedCustomer { get; set; }
        public List<ActivityLog> ActivityLogs { get; set; } = new();
    }
}
