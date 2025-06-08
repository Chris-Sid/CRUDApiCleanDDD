using MyApiProject.Domain.Entities;
using MyApiProject.Domain.Entities.GetCustomerDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApiProject.Application.Interfaces
{
    public interface ICustomerService
    {
        Task<IEnumerable<CustomerGetDto>> GetCustomersAsync();
        Task<Customer?> GetCustomerAsync(Guid id);
        Task<Customer?> CreateCustomerAsync(CreateCustomerDto dto);
        Task<bool> UpdateCustomerAsync(Guid id, CustomerUpdateDto dto);
        Task<bool> DeleteCustomerAsync(Guid id);
    }
}
