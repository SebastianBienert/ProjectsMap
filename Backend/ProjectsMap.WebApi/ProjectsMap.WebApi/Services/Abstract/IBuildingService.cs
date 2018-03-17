using ProjectsMap.WebApi.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectsMap.WebApi.Services.Abstract
{
	public interface IBuildingService
	{
		void Delete(int id);
		IEnumerable<BuildingDto> GetAllBuildings();
		BuildingDto GetBuilding(int id);
		int Post(BuildingDto buildingDto);
		void Update(BuildingDto buildingDto);
		IEnumerable<BuildingDto> GetBuildingsList(int companyId);
	}
}
