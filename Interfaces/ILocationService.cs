using EstatePredict.DTOs;
using EstatePredict.Requests;

namespace EstatePredict.Services.Interfaces;

public interface ILocationService
{
    Task<IEnumerable<LocationDTO>> GetAllAsync();
    Task<LocationDTO> CreateAsync(CreateLocationRequest request);
    Task<LocationDTO?> GetByIdAsync(int id); 
}