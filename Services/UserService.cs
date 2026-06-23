using EstatePredict.Api.Database;
using EstatePredict.DTOs;
using EstatePredict.Entities;
using EstatePredict.Requests;
using EstatePredict.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using BC = BCrypt.Net.BCrypt;

namespace EstatePredict.Services.Implementations;

public class UserService : IUserService
{
    private readonly EstatePredictContext _context;
    private readonly IJtokenService _jtokenService;

    public UserService(EstatePredictContext context, IJtokenService jtokenService)
    {
        _context = context;
        _jtokenService = jtokenService;
    }

    public async Task<UserDTO> RegisterAsync(RegisterUserRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Email) || string.IsNullOrWhiteSpace(request.Password))
            throw new ArgumentException("Email and password are required.");

        var emailExists = await _context.Users.AnyAsync(u => u.Email.ToLower() == request.Email.ToLower());
        if (emailExists)
            throw new InvalidOperationException($"User with email {request.Email} already exists.");

        var user = new ApplicationUser
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email.ToLower(),
            PasswordHash = BC.HashPassword(request.Password), // Sigurno heširanje lozinke
            Role = "User", // Defaultna uloga za novoregistrovane
            CreatedAt = DateTime.UtcNow
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return new UserDTO(user.Id, user.FirstName, user.LastName, user.Email, user.Role, user.CreatedAt);
    }

    public async Task<LoginResponseDTO> LoginAsync(LoginRequest request)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email.ToLower() == request.Email.ToLower());

        if (user is null || !BC.Verify(request.Password, user.PasswordHash))
            throw new UnauthorizedAccessException("Invalid email or password.");

        var userDto = new UserDTO(user.Id, user.FirstName, user.LastName, user.Email, user.Role, user.CreatedAt);

        var token = _jtokenService.CreateToken(user);

        return new LoginResponseDTO(userDto, token);
    }

    public async Task<UserDTO?> GetByIdAsync(int id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user is null) return null;

        return new UserDTO(user.Id, user.FirstName, user.LastName, user.Email, user.Role, user.CreatedAt);
    }
}