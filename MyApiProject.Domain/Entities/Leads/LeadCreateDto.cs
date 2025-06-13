using MyApiProject.Domain.Entities.FollowUps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApiProject.Domain.Entities.Leads
{
    public class LeadCreateDto
    {
        [System.Text.Json.Serialization.JsonIgnore]
        public Guid Id { get; set; }
        public string Source { get; set; }
        public string ContactInfo { get; set; }
        public string AssignedTo { get; set; }
        public string Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public List<FollowUpCreateDto> FollowUps { get; set; }
    }
}
