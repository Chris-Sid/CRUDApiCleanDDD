using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApiProject.Domain.Entities
{
    public class CommunicationLog
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Type { get; set; } // Call, Email
        public DateTime Date { get; set; }
        public string Summary { get; set; }
        public Guid CustomerId { get; set; }
    }
}
