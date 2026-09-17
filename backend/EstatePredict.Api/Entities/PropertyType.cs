namespace EstatePredict.Api.Entities;

public class PropertyType
{
	public int Id { get; set; }

	public string Name { get; set; } = null!;

	public ICollection<Property> Properties { get; set; } = new List<Property>();
}