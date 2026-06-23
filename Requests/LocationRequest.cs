namespace EstatePredict.Requests;

public record CreateLocationRequest(
    string Country,
    string City,
    string Municipality
);