using ProjectsMap.WebApi.DTOs;
using ProjectsMap.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectsMap.WebApi.Repositories.Abstract
{
	public interface IFloorRepository
	{

		IEnumerable<Floor> Floors{ get; }

		Floor Get(int id);

		int Add(FloorDto floorDto);

		void Delete(int floorId);

		void Update(Floor floor);

	}
}