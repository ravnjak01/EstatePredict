using EstatePredict.DTOs;
using EstatePredict.Requests;

namespace EstatePredict.Services.Interfaces;

public interface IUserService
{
    Task<UserDTO> RegisterAsync(RegisterUserRequest request);
    Task<LoginResponseDTO> LoginAsync(LoginRequest request);
    Task<UserDTO?> GetByIdAsync(int id);
}