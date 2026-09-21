using EstatePredict.Api.DTO.Location;
using EstatePredict.Api.Requests.Location;

namespace EstatePredict.Api.Interfaces;

public interface ILocationService
{
    Task<List<LocationResponse>> GetAllAsync();

    Task<LocationResponse?> GetByIdAsync(int id);

    Task<LocationResponse> InsertAsync(LocationInsertRequest request);

    Task<LocationResponse?> UpdateAsync(int id, LocationUpdateRequest request);

    Task<bool> DeleteAsync(int id);
}