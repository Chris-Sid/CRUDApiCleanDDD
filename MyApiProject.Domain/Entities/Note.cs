using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApiProject.Domain.Entities
{
    public class Note
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid RelatedCustomerId { get; set; }
        public string CreatedBy { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
