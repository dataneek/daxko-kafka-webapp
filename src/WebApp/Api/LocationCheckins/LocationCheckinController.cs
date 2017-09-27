﻿using System;
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
        
//        // GET: api/values
//        [HttpGet]
//        public IEnumerable<string> Get()
//        {
//            return new string[] { "value1", "value2" };
//        }
//
//        // GET api/values/5
//        [HttpGet("{id}")]
//        public string Get(int id)
//        {
//            return "value";
//        }
//
//        
//
//        // PUT api/values/5
//        [HttpPut("{id}")]
//        public void Put(int id, [FromBody]string value)
//        {
//        }
//
//        // DELETE api/values/5
//        [HttpDelete("{id}")]
//        public void Delete(int id)
//        {
//        }
        
        // POST api/values
        [HttpPost]
        public void Post([FromBody]CheckinCommand value)
        {
            this.mediator.Send(value);
        }
    }
}
