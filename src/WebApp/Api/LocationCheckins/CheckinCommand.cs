using System;
using MediatR;

namespace WebApp.Api.LocationCheckins
{
    public class CheckinCommand : IRequest, IRequest<CheckinResponse>
    {
        public int memberId { get; set; }
        public int locationId { get; set; }
    }
}
