using Microsoft.AspNetCore.Mvc;
using Server.Api.DTOs.Brands;
using Server.Api.Models;
using Server.Api.Services;

namespace Server.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BrandController : ControllerBase
{
    private readonly BrandService _brandService;
    
    public  BrandController(BrandService brandService)
    {
      _brandService = brandService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        var brands = await _brandService.GetAllAsync(page, pageSize);
        return Ok(brands);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var brand = await _brandService.GetByIdAsync(id);
        if (brand == null)
        {
            return NotFound();
        }

        return Ok(brand);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateBrandRequest request)
    {
        var brand = await _brandService.AddAsync(request);
        return CreatedAtAction(nameof(GetById), new { id = brand.Id }, brand);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateBrandRequest request)
    {
        var brand = await _brandService.UpdateAsync(id, request);
        if (brand == null)
        {
            return NotFound();
        }
        return Ok(brand);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var sucess = await _brandService.DeleteAsync(id);
        if (!sucess) return NotFound();
        return NoContent();
    }
}