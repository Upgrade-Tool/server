using Server.Api.DTOs;
using Server.Api.Models;
using Server.Api.Repositories;

namespace Server.Api.Services;

public class BrandService
{
    private readonly IBrandRepository  _brandRepository;

    public BrandService(IBrandRepository brandRepository)
    {
        _brandRepository = brandRepository;
    }


   /* public async Task<List<BrandResponse>> GetAllAsync()
    {
    }
    */
}