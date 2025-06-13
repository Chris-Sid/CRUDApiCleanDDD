using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApiProject.Domain.Entities.Customers.GetCustomerDTOs
{
    public class CustomerGetDto : BaseEntityDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public List<ContactPersonGetDto> Contacts { get; set; }
        public List<NoteGetDto> Notes { get; set; }
        public List<CommunicationLogGetDto> CommunicationLogs { get; set; }
        public List<OpportunityGetDto> Opportunities { get; set; }
    }
}
