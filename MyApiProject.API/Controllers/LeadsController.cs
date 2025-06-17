using Microsoft.AspNetCore.Mvc;
using MyApiProject.Application.Interfaces;
using MyApiProject.Domain.Entities.Customers;
using MyApiProject.Domain.Entities.Leads;
using MyApiProject.Infrastructure.Services.Customers;

namespace MyApiProject.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LeadsController : ControllerBase
    {
        private readonly ILeadRepository _leadRepo;
        public LeadsController(ILeadRepository leadRepo) { _leadRepo = leadRepo; }

        [HttpPost]
        public async Task<IActionResult> CreateLead([FromBody] LeadCreateDto dto)
        {
            var lead = await _leadRepo.CreateAsync(dto);
            return CreatedAtAction(nameof(CreateLead), new { id = lead.Id }, lead);
        }

        [HttpPost("batch")]
        public async Task<IActionResult> CreateBatch([FromBody] LeadBatchCreateDto dto)
        {
            foreach (var customerDto in dto.Leads)
            {
                await _leadRepo.CreateAsync(customerDto);
            }
            return Ok(new { message = $"{dto.Leads.Count} Leads created." });
        }
    }
}
