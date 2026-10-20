using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApiProject.Domain.Entities
{
    public abstract class ConcurrentEntity
    {
        [Timestamp]
        public uint RowVersion { get; set; }
    }
}
