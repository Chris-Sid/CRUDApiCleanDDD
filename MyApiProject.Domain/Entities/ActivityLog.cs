using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApiProject.Domain.Entities
{
    public class ActivityLog
    {
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
        public string Action { get; set; }
        public string Description { get; set; }
    }

}
