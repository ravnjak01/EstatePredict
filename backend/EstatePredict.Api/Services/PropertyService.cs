using EstatePredict.Api.Data;
using EstatePredict.Api.DTO.Property;
using EstatePredict.Api.Entities;
using EstatePredict.Api.Interfaces;
using EstatePredict.Api.Requests.Property;
using Microsoft.EntityFrameworkCore;

namespace EstatePredict.Api.Services;

public class PropertyService : IPropertyService
{
    private readonly EstatePredictContext _context;

    public PropertyService(EstatePredictContext context)
    {
        _context = context;
    }

    public async Task<List<PropertyResponse>> GetAllAsync()
    {
        return await _context.Properties
            .Select(p => new PropertyResponse
            {
                Id = p.Id,
                Title = p.Title,
                Description = p.Description,
                Area = p.Area,
                NumberOfRooms = p.NumberOfRooms,
                YearBuilt = p.YearBuilt,
                HasParking = p.HasParking,
                HasLift = p.HasLift,
                Condition = p.Condition,
                CurrentPrice = p.CurrentPrice,
                UserId = p.UserId,
                LocationId = p.LocationId,
                PropertyTypeId = p.PropertyTypeId
            })
            .ToListAsync();
    }

    public async Task<PropertyResponse?> GetByIdAsync(int id)
    {
        return await _context.Properties
            .Where(p => p.Id == id)
            .Select(p => new PropertyResponse
            {
                Id = p.Id,
                Title = p.Title,
                Description = p.Description,
                Area = p.Area,
                NumberOfRooms = p.NumberOfRooms,
                YearBuilt = p.YearBuilt,
                HasParking = p.HasParking,
                HasLift = p.HasLift,
                Condition = p.Condition,
                CurrentPrice = p.CurrentPrice,
                UserId = p.UserId,
                LocationId = p.LocationId,
                PropertyTypeId = p.PropertyTypeId
            })
            .FirstOrDefaultAsync();
    }

    public async Task<PropertyResponse> InsertAsync(PropertyInsertRequest request)
    {
        var property = new Property
        {
            Title = request.Title,
            Description = request.Description,
            Area = request.Area,
            NumberOfRooms = request.NumberOfRooms,
            YearBuilt = request.YearBuilt,
            HasParking = request.HasParking,
            HasLift = request.HasLift,
            Condition = request.Condition,
            CurrentPrice = request.CurrentPrice,
            UserId = request.UserId,
            LocationId = request.LocationId,
            PropertyTypeId = request.PropertyTypeId
        };

        _context.Properties.Add(property);

        await _context.SaveChangesAsync();

        return new PropertyResponse
        {
            Id = property.Id,
            Title = property.Title,
            Description = property.Description,
            Area = property.Area,
            NumberOfRooms = property.NumberOfRooms,
            YearBuilt = property.YearBuilt,
            HasParking = property.HasParking,
            HasLift = property.HasLift,
            Condition = property.Condition,
            CurrentPrice = property.CurrentPrice,
            UserId = property.UserId,
            LocationId = property.LocationId,
            PropertyTypeId = property.PropertyTypeId
        };
    }

    public async Task<PropertyResponse?> UpdateAsync(
        int id,
        PropertyUpdateRequest request)
    {
        var property = await _context.Properties
            .FirstOrDefaultAsync(p => p.Id == id);

        if (property == null)
        {
            return null;
        }

        property.Title = request.Title;
        property.Description = request.Description;
        property.Area = request.Area;
        property.NumberOfRooms = request.NumberOfRooms;
        property.YearBuilt = request.YearBuilt;
        property.HasParking = request.HasParking;
        property.HasLift = request.HasLift;
        property.Condition = request.Condition;
        property.CurrentPrice = request.CurrentPrice;
        property.LocationId = request.LocationId;
        property.PropertyTypeId = request.PropertyTypeId;

        await _context.SaveChangesAsync();

        return new PropertyResponse
        {
            Id = property.Id,
            Title = property.Title,
            Description = property.Description,
            Area = property.Area,
            NumberOfRooms = property.NumberOfRooms,
            YearBuilt = property.YearBuilt,
            HasParking = property.HasParking,
            HasLift = property.HasLift,
            Condition = property.Condition,
            CurrentPrice = property.CurrentPrice,
            UserId = property.UserId,
            LocationId = property.LocationId,
            PropertyTypeId = property.PropertyTypeId
        };
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var property = await _context.Properties
            .FirstOrDefaultAsync(p => p.Id == id);

        if (property == null)
        {
            return false;
        }

        _context.Properties.Remove(property);

        await _context.SaveChangesAsync();

        return true;
    }
}