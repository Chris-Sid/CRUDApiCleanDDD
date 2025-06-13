using Microsoft.AspNetCore.Mvc;
using MyApiProject.Application.Interfaces;
using MyApiProject.Domain.Entities.Leads;

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
    }
}
