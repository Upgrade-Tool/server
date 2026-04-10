namespace Server.Api.DTOs;

public record BrandResponse(Guid id,  string name);

public record UpdateBrandRequest(string Name);
