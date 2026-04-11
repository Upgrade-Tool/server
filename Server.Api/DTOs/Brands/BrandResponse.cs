namespace Server.Api.DTOs.Brands;

public record BrandResponse(Guid Id,  string Name, DateTime CreatedAt, DateTime UpdatedAt);
