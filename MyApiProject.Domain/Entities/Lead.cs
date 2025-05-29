using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApiProject.Domain.Entities
{
    public class Lead
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Source { get; set; } // Web, Referral, etc.
        public string ContactInfo { get; set; }
        public string AssignedTo { get; set; } // User ID or name
        public string Status { get; set; } // New, Contacted, Converted, Lost
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public List<FollowUp> FollowUps { get; set; } = new();
    }
}
