using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApiProject.Domain.Entities.Opportunities
{
    public class OpportunityBatchCreateDto
    {
        public List<OpportunityCreateDto> Opportunities { get; set; }
    }
}
