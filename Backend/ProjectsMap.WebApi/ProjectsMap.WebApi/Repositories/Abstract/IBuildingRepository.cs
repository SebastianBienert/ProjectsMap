using ProjectsMap.WebApi.DTOs;
using ProjectsMap.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectsMap.WebApi.Repositories.Abstract
{
	public interface IBuildingRepository
	{
		IEnumerable<Building> Buildings { get; }
		Building Get(int id);

		int Add(BuildingDto buildingDto);

		void Delete(int buildingId);

		void Update(BuildingDto buildingDto);
	}
}
