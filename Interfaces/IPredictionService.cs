using EstatePredict.DTOs;
using EstatePredict.Requests;

namespace EstatePredict.Services.Interfaces;

/// <summary>
/// Defines all operations for creating and retrieving property price predictions.
/// </summary>
public interface IPredictionService
{
    /// <summary>
    /// Runs the prediction model for the given property and target year,
    /// persists the result, and returns it as a DTO.
    /// </summary>
    Task<PredictionDTO> CreateAsync(CreatePredictionRequest request);

    /// <summary>
    /// Returns a single prediction by its identifier.
    /// Returns null when the prediction does not exist.
    /// </summary>
    Task<PredictionDTO?> GetByIdAsync(int id);

    /// <summary>
    /// Returns all predictions that belong to a specific user.
    /// Returns an empty collection when the user has no predictions.
    /// </summary>
    Task<IEnumerable<PredictionDTO>> GetByUserIdAsync(int userId);
}