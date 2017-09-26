namespace WebApp.Models
{
    using System;

    public class LocationCheckinUpdate : ILocationCheckinUpdate
    {
        public Location Location { get; set;}
        public Member Member { get; set;}
        public DateTime CheckinCompleted { get; set;}
    }
}