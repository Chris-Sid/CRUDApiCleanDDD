using MyApiProject.Domain.Entities.Activity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApiProject.Domain.Entities.Opportunities
{
    public class OpportunityCreateDto
    {
        public string Title { get; set; }
        public decimal EstimatedValue { get; set; }
        public string Stage { get; set; }
        public Guid RelatedCustomerId { get; set; }
        public List<ActivityLogCreateDto> ActivityLogs { get; set; }
    }
}
