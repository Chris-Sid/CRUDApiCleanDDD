using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApiProject.Domain.Entities.GetCustomerDTOs
{
    public class OpportunityGetDto: BaseEntityDto
    {
        public string Title { get; set; }
        public decimal EstimatedValue { get; set; }
        public string Stage { get; set; }
        public List<ActivityLogGetDto> ActivityLogs { get; set; }
    }
}
