namespace WebApp.Models
{
    public class LocationUpdate : ILocationUpdate
    {
        public string LocationName { get; set; }
        public double? Longitude { get; set; }
        public double? Latitude { get; set; }
    }
}