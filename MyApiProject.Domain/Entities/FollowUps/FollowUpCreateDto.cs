using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApiProject.Domain.Entities.FollowUps
{
    public class FollowUpCreateDto
    {
        [System.Text.Json.Serialization.JsonPropertyName("id")]
        public Guid Id { get; set; }
        public DateTime ScheduledDate { get; set; }
        public bool IsCompleted { get; set; }
        public string Notes { get; set; }
    }
}
