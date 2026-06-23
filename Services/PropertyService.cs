using EstatePredict.Api.Database;
using EstatePredict.DTO;
using EstatePredict.Entities;
using EstatePredict.Requests;
using EstatePredict.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EstatePredict.Services;

/// <summary>
/// Handles all business logic related to properties.
/// DbContext is only accessed here — never in controllers.
/// </summary>
public class PropertyService : IPropertyService
{
    private readonly EstatePredictContext _db;

    public PropertyService(EstatePredictContext db)
    {
        _db = db;
    }

    // ------------------------------------------------------------------ //
    //  READ
    // ------------------------------------------------------------------ //

    public async Task<IEnumerable<PropertyDTO>> GetAllAsync()
    {
        return await _db.Properties
            .AsNoTracking()
            .Include(p => p.Location)
            .Include(p => p.PropertyType)
            .Select(p => MapToDto(p))
            .ToListAsync();
    }

    public async Task<PropertyDTO?> GetByIdAsync(int id)
    {
        var property = await FindWithRelationsAsync(id);
        return property is null ? null : MapToDto(property);
    }

    // ------------------------------------------------------------------ //
    //  WRITE
    // ------------------------------------------------------------------ //

    public async Task<PropertyDTO> CreateAsync(CreatePropertyRequest request)
    {
        await ValidateForeignKeysAsync(request.LocationId, request.PropertyTypeId, request.UserId);

        var property = new Property
        {
            Title = request.Title,
            Description = request.Description,
            Area = request.Area,
            NumberOfRooms = request.NumberOfRooms,
            YearBuilt = request.YearBuilt,
            CurrentPrice = request.CurrentPrice,
            LocationId = request.LocationId,
            PropertyTypeId = request.PropertyTypeId,
            UserId = request.UserId
        };

        _db.Properties.Add(property);
        await _db.SaveChangesAsync();

        // Reload with navigation properties for the response DTO
        await _db.Entry(property).Reference(p => p.Location).LoadAsync();
        await _db.Entry(property).Reference(p => p.PropertyType).LoadAsync();

        return MapToDto(property);
    }

    public async Task<PropertyDTO?> UpdateAsync(int id, UpdatePropertyRequest request)
    {
        var property = await _db.Properties
            .Include(p => p.Location)
            .Include(p => p.PropertyType)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (property is null)
            return null;

        await ValidateForeignKeysAsync(request.LocationId, request.PropertyTypeId, userId: null);

        property.Title = request.Title;
        property.Description = request.Description;
        property.Area = request.Area;
        property.NumberOfRooms = request.NumberOfRooms;
        property.YearBuilt = request.YearBuilt;
        property.CurrentPrice = request.CurrentPrice;
        property.LocationId = request.LocationId;
        property.PropertyTypeId = request.PropertyTypeId;

        await _db.SaveChangesAsync();

        // Refresh navigation properties if FKs changed
        await _db.Entry(property).Reference(p => p.Location).LoadAsync();
        await _db.Entry(property).Reference(p => p.PropertyType).LoadAsync();

        return MapToDto(property);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var property = await _db.Properties.FindAsync(id);

        if (property is null)
            return false;

        _db.Properties.Remove(property);
        await _db.SaveChangesAsync();
        return true;
    }

    // ------------------------------------------------------------------ //
    //  PRIVATE HELPERS
    // ------------------------------------------------------------------ //

    /// <summary>
    /// Loads a property together with its navigation properties.
    /// Uses AsNoTracking for read-only queries to improve performance.
    /// </summary>
    private async Task<Property?> FindWithRelationsAsync(int id)
    {
        return await _db.Properties
            .AsNoTracking()
            .Include(p => p.Location)
            .Include(p => p.PropertyType)
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    /// <summary>
    /// Validates that referenced FK records actually exist before saving.
    /// Throws ArgumentException so the controller can surface a 400 response.
    /// userId is optional because update requests don't change ownership.
    /// </summary>
    private async Task ValidateForeignKeysAsync(int locationId, int propertyTypeId, int? userId)
    {
        var locationExists = await _db.Locations.AnyAsync(l => l.Id == locationId);
        if (!locationExists)
            throw new ArgumentException($"Location with id {locationId} does not exist.");

        var typeExists = await _db.PropertyTypes.AnyAsync(t => t.Id == propertyTypeId);
        if (!typeExists)
            throw new ArgumentException($"PropertyType with id {propertyTypeId} does not exist.");

        if (userId.HasValue)
        {
            var userExists = await _db.Users.AnyAsync(u => u.Id == userId.Value);
            if (!userExists)
                throw new ArgumentException($"User with id {userId.Value} does not exist.");
        }
    }

    /// <summary>
    /// Maps a tracked or no-tracked Property entity to its DTO.
    /// Kept as a static expression so EF can translate it in Select() projections.
    /// </summary>
    private static PropertyDTO MapToDto(Property p) => new()
    {
        Id = p.Id,
        Title = p.Title,
        Description = p.Description,
        Area = p.Area,
        NumberOfRooms = p.NumberOfRooms,
        YearBuilt = p.YearBuilt,
        CurrentPrice = p.CurrentPrice,
        LocationId = p.LocationId,
        Country = p.Location.Country,
        City = p.Location.City,
        Municipality = p.Location.Municipality,
        PropertyTypeId = p.PropertyTypeId,
        PropertyTypeName = p.PropertyType.Name,
        UserId = p.UserId
    };
}