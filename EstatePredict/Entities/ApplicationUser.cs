using EstatePredict.Models.Entities;

namespace EstatePredict.Entities
{
    public class ApplicationUser
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string Role { get; set; }
        public DateTime CreatedAt { get; set; }
        public ICollection<Property> Properties { get; set; } = new List<Property>();
        public ICollection<Prediction> Predictions { get; set; } = new List<Prediction>();
    }
}
