using Microsoft.AspNetCore.Mvc;
using Server.Api.DTOs.Cars;
using Server.Api.Services;

namespace Server.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CarController : ControllerBase
{
    private readonly CarService _carService;

    public CarController(CarService carService)
    {
        _carService = carService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        var cars = await _carService.GetAllAsync(page, pageSize);
        return Ok(cars);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var car = await _carService.GetByIdAsync(id);
        if (car == null) return NotFound();
        return Ok(car);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateCarRequest request)
    {
        var (success, error, car) = await _carService.CreateAsync(request);
        if (!success) return BadRequest(new { message = error });
        return CreatedAtAction(nameof(GetById), new { id = car!.Id }, car);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateCarRequest request)
    {
        var (success, error, car) = await _carService.UpdateAsync(id, request);
        if (!success) return BadRequest(new { message = error });
        return Ok(car);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var success = await _carService.DeleteAsync(id);
        if (!success) return NotFound();
        return NoContent();
    }

    [HttpPatch("{id}/toggle-active")]
    public async Task<IActionResult> ToggleActive(Guid id)
    {
        var success = await _carService.ToggleActiveAsync(id);
        if (!success) return NotFound();
        return NoContent();
    }
}