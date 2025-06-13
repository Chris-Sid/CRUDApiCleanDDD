using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApiProject.Domain.Entities.Customers.GetCustomerDTOs
{
    public class ContactPersonGetDto: BaseEntityDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
    }
}
