using MyApiProject.Domain.Entities.FollowUps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApiProject.Domain.Entities.Leads
{
    public class LeadDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<FollowUpDto> FollowUps { get; set; }
    }
}
