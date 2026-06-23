using EstatePredict.DTO;
using EstatePredict.Requests;

namespace EstatePredict.Services.Interfaces;

/// <summary>
/// Defines all operations available for managing properties.
/// </summary>
public interface IPropertyService
{
    /// <summary>
    /// Returns all properties with their location and type details.
    /// </summary>
    Task<IEnumerable<PropertyDTO>> GetAllAsync();

    /// <summary>
    /// Returns a single property by its identifier.
    /// Returns null when the property does not exist.
    /// </summary>
    Task<PropertyDTO?> GetByIdAsync(int id);

    /// <summary>
    /// Creates a new property and returns the persisted DTO.
    /// </summary>
    Task<PropertyDTO> CreateAsync(CreatePropertyRequest request);

    /// <summary>
    /// Updates an existing property.
    /// Returns the updated DTO, or null when the property does not exist.
    /// </summary>
    Task<PropertyDTO?> UpdateAsync(int id, UpdatePropertyRequest request);

    /// <summary>
    /// Deletes a property by its identifier.
    /// Returns true on success, false when the property does not exist.
    /// </summary>
    Task<bool> DeleteAsync(int id);
}