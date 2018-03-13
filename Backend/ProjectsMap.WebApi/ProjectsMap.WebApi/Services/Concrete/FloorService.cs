using ProjectsMap.WebApi.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProjectsMap.WebApi.DTOs;
using ProjectsMap.WebApi.Repositories.Abstract;
using ProjectsMap.WebApi.Mappers;

namespace ProjectsMap.WebApi.Services.Concrete
{
	public class FloorService : IFloorService
	{

		private readonly IFloorRepository _floorRepository;

		public FloorService(IFloorRepository floorRepository)
		{
			_floorRepository = floorRepository;
		}

		public void Delete(int id)
		{
			throw new NotImplementedException();
		}

		public IEnumerable<FloorDto> GetAllFloors()
		{
			return _floorRepository.Floors.Select(x => DTOMapper.GetFloorDto(x));
		}
		public IEnumerable<FloorDto> GetFloorsList()
		{
			return _floorRepository.Floors.Select(x => DTOMapper.GetFloorDtoListElement(x));
		}

		public IEnumerable<FloorDto> GetFloorsList(int buildingId)
		{
			return _floorRepository.Floors.Where(x => x.BuildingId == buildingId).Select(x => DTOMapper.GetFloorDtoListElement(x));
		}

		public FloorDto GetFloor(int id)
		{
			var floor = _floorRepository.Get(id);
			if (floor == null)
				return null;

			return DTOMapper.GetFloorDto(floor);
		}

		public int Post(FloorDto floorDto)
		{
			return _floorRepository.Add(floorDto);
		}

		public void Update(FloorDto floorDto)
		{
			throw new NotImplementedException();
		}
	}
}