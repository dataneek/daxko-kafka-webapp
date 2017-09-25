namespace WebApp.Models
{
    public class LocationChangeset : ILocationChangeset
    {
        public string LocationName { get; set; }
        public double? Longitude { get; set; }
        public double? Latitude { get; set; }
    }
}