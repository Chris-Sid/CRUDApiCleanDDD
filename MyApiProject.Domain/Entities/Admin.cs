using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApiProject.Domain.Entities
{
    public class Admin
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        [MaxLength(100)]
        public string PasswordHash { get; set; } // Store hashed passwords
        public string Role { get; set; } // "Admin", "User", etc.
    }
}
