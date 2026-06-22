using EstatePredict.Models.Entities;

namespace EstatePredict.Entities
{
    public class Location
    {
        public int Id { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Municipality { get; set; }
        public ICollection<Property> Properties { get; set; } = new List<Property>();
        public ICollection<MarketData> MarketData { get; set; } = new List<MarketData>();

    }
}
