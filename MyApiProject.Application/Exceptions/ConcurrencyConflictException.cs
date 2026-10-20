using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApiProject.Application.Exceptions
{
    public class ConcurrencyConflictException : Exception
    {
        public string EntityName { get; }
        public object? CurrentValues { get; }

        public ConcurrencyConflictException(string entityName, object? currentValues = null)
            : base($"{entityName} was modified or deleted by another user.")
        {
            EntityName = entityName;
            CurrentValues = currentValues;
        }
    }
}
