namespace WebApp.Models
{
    using System;

    public interface ILocationCheckinUpdate
    {
        Location Location { get; }
        Member Member { get; }
        DateTime CheckinCompleted { get; }
    }
}