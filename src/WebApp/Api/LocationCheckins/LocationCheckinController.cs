using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApp.Api.LocationCheckins
{
    [Route("api/[controller]")]
    public class LocationCheckinController : Controller
    {
        public IMediator mediator { get; }

        public LocationCheckinController(IMediator mediator)
        {
            this.mediator = mediator;
        }
                
        // POST api/values
        [HttpPost]
        public CheckinResponse Post([FromBody]CheckinCommand value)
        {
            var response = this.mediator.Send<CheckinResponse>(value);
            return response.Result;
        }
    }
}
