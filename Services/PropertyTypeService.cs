using EstatePredict.Api.Database;
using EstatePredict.DTOs;
using EstatePredict.Entities;
using EstatePredict.Requests;
using EstatePredict.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EstatePredict.Services;

/// <summary>
/// Handles all business logic related to property types.
/// </summary>
public class PropertyTypeService : IPropertyTypeService
{
    private readonly EstatePredictContext _db;

    public PropertyTypeService(EstatePredictContext db)
    {
        _db = db;
    }

    // ------------------------------------------------------------------ //
    //  READ
    // ------------------------------------------------------------------ //

    public async Task<IEnumerable<PropertyTypeDTO>> GetAllAsync()
    {
        return await _db.PropertyTypes
            .AsNoTracking()
            .Include(pt => pt.Properties)
            .OrderBy(pt => pt.Name)
            .Select(pt => MapToDto(pt))
            .ToListAsync();
    }

    public async Task<PropertyTypeDTO?> GetByIdAsync(int id)
    {
        var propertyType = await FindWithPropertiesAsync(id);
        return propertyType is null ? null : MapToDto(propertyType);
    }

    // ------------------------------------------------------------------ //
    //  WRITE
    // ------------------------------------------------------------------ //

    public async Task<PropertyTypeDTO> CreateAsync(CreatePropertyTypeRequest request)
    {
        await EnsureNameIsUniqueAsync(request.Name, excludeId: null);

        var propertyType = new PropertyType
        {
            Name = NormalizeName(request.Name)
        };

        _db.PropertyTypes.Add(propertyType);
        await _db.SaveChangesAsync();

        return MapToDto(propertyType);
    }

    public async Task<PropertyTypeDTO?> UpdateAsync(int id, UpdatePropertyTypeRequest request)
    {
        var propertyType = await FindWithPropertiesAsync(id);

        if (propertyType is null)
            return null;

        await EnsureNameIsUniqueAsync(request.Name, excludeId: id);

        propertyType.Name = NormalizeName(request.Name);
        await _db.SaveChangesAsync();

        return MapToDto(propertyType);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var propertyType = await _db.PropertyTypes
            .Include(pt => pt.Properties)
            .FirstOrDefaultAsync(pt => pt.Id == id);

        if (propertyType is null)
            return false;

        // Guard: refuse deletion when properties still reference this type.
        // A cascading delete here would silently corrupt property records.
        if (propertyType.Properties.Any())
            throw new InvalidOperationException(
                $"Cannot delete property type '{propertyType.Name}' because " +
                $"{propertyType.Properties.Count} property record(s) still reference it. " +
                "Reassign those properties first.");

        _db.PropertyTypes.Remove(propertyType);
        await _db.SaveChangesAsync();
        return true;
    }

    // ------------------------------------------------------------------ //
    //  PRIVATE HELPERS
    // ------------------------------------------------------------------ //

    /// <summary>
    /// Loads a PropertyType together with its Properties navigation.
    /// Used for both read and write operations that need the child count.
    /// </summary>
    private async Task<PropertyType?> FindWithPropertiesAsync(int id)
    {
        return await _db.PropertyTypes
            .AsNoTracking()
            .Include(pt => pt.Properties)
            .FirstOrDefaultAsync(pt => pt.Id == id);
    }

    /// <summary>
    /// Ensures no other PropertyType with the same name (case-insensitive) exists.
    /// Pass excludeId on update so the type being edited does not conflict with itself.
    /// </summary>
    private async Task EnsureNameIsUniqueAsync(string name, int? excludeId)
    {
        var normalized = NormalizeName(name);

        var duplicate = await _db.PropertyTypes
            .AnyAsync(pt =>
                pt.Name.ToLower() == normalized.ToLower() &&
                (excludeId == null || pt.Id != excludeId));

        if (duplicate)
            throw new InvalidOperationException(
                $"A property type named '{normalized}' already exists.");
    }

    /// <summary>
    /// Trims whitespace and applies title-case so storage is consistent
    /// (e.g. "  apartment " → "Apartment").
    /// </summary>
    private static string NormalizeName(string name)
    {
        var trimmed = name.Trim();
        return char.ToUpper(trimmed[0]) + trimmed[1..].ToLower();
    }

    private static PropertyTypeDTO  MapToDto(PropertyType pt) => new()
    {
        Id = pt.Id,
        Name = pt.Name,
        PropertyCount = pt.Properties.Count
    };
}