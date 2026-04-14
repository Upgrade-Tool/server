using Microsoft.AspNetCore.Mvc;
using Server.Api.Services;
using System.Text.Json;

namespace Server.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class KioskStateController : ControllerBase
{
    private readonly KioskStateService _kioskStateService;

    public KioskStateController(KioskStateService kioskStateService)
    {
        _kioskStateService = kioskStateService;
    }

    [HttpGet("{slug}")]
    public async Task<IActionResult> GetState(string slug)
    {
        var (success, error, state) = await _kioskStateService.GetStateAsync(slug);
        if (!success) return NotFound(new { message = error });
        return Ok(state);
    }

    [HttpPost("{slug}/draft")]
    public async Task<IActionResult> SaveDraft(string slug, [FromBody] JsonDocument state)
    {
        var (success, error) = await _kioskStateService.SaveDraftAsync(slug, state);
        if (!success) return NotFound(new { message = error });
        return Ok(new { message = "Draft saved" });
    }

    [HttpPost("{slug}/publish")]
    public async Task<IActionResult> Publish(string slug, [FromBody] JsonDocument state)
    {
        var (success, error) = await _kioskStateService.PublishAsync(slug, state);
        if (!success) return NotFound(new { message = error });
        return Ok(new { message = "Published successfully" });
    }

    [HttpPost("{slug}/reset")]
    public async Task<IActionResult> Reset(string slug)
    {
        var (success, error) = await _kioskStateService.ResetAsync(slug);
        if (!success) return NotFound(new { message = error });
        return Ok(new { message = "Reset successfully" });
    }

    [HttpPost("{slug}/accept")]
    public async Task<IActionResult> AcceptOffer(string slug)
    {
        var (success, error) = await _kioskStateService.AcceptOfferAsync(slug);
        if (!success) return NotFound(new { message = error });
        return Ok(new { message = "Offer accepted" });
    }

    [HttpPost("{slug}/showcase-open")]
    public async Task<IActionResult> ShowcaseOpen(string slug, [FromQuery] Guid carId)
    {
        var (success, error) = await _kioskStateService.ShowcaseOpenAsync(slug, carId);
        if (!success) return NotFound(new { message = error });
        return Ok(new { message = "Showcase opened" });
    }

    [HttpPost("{slug}/showcase-close")]
    public async Task<IActionResult> ShowcaseClose(string slug)
    {
        var (success, error) = await _kioskStateService.ShowcaseCloseAsync(slug);
        if (!success) return NotFound(new { message = error });
        return Ok(new { message = "Showcase closed" });
    }
}