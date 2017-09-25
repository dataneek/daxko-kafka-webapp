namespace WebApp.Pages.Members
{
    using MediatR;

    public class CreateCommand : IRequest
    {
        public int NumberToCreate { get; set; }
    }
}