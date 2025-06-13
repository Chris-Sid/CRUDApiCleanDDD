using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApiProject.Domain.Entities.Activity
{
    public class ActivityLogCreateDto
    {
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
    }

}
