using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApiProject.Domain.Entities.Customers
{
    public class CustomerBatchCreateDto
    {
        public List<CreateCustomerDto> Customers { get; set; }
    }
}
