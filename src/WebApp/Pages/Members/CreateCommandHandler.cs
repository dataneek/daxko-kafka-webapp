namespace WebApp.Pages.Members
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using MediatR;

    public class CreateCommandHandler : IRequestHandler<CreateCommand>
    {
        void IRequestHandler<CreateCommand>.Handle(CreateCommand message)
        {
            throw new NotImplementedException();
        }
    }
}