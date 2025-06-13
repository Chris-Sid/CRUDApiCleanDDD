using MyApiProject.Domain.Entities;
using MyApiProject.Domain.Entities.Leads;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApiProject.Application.Interfaces
{
    public interface ILeadRepository
    {
        Task<List<Lead>> GetAllAsync();
        Task<Lead> CreateAsync(LeadCreateDto dto);
    }

}
