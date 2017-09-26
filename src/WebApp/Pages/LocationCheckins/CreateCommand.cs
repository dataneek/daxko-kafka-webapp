using MediatR;

namespace WebApp.Pages.LocationCheckins
{
    public class CreateCommand : IRequest
    {
        public int NumberToCreate { get; set; }
    }
}