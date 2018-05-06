using ProjectsMap.WebApi.Repositories.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProjectsMap.WebApi.DTOs;
using ProjectsMap.WebApi.Models;
using System.Data.Entity;

namespace ProjectsMap.WebApi.Repositories.Concrete
{
	public class BuildingRepository : IBuildingRepository
	{
		public IEnumerable<Building> Buildings
		{
			get
			{
				using (var dbContext = new EfDbContext())
				{
					var buildings = dbContext.Buildings
						.Include(b => b.Floors)
						.ToList();

					return buildings;
				}
			}
		}

		public int Add(BuildingDto buildingDto)
		{
			using (var dbContext = new EfDbContext())
			{

				Building building = new Building()
				{
					Address = buildingDto.Address,
				};
				dbContext.Buildings.Add(building);
				dbContext.SaveChanges();
				return building.BuildingId;
			}
		}

		public void Delete(int buildingId)
		{
			throw new NotImplementedException();
		}

		public Building Get(int id)
		{
			using (var dbContext = new EfDbContext())
			{
				var building = dbContext.Buildings
					.Include(b => b.Floors)
					.FirstOrDefault(b => b.BuildingId == id);
				return building;

			}
		}

		public void Update(BuildingDto buildingDto)
		{
			throw new NotImplementedException();
		}
	}
}