using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApiProject.Domain.Entities
{
    public class Customer
    {
        public Guid Id { get; set; }
        [Required]
        public string name { get; set; }
        [EmailAddress]
        [Required]
        public string email { get; set; }
        public string phone { get; set; }
        [Required]
        public string status { get; set; }
        public DateTime createddate { get; set; } = DateTime.UtcNow;
        public List<ContactPerson> contacts { get; set; } = new();
        public List<CommunicationLog> communicationlogs { get; set; }
        public List<Note> notes { get; set; }
        public List<Opportunity> opportunities { get; set; }
    }
}
