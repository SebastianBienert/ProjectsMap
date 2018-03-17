using ProjectsMap.WebApi.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectsMap.WebApi.Services.Abstract
{
	public interface IFloorService
	{
		void Delete(int id);
		IEnumerable<FloorDto> GetAllFloors();
		FloorDto GetFloor(int id);
		int Post(FloorDto floorDto);
		void Update(FloorDto floorDto);
		IEnumerable<FloorDto> GetFloorsList();
		IEnumerable<FloorDto> GetFloorsList(int buildingId);
	}
}
