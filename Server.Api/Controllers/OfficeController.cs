using Microsoft.AspNetCore.Mvc;
using Server.Api.DTOs.Offices;
using Server.Api.Services;

namespace Server.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OfficeController : ControllerBase
{
    private readonly OfficeService _officeService;

    public OfficeController(OfficeService officeService)
    {
        _officeService = officeService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        var offices = await _officeService.GetAllAsync(page, pageSize);
        return Ok(offices);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var office = await _officeService.GetByIdAsync(id);
        if (office == null) return NotFound();
        return Ok(office);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateOfficeRequest request)
    {
        var (success, error, office) = await _officeService.CreateAsync(request);
        if (!success) return Conflict(new { message = error });
        return CreatedAtAction(nameof(GetById), new { id = office!.Id }, office);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateOfficeRequest request)
    {
        var (success, error, office) = await _officeService.UpdateAsync(id, request);
        if (!success) return BadRequest(new { message = error });
        return Ok(office);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var success = await _officeService.DeleteAsync(id);
        if (!success) return NotFound();
        return NoContent();
    }
}