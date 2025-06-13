using MyApiProject.Domain.Entities.Activity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApiProject.Domain.Entities.Opportunities
{
    public class OpportunityDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public decimal EstimatedValue { get; set; }
        public string Stage { get; set; }
        public List<ActivityLogDto> ActivityLogs { get; set; }
    }
}
