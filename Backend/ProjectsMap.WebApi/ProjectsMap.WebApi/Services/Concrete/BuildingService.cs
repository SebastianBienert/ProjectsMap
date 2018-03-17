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
	public class BuildingService : IBuildingService
	{
		private readonly IBuildingRepository _buildingRepository;

		public BuildingService(IBuildingRepository buildingRepository)
		{
			_buildingRepository = buildingRepository;
		}

		public void Delete(int id)
		{
			throw new NotImplementedException();
		}

		public IEnumerable<BuildingDto> GetAllBuildings()
		{
			return _buildingRepository.Buildings.Select(x => DTOMapper.GetBuildingDto(x));
		}

		public BuildingDto GetBuilding(int id)
		{
			throw new NotImplementedException();
		}

		public IEnumerable<BuildingDto> GetBuildingsList(int companyId)
		{
			return _buildingRepository.Buildings.Where(x => x.CompanyId == companyId).Select(x => DTOMapper.GetBuildingDto(x));
		}

		public int Post(BuildingDto buildingDto)
		{
			return _buildingRepository.Add(buildingDto);
		}

		public void Update(BuildingDto buildingDto)
		{
			throw new NotImplementedException();
		}
	}
}