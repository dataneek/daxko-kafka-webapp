namespace WebApp.Pages.Locations
{
    using MediatR;

    public class CreateCommand : IRequest
    {
        public int NumberToCreate { get; set; }
    }
}