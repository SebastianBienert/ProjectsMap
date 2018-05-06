using ProjectsMap.WebApi.DTOs;
using ProjectsMap.WebApi.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace ProjectsMap.WebApi.Controllers
{
	[RoutePrefix("api/buildings")]
	public class BuildingController : ApiController
	{
		private IBuildingService _buildingService;
		private IFloorService _floorService;

		public BuildingController(IBuildingService buildingService, IFloorService floorService)
		{
			_buildingService = buildingService;
			_floorService = floorService;
		}

		[HttpGet]
		[Route("{id:int}/floors")]
		public IHttpActionResult GetBuildingFloorsList(int id)
		{
			var buildingFloorList = _floorService.GetFloorsList(id);
			return Ok(buildingFloorList);
			/*if (companyBuildingsList)
				return Ok(companyDto);
			else
				return NotFound();*/
		}

		[HttpPost]
		[Route("")]
		public IHttpActionResult Post([FromBody] BuildingDto buildingDto)
		{
			int createdId = _buildingService.Post(buildingDto);
			return Ok(createdId);
			/*if (companyBuildingsList)
				return Ok(companyDto);
			else
				return NotFound();*/
		}

		[HttpGet]
		[Route("")]
		public IHttpActionResult Get()
		{
			var companyBuildingsList = _buildingService.GetAllBuildings();
			return Ok(companyBuildingsList);
		}

	}
}