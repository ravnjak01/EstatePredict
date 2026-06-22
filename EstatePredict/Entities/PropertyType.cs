using EstatePredict.Models.Entities;

namespace EstatePredict.Entities
{
    public class PropertyType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Property> Properties { get; set; } = new List<Property>();
        public ICollection<MarketData> MarketData { get; set; } = new List<MarketData>();
    }
}
