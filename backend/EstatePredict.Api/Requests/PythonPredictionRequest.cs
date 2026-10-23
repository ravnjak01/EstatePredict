namespace EstatePredict.Api.Requests
{
    public class PythonPredictionRequest
    {

        public decimal Area { get; set; }

        public decimal NumberOfRooms { get; set; }

        public int YearBuilt { get; set; }

        public bool HasParking { get; set; }

        public bool HasLift { get; set; }

        public string Condition { get; set; } = null!;

        public string Country { get; set; } = null!;

        public string City { get; set; } = null!;

        public string Municipality { get; set; } = null!;

        public string PropertyType { get; set; } = null!;
    }
}
