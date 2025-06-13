using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApiProject.Domain.Entities.Leads
{
    public class LeadBatchCreateDto
    {
        public List<LeadCreateDto> Leads { get; set; }
    }
}
