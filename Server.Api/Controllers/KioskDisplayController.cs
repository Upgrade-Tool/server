using Microsoft.AspNetCore.Mvc;
using Server.Api.DTOs.KioskDisplays;
using Server.Api.Services;

namespace Server.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class KioskDisplayController : ControllerBase
{
    private readonly KioskDisplayService _kioskDisplayService;

    public KioskDisplayController(KioskDisplayService kioskDisplayService)
    {
        _kioskDisplayService = kioskDisplayService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        var kiosks = await _kioskDisplayService.GetAllAsync(page, pageSize);
        return Ok(kiosks);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var kiosk = await _kioskDisplayService.GetByIdAsync(id);
        if (kiosk == null) return NotFound();
        return Ok(kiosk);
    }

    [HttpGet("slug/{slug}")]
    public async Task<IActionResult> GetBySlug(string slug)
    {
        var kiosk = await _kioskDisplayService.GetBySlugAsync(slug);
        if (kiosk == null) return NotFound();
        return Ok(kiosk);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateKioskDisplayRequest request)
    {
        var (success, error, kiosk) = await _kioskDisplayService.CreateAsync(request);
        if (!success) return Conflict(new { message = error });
        return CreatedAtAction(nameof(GetById), new { id = kiosk!.Id }, kiosk);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateKioskDisplayRequest request)
    {
        var (success, error, kiosk) = await _kioskDisplayService.UpdateAsync(id, request);
        if (!success) return BadRequest(new { message = error });
        return Ok(kiosk);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var success = await _kioskDisplayService.DeleteAsync(id);
        if (!success) return NotFound();
        return NoContent();
    }

    [HttpPatch("{id}/toggle-active")]
    public async Task<IActionResult> ToggleActive(Guid id)
    {
        var success = await _kioskDisplayService.ToggleActiveAsync(id);
        if (!success) return NotFound();
        return NoContent();
    }
}