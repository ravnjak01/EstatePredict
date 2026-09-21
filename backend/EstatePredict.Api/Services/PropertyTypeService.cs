using EstatePredict.Api.Data;
using EstatePredict.Api.DTO.PropertyType;
using EstatePredict.Api.Entities;
using EstatePredict.Api.Interfaces;
using EstatePredict.Api.Requests.PropertyType;
using Microsoft.EntityFrameworkCore;

namespace EstatePredict.Api.Services;

public class PropertyTypeService : IPropertyTypeService
{
    private readonly EstatePredictContext _context;

    public PropertyTypeService(EstatePredictContext context)
    {
        _context = context;
    }

    public async Task<List<PropertyTypeResponse>> GetAllAsync()
    {
        return await _context.PropertyTypes
            .Select(pt => new PropertyTypeResponse
            {
                Id = pt.Id,
                Name = pt.Name
            })
            .ToListAsync();
    }

    public async Task<PropertyTypeResponse?> GetByIdAsync(int id)
    {
        return await _context.PropertyTypes
            .Where(pt => pt.Id == id)
            .Select(pt => new PropertyTypeResponse
            {
                Id = pt.Id,
                Name = pt.Name
            })
            .FirstOrDefaultAsync();
    }

    public async Task<PropertyTypeResponse> InsertAsync(
        PropertyTypeInsertRequest request)
    {
        var exists = await _context.PropertyTypes
            .AnyAsync(pt => pt.Name == request.Name);

        if (exists)
        {
            throw new InvalidOperationException(
                "Property type already exists.");
        }

        var propertyType = new PropertyType
        {
            Name = request.Name
        };

        _context.PropertyTypes.Add(propertyType);

        await _context.SaveChangesAsync();

        return new PropertyTypeResponse
        {
            Id = propertyType.Id,
            Name = propertyType.Name
        };
    }

    public async Task<PropertyTypeResponse?> UpdateAsync(
        int id,
        PropertyTypeUpdateRequest request)
    {
        var propertyType = await _context.PropertyTypes
            .FirstOrDefaultAsync(pt => pt.Id == id);

        if (propertyType == null)
        {
            return null;
        }

        var exists = await _context.PropertyTypes
            .AnyAsync(pt =>
                pt.Id != id &&
                pt.Name == request.Name);

        if (exists)
        {
            throw new InvalidOperationException(
                "Property type already exists.");
        }

        propertyType.Name = request.Name;

        await _context.SaveChangesAsync();

        return new PropertyTypeResponse
        {
            Id = propertyType.Id,
            Name = propertyType.Name
        };
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var propertyType = await _context.PropertyTypes
            .FirstOrDefaultAsync(pt => pt.Id == id);

        if (propertyType == null)
        {
            return false;
        }

        _context.PropertyTypes.Remove(propertyType);

        await _context.SaveChangesAsync();

        return true;
    }
}