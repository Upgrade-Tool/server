using Server.Api.DTOs;
using Server.Api.DTOs.Brands;
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


   public async Task<List<BrandResponse>> GetAllAsync(int page, int  pageSize)
   {
       var brands = await _brandRepository.GetAllAsync(page, pageSize);
       return brands.Select(MapToResponse).ToList();
   }

   public async Task<BrandResponse?> GetByIdAsync(Guid id)
   {
       var brand = await _brandRepository.GetByIdAsync(id);
       if (brand == null)
       {
           return null;
       }

       return MapToResponse(brand);
   }

   public async Task<BrandResponse> AddAsync(CreateBrandRequest request)
   {
       var brand = new Brand
       {
           Id = Guid.NewGuid(),
           Name = request.Name,
           CreatedAt = DateTime.UtcNow,
           UpdatedAt = DateTime.UtcNow,
       };
       var created = await _brandRepository.CreateAsync(brand);
       return MapToResponse(created);
   }

   public async Task<BrandResponse?> UpdateAsync(Guid id, UpdateBrandRequest request)
   {
       var brand = await _brandRepository.GetByIdAsync(id);
       if (brand == null)
       {
           return null;
       }
       brand.Name = request.Name;
       brand.UpdatedAt = DateTime.UtcNow;

       var updated = await _brandRepository.UpdateAsync(brand);
       return MapToResponse(updated);
   }
   
   public async Task<bool> DeleteAsync(Guid id)
   {
       return await _brandRepository.DeleteAsync(id);
   }
   
   
   private static BrandResponse MapToResponse(Brand brand) =>
       new(brand.Id, brand.Name, brand.CreatedAt, brand.UpdatedAt);
   
}