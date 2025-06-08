using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApiProject.Domain.Entities.GetCustomerDTOs
{
    public class ActivityLogGetDto: BaseEntityDto
    {
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
