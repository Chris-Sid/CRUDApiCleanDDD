using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MyApiProject.Domain.Entities
{
    public class CreateCustomerDto
    {
        [Required] public string Name { get; set; }
        [Required][EmailAddress] public string Email { get; set; }
        [Required] public string Phone { get; set; }
        [Required] public string Status { get; set; }
        public List<string> Tags { get; set; } = new();
        public List<ContactPersonDto> Contacts { get; set; } = new();
        public DateTime CreatedDate { get; set; }
    }
}
