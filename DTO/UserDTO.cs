namespace EstatePredict.DTOs;

public record UserDTO(
    int Id,
    string FirstName,
    string LastName,
    string Email,
    string Role,
    DateTime CreatedAt
);

public record LoginResponseDTO(
    UserDTO User,
    string Token 
);