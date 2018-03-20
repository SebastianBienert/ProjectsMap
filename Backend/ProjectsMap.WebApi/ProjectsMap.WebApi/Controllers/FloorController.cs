using ProjectsMap.WebApi.DTOs;
using ProjectsMap.WebApi.Services.Abstract;
using ProjectsMap.WebApi.Services.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ProjectsMap.WebApi.Controllers
{
	[RoutePrefix("api/floor")]
	public class FloorController : ApiController
	{
		private IFloorService _service;

		public FloorController(IFloorService floorService)
		{
			_service = floorService;
		}

		[HttpGet]
		[Route("")]
		public IHttpActionResult GetAll()
		{
			return Ok(_service.GetFloorsList().Select(x => new { x.Id, x.BuildingId, x.Description }));
		}


		[HttpGet]
		[Route("{id:int}")]
		public IHttpActionResult Get(int id)
		{
			var floor = _service.GetFloor(id);

			if (floor != null)
				return Ok(floor);
			else
				return NotFound();
		}

		[HttpPut]
		[Route("{id:int}")]
		public IHttpActionResult Put(FloorDto floorDto)
		{
			_service.Update(floorDto);

			return Ok();
		}

		[HttpPost]
		[Route("")]
		public IHttpActionResult Post([FromBody] FloorDto floorDto)
		{
			int createdId = _service.Post(floorDto);
			return Ok();//for now
		}

	}
}
