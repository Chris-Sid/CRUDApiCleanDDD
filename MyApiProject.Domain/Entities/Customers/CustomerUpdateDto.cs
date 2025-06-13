using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApiProject.Domain.Entities.Customers
{
    public class CustomerUpdateDto
    {
        public string Name { get; set; }
        [EmailAddress] 
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Status { get; set; }
        public List<ContactPerson> Contacts { get; set; }
        public List<Note> Notes { get; set; }
        public List<CommunicationLog> CommunicationLogs { get; set; }
        public List<Opportunity> Opportunities { get; set; }
    }
}
