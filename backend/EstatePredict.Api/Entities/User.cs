namespace EstatePredict.Api.Entities;

public class User
{
    public int Id { get; set; }

    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;

    public string Email { get; set; } = null!;
    public string PasswordHash { get; set; } = null!;

    public string Role { get; set; } = "User";

    public DateTime CreatedAt { get; set; }

    public ICollection<Property> Properties { get; set; } = new List<Property>();
    public ICollection<Prediction> Predictions { get; set; } = new List<Prediction>();
}