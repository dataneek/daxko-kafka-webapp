using System;
using MediatR;

namespace WebApp.Api.LocationCheckins
{
    public class CheckinCommand : IRequest
    {
        public int MemberId { get; set; }
        public int LocationId { get; set; }
    }
}
