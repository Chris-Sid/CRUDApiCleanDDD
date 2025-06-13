using MyApiProject.Domain.Entities.Leads;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApiProject.Domain.Entities.FollowUps
{
    public class FollowUp
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTime ScheduledDate { get; set; }
        public bool IsCompleted { get; set; }
        public string Notes { get; set; }
        public Guid LeadId { get; set; }
        public Lead Lead { get; set; }
    }
}
