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
}