using EstatePredict.Api.DTO.PropertyType;
using EstatePredict.Api.Requests.PropertyType;

namespace EstatePredict.Api.Interfaces;

public interface IPropertyTypeService
{
    Task<List<PropertyTypeResponse>> GetAllAsync();

    Task<PropertyTypeResponse?> GetByIdAsync(int id);

    Task<PropertyTypeResponse> InsertAsync(PropertyTypeInsertRequest request);

    Task<PropertyTypeResponse?> UpdateAsync(
        int id,
        PropertyTypeUpdateRequest request);

    Task<bool> DeleteAsync(int id);
}