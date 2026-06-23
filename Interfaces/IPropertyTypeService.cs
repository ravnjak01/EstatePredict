using EstatePredict.DTOs;
using EstatePredict.Requests;

namespace EstatePredict.Services.Interfaces;

/// <summary>
/// Defines all operations for managing property types (e.g. Apartment, House, Land).
/// </summary>
public interface IPropertyTypeService
{
    /// <summary>
    /// Returns all property types including how many properties reference each one.
    /// </summary>
    Task<IEnumerable<PropertyTypeDTO>> GetAllAsync();

    /// <summary>
    /// Returns a single property type by its identifier.
    /// Returns null when no match is found.
    /// </summary>
    Task<PropertyTypeDTO?> GetByIdAsync(int id);

    /// <summary>
    /// Creates a new property type.
    /// Throws InvalidOperationException when a type with the same name already exists.
    /// </summary>
    Task<PropertyTypeDTO> CreateAsync(CreatePropertyTypeRequest request);

    /// <summary>
    /// Updates an existing property type's name.
    /// Returns the updated DTO, or null when the type does not exist.
    /// Throws InvalidOperationException when another type already owns that name.
    /// </summary>
    Task<PropertyTypeDTO?> UpdateAsync(int id, UpdatePropertyTypeRequest request);

    /// <summary>
    /// Deletes a property type by its identifier.
    /// Returns true on success, false when it does not exist.
    /// Throws InvalidOperationException when properties still reference this type.
    /// </summary>
    Task<bool> DeleteAsync(int id);
}