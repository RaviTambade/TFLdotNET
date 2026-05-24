using CRMAnalyticsApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace CRMAnalyticsApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AnalyticsController : ControllerBase
{
    private readonly ICRMAnalyticsService _service;

    public AnalyticsController(
        ICRMAnalyticsService service)
    {
        _service = service;
    }

    [HttpGet("dashboard")]
    public async Task<IActionResult> GetDashboard()
    {
        var analytics =
            await _service.GetDashboardAnalyticsAsync();

        return Ok(analytics);
    }
}