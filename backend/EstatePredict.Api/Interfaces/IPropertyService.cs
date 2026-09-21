using EstatePredict.Api.DTO.Property;
using EstatePredict.Api.Requests.Property;

namespace EstatePredict.Api.Interfaces;

public interface IPropertyService
{
    Task<List<PropertyResponse>> GetAllAsync();

    Task<PropertyResponse?> GetByIdAsync(int id);

    Task<PropertyResponse> InsertAsync(PropertyInsertRequest request);

    Task<PropertyResponse?> UpdateAsync(int id, PropertyUpdateRequest request);

    Task<bool> DeleteAsync(int id);
}