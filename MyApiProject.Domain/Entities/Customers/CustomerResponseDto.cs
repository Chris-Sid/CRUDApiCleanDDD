﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApiProject.Domain.Entities.Customers
{
    public class CustomerResponseDto : Customer
    {
        public Guid Id { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
