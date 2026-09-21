namespace EstatePredict.Api.Services;
using EstatePredict.Api.Data;
using EstatePredict.Api.DTO.Location;
using EstatePredict.Api.Entities;
using EstatePredict.Api.Interfaces;
using EstatePredict.Api.Requests.Location;
using Microsoft.EntityFrameworkCore;

public class LocationService : ILocationService
{
    private readonly EstatePredictContext _context;

    public LocationService(EstatePredictContext context)
    {
        _context = context;
    }

    public async Task<List<LocationResponse>> GetAllAsync()
    {
        return await _context.Locations
            .Select(l => new LocationResponse
            {
                Id = l.Id,
                Country = l.Country,
                City = l.City,
                Municipality = l.Municipality
            })
            .ToListAsync();
    }

    public async Task<LocationResponse?> GetByIdAsync(int id)
    {
        return await _context.Locations
            .Where(l => l.Id == id)
            .Select(l => new LocationResponse
            {
                Id = l.Id,
                Country = l.Country,
                City = l.City,
                Municipality = l.Municipality
            })
            .FirstOrDefaultAsync();
    }

    public async Task<LocationResponse> InsertAsync(
        LocationInsertRequest request)
    {
        var location = new Location
        {
            Country = request.Country,
            City = request.City,
            Municipality = request.Municipality
        };

        _context.Locations.Add(location);

        await _context.SaveChangesAsync();

        return new LocationResponse
        {
            Id = location.Id,
            Country = location.Country,
            City = location.City,
            Municipality = location.Municipality
        };
    }

    public async Task<LocationResponse?> UpdateAsync(
        int id,
        LocationUpdateRequest request)
    {
        var location = await _context.Locations
            .FirstOrDefaultAsync(l => l.Id == id);

        if (location == null)
        {
            return null;
        }

        location.Country = request.Country;
        location.City = request.City;
        location.Municipality = request.Municipality;

        await _context.SaveChangesAsync();

        return new LocationResponse
        {
            Id = location.Id,
            Country = location.Country,
            City = location.City,
            Municipality = location.Municipality
        };
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var location = await _context.Locations
            .FirstOrDefaultAsync(l => l.Id == id);

        if (location == null)
        {
            return false;
        }

        _context.Locations.Remove(location);

        await _context.SaveChangesAsync();

        return true;
    }
}