using EstatePredict.Api.Database;
using EstatePredict.DTOs;
using EstatePredict.Entities;
using EstatePredict.Requests;
using EstatePredict.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EstatePredict.Services.Implementations;

public class LocationService : ILocationService
{
    private readonly EstatePredictContext _context;

    public LocationService(EstatePredictContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<LocationDTO>> GetAllAsync()
    {
        return await _context.Locations
            .Select(l => new LocationDTO(l.Id, l.Country, l.City, l.Municipality))
            .ToListAsync();
    }

    public async Task<LocationDTO> CreateAsync(CreateLocationRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.City) || string.IsNullOrWhiteSpace(request.Municipality))
            throw new ArgumentException("City and Municipality cannot be empty.");

        var exists = await _context.Locations.AnyAsync(l =>
            l.City.ToLower() == request.City.ToLower() &&
            l.Municipality.ToLower() == request.Municipality.ToLower());

        if (exists)
            throw new InvalidOperationException($"Location in {request.City} ({request.Municipality}) already exists.");

        var location = new Location
        {
            Country = request.Country,
            City = request.City,
            Municipality = request.Municipality
        };

        _context.Locations.Add(location);
        await _context.SaveChangesAsync();

        return new LocationDTO(location.Id, location.Country, location.City, location.Municipality);
    }

    public async Task<LocationDTO?> GetByIdAsync(int id)
    {
        var location = await _context.Locations.FindAsync(id);

        if (location is null) return null;

        return new LocationDTO(location.Id, location.Country, location.City, location.Municipality);
    }
}