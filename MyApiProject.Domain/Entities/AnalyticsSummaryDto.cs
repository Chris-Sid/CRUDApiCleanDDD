using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApiProject.Domain.Entities
{
    public class AnalyticsSummaryDto
    {
        public int TotalCustomers { get; set; }
        public int ActiveLeads { get; set; }
        public int DealsInNegotiation { get; set; }
        public int WonDealsThisMonth { get; set; }
        public string TopPerformingSalesRep { get; set; }
        public List<string> InactiveCustomers { get; set; }
    }
}
