using Microsoft.AspNetCore.Mvc;
using MyApiProject.Application.Interfaces;
using MyApiProject.Domain.Entities.Customers;
using MyApiProject.Domain.Entities.Leads;
using MyApiProject.Infrastructure.Services.Customers;
using MyApiProject.Infrastructure.Services.Leads;

namespace MyApiProject.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LeadsController : ControllerBase
    {
        private readonly ILeadRepository _leadRepo;
        public LeadsController(ILeadRepository leadRepo) { _leadRepo = leadRepo; }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var leads = await _leadRepo.GetAllAsync();
            return Ok(leads);
        }

        [HttpPost]
        public async Task<IActionResult> CreateLead([FromBody] LeadCreateDto dto)
        {
            var lead = await _leadRepo.CreateAsync(dto);
            return CreatedAtAction(nameof(CreateLead), new { id = lead.Id }, lead);
        }

        [HttpPost("batch")]
        public async Task<IActionResult> CreateBatch([FromBody] LeadBatchCreateDto dto)
        {
            foreach (var leadDto in dto.Leads)
            {
                await _leadRepo.CreateAsync(leadDto);
            }
            return Ok(new { message = $"{dto.Leads.Count} leads created." });
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var lead = await _leadRepo.GetByIdAsync(id);
            return lead is null ? NotFound() : Ok(lead);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateLeadRequest request)
        {
            try
            {
                var lead = await _leadRepo.UpdateAsync(id, request);
                return lead is null ? NotFound() : Ok(lead);
            }
            catch (LeadConcurrencyException ex)
            {
                // 409 Conflict with the current server-side state so the client can merge/retry
                return Conflict(new
                {
                    message = "The lead was modified by another user since you last loaded it. Review the current data and resubmit.",
                    current = ex.CurrentState,
                    attempted = ex.AttemptedChange
                });
            }
        }
    }
}
