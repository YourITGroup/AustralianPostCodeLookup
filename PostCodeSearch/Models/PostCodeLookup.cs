namespace PostCodeSearch.Models
{
    public class PostCodeLookup
    {
        public int Id { get; set; }
        public string Postcode { get; set; }
        public string Locality { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public string Region { get; set; }
        public string State { get; set; }
        public string Electorate { get; set; }
        public string ElectorateRating { get; set; }
    }
}
