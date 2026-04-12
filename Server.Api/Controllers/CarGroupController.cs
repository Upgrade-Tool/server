using Microsoft.AspNetCore.Mvc;
using Server.Api.DTOs.CarGroups;
using Server.Api.Models;
using Server.Api.Services;

namespace Server.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CarGroupController : ControllerBase
{
    private readonly CarGroupService _carGroupService;

    public CarGroupController(CarGroupService carGroupService)
    {
        _carGroupService = carGroupService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        var groups = await _carGroupService.GetAllAsync(page, pageSize);
        return Ok(groups);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var group = await _carGroupService.GetByIdAsync(id);
        if (group == null)
        {
            return NotFound();
        }

        return Ok(group);
    }
    
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateCarGroupRequest request)
    {
        var group = await _carGroupService.CreateAsync(request);
        return CreatedAtAction(nameof(GetById), new { id = group.Id }, group);
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateCarGroupRequest request)
    {
        var group = await _carGroupService.UpdateAsync(id, request);
        if (group == null) return NotFound();
        return Ok(group);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var success = await _carGroupService.DeleteAsync(id);
        if (!success) return NotFound();
        return NoContent();
    }
}