namespace EstatePredict.Requests;

public record RegisterUserRequest(
    string FirstName,
    string LastName,
    string Email,
    string Password
);

public record LoginRequest(
    string Email,
    string Password
);