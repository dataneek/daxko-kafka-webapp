using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models;

namespace WebApp.Api.Locations
{
    [Route("api/[controller]")]
    public class LocationsController : Controller
    {
        private readonly AppDbContext context;

        public LocationsController(AppDbContext context)
        {
            this.context = context;
        }
        
        public object Get()
        {
            return context.Locations.Select(x => new
            {
                LocationId = x.LocationId,
                LocationName = x.LocationName
            }).ToList();
        }
    }
}