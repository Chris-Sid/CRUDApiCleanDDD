using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApiProject.Domain.Entities.FollowUps
{
    public class FollowUpDto
    {
        public Guid Id { get; set; }
        public DateTime ScheduledDate { get; set; }
        public bool IsCompleted { get; set; }
        public string Notes { get; set; }
        public Guid LeadId { get; set; } // only FK
                                         // No Lead navigation here!
    }
}
