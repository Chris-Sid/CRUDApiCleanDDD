using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApiProject.Domain.Entities
{
    public class FollowUp
    {
        public DateTime ScheduledDate { get; set; }
        public bool IsCompleted { get; set; }
        public string Notes { get; set; }
    }
}
