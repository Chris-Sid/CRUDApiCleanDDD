using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyApiProject.Application.Interfaces;
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
            var opportunity = await _opportunityRepo.CreateAsync(dto);
            return CreatedAtAction(nameof(CreateOpportunity), new { id = opportunity.Id }, opportunity);
        }
 
    }
}
