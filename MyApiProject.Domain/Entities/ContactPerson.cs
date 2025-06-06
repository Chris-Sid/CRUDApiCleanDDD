using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MyApiProject.Domain.Entities
{
    public class ContactPerson
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        public Guid CustomerId { get; set; }
        [JsonIgnore] // Prevent circular reference during serialization
        public Customer? Customer { get; set; }
    }
}
