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
		private IEmployeeService _employeeService;


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

		[HttpPost]
		[Route("{seatId:int}/assignEmployee/{userId}")]
		public IHttpActionResult Post(int seatId, string userId)
		{
			var newSeatAssignedCorrectly = _service.assignSeat(seatId, userId);

			if (newSeatAssignedCorrectly)
				return Ok();
			else
				return NotFound();//TODO should be something smaarter
		}


	}
}
