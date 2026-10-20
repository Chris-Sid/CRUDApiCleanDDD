using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyApiProject.Application.Interfaces;
using MyApiProject.Domain.Entities.Leads;
using MyApiProject.Domain.Entities.Opportunities;

namespace MyApiProject.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OpportunitiesController : ControllerBase
    {
        private readonly IOpportunityRepository _opportunityRepo;
        public OpportunitiesController(IOpportunityRepository opportunityRepo) { _opportunityRepo = opportunityRepo; }

        [HttpPost]
        public async Task<IActionResult> CreateOpportunity([FromBody] OpportunityCreateDto dto)
        {
            try
            {
                var opportunity = await _opportunityRepo.CreateAsync(dto);
                return CreatedAtAction(nameof(CreateOpportunity), new { id = opportunity.Id }, opportunity);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("batch")]
        public async Task<IActionResult> CreateBatch([FromBody] OpportunityBatchCreateDto dto)
        {
            try
            {
                foreach (var customerDto in dto.Opportunities)
                {
                    await _opportunityRepo.CreateAsync(customerDto);
                }
                return Ok(new { message = $"{dto.Opportunities.Count} opportunities created." });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
