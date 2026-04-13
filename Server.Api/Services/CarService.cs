using Server.Api.DTOs.Cars;
using Server.Api.Models;
using Server.Api.Repositories;

namespace Server.Api.Services;

public class CarService
{
    private readonly ICarGroupRepository _carGroupRepository;
    private readonly IBrandRepository _brandRepository;
    private readonly ICarRepository _carRepository;

    public CarService(ICarGroupRepository carGroupRepository, IBrandRepository brandRepository,
        ICarRepository carRepository)
    {
        _carGroupRepository = carGroupRepository;
        _brandRepository = brandRepository;
        _carRepository = carRepository;
    }

    public async Task<List<CarResponse>> GetAllAsync(int page, int pageSize)
    {
        var cars = await _carRepository.GetAllAsync(page, pageSize);
        return cars.Select(MapToResponse).ToList();
    }
    
    public async Task<CarResponse?> GetByIdAsync(Guid id)
    {
        var car = await _carRepository.GetByIdAsync(id);
        if (car == null) return null;
        return MapToResponse(car);
    }

    public async Task<(bool success, string error, CarResponse? car)> CreateAsync(CreateCarRequest request)
    {
        var brandExists = await _brandRepository.GetByIdAsync(request.BrandId);
        if (brandExists == null)
            return (false, "Brand not found", null);

        var groupExists = await _carGroupRepository.GetByIdAsync(request.GroupId);
        if (groupExists == null)
            return (false, "Car group not found", null);

        var car = new Car
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            BrandId = request.BrandId,
            GroupId = request.GroupId,
            Horsepower = request.Horsepower,
            RangeKm = request.RangeKm,
            Drivetrain = request.Drivetrain,
            Transmission = request.Transmission,
            CarValueFactor = request.CarValueFactor,
            ImageUrlSideLeft = request.ImageUrlSideLeft,
            ImageUrlSideRight = request.ImageUrlSideRight,
            ImageUrlDisplay = request.ImageUrlDisplay,
            IsActive = true,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        var created = await _carRepository.CreateAsync(car);

        // Reload with includes so Brand and Group are populated
        var result = await _carRepository.GetByIdAsync(created.Id);
        return (true, string.Empty, MapToResponse(result!));
    }

    public async Task<(bool success, string error, CarResponse? car)> UpdateAsync(Guid id, UpdateCarRequest request)
    {
        var car = await _carRepository.GetByIdAsync(id);
        if (car == null)
            return (false, "Car not found", null);

        var brandExists = await _brandRepository.GetByIdAsync(request.BrandId);
        if (brandExists == null)
            return (false, "Brand not found", null);

        var groupExists = await _carGroupRepository.GetByIdAsync(request.GroupId);
        if (groupExists == null)
            return (false, "Car group not found", null);

        car.Name = request.Name;
        car.BrandId = request.BrandId;
        car.GroupId = request.GroupId;
        car.Horsepower = request.Horsepower;
        car.RangeKm = request.RangeKm;
        car.Drivetrain = request.Drivetrain;
        car.Transmission = request.Transmission;
        car.CarValueFactor = request.CarValueFactor;
        car.ImageUrlSideLeft = request.ImageUrlSideLeft;
        car.ImageUrlSideRight = request.ImageUrlSideRight;
        car.ImageUrlDisplay = request.ImageUrlDisplay;
        car.UpdatedAt = DateTime.UtcNow;

        var updated = await _carRepository.UpdateAsync(car);
        var result = await _carRepository.GetByIdAsync(updated.Id);
        return (true, string.Empty, MapToResponse(result!));
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        return await _carRepository.DeleteAsync(id);
    }

    public async Task<bool> ToggleActiveAsync(Guid id)
    {
        return await _carRepository.ToggleActiveAsync(id);
    }
    
    
    
    private static CarResponse MapToResponse(Car car) =>
        new(
            car.Id,
            car.Name,
            car.BrandId,
            car.Brand.Name,
            car.GroupId,
            car.Group.Code,
            car.Horsepower,
            car.RangeKm,
            car.Drivetrain,
            car.Transmission,
            car.CarValueFactor,
            car.ImageUrlSideLeft,
            car.ImageUrlSideRight,
            car.ImageUrlDisplay,
            car.IsActive,
            car.CreatedAt,
            car.UpdatedAt
        );
}