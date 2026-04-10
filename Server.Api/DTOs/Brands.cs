namespace Server.Api.DTOs;

public record BrandResponse(Guid Id,  string Name);

public record UpdateBrandRequest(string Name);