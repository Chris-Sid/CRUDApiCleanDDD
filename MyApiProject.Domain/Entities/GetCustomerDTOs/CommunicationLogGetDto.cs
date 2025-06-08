using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApiProject.Domain.Entities.GetCustomerDTOs
{
    public class CommunicationLogGetDto: BaseEntityDto
    {
        public string Type { get; set; }
        public DateTime Date { get; set; }
        public string Summary { get; set; }
    }
}
