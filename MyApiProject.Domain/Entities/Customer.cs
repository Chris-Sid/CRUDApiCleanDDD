using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApiProject.Domain.Entities
{
    public class Customer
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Status { get; set; } // Lead, Prospect, Client
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public List<string> Tags { get; set; } = new();
        public List<ContactPerson> Contacts { get; set; } = new();
    }
}
