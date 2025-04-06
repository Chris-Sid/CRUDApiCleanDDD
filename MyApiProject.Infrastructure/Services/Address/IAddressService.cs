using MyApiProject.Application.DTOs;
using MyApiProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApiProject.Infrastructure.Services.Address
{
    public interface IAddressService
    {
        Task<AddressDto> GetAddressAsync(DmsIdentifier Address,AddressRequestHeaders headers, CancellationToken cancellationToken = default);   
        Task<AddressDto> PostAddressAsync(AddressDto AddressDto,AddressRequestHeaders headers, CancellationToken cancellationToken = default);
    }
}
