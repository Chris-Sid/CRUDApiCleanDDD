using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyApiProject.Application.Interfaces;

namespace MyApiProject.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]
    public class AnalyticsController : Controller
    {
        private readonly IAnalyticsService _analyticsService;
        public AnalyticsController(IAnalyticsService analyticsService)
        {
            _analyticsService = analyticsService;
        }

        [HttpGet("analytics/summary")]
        public async Task<IActionResult> GetAnalyticsSummary([FromQuery] DateTime? from, [FromQuery] DateTime? to)
        {
            if (from.HasValue && to.HasValue && from > to)
            {
                return BadRequest("Invalid date range: 'from' date must be earlier than 'to' date.");
            }
            var summary = await _analyticsService.GetSummaryAsync(from, to);
            return Ok(summary);
        }
    }
}
