using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ProjectsMap.WebApi.DTOs;
using ProjectsMap.WebApi.Services.Abstract;

namespace ProjectsMap.WebApi.Controllers
{
    [RoutePrefix("api/seat")]
    public class SeatController : ApiController
    {
        private ISeatService _service;

        public SeatController(ISeatService service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("")]
        public IHttpActionResult GetAll()
        {
            return Ok(_service.GetAllSeats());
        }

        [HttpGet]
        [Route("{id:int}")]
        public IHttpActionResult Get(int id)
        {
            var seat = _service.GetSeat(id);

            if (seat != null)
                return Ok(seat);
            else
                return NotFound();
        }

        [HttpPost]
        [Route("")]
        public IHttpActionResult Post(SeatDto seatDto)
        {
            _service.Post(seatDto);
            return Ok();
        }
    }
}
