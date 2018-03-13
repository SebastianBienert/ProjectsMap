using ProjectsMap.WebApi.Repositories.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProjectsMap.WebApi.Models;
using ProjectsMap.WebApi.DTOs;
using System.Data.Entity;

namespace ProjectsMap.WebApi.Repositories.Concrete
{
	public class FloorRepository : IFloorRepository

	{
		public IEnumerable<Floor> Floors
	{
		get

			{
				using (var dbContext = new EfDbContext())
				{
					var floors = dbContext.Floors
						.Include(f => f.Walls.Select(w => w.StartVertex))
						.Include((f => f.Walls.Select(w => w.EndVertex)))
						.Include(f => f.Rooms.Select(x => x.Walls.Select(v => v.StartVertex)))
						.Include(f => f.Rooms.Select(x => x.Walls.Select(v => v.EndVertex)))
						.Include(f => f.Building)
						.Include(f => f.Rooms.Select(s => s.Seats.Select(v => v.Vertex)))
						.ToList()
						;
					return floors;
				}
			}
	}

		public int Add(FloorDto floorDto)
		{
			using (var dbContext = new EfDbContext())
			{
				dbContext.Vertexes.Load();
				dbContext.Walls.Load();
				dbContext.Rooms.Load();

				ICollection<Room> roomsList = new List<Room>();
				foreach (RoomDto roomDto in floorDto.Rooms)
				{
					ICollection<Wall> roomWallsList = new List<Wall>();
					for (int i = 0; i < roomDto.Walls.Count(); i++)
					{
						var x = roomDto.Walls.ElementAt(i).StartVertex.X;
						var y = roomDto.Walls.ElementAt(i).StartVertex.Y;
						Vertex start = dbContext.Vertexes.Local.Where(v => v.X == x && v.Y == y).FirstOrDefault();
						if (start == null)
						{
							start = new Vertex(x, y);
							dbContext.Vertexes.Local.Add(start);
						}

						x = roomDto.Walls.ElementAt(i).EndVertex.X;
						y = roomDto.Walls.ElementAt(i).EndVertex.Y;
						Vertex end = dbContext.Vertexes.Local.Where(v => v.X == x && v.Y == y).FirstOrDefault();
						if (end == null)
						{
							end = new Vertex(x, y);
							dbContext.Vertexes.Local.Add(end);
						}
						Wall wall = new Wall(start, end);
						dbContext.Walls.Local.Add(wall);
						roomWallsList.Add(wall);
					}
					ICollection<Seat> seatList = new List<Seat>();
					for (int i = 0; i < roomDto.Seats.Count(); i++)
					{
						var x = roomDto.Seats.ElementAt(i).X;
						var y = roomDto.Seats.ElementAt(i).Y;
						Vertex seatVertex = dbContext.Vertexes.Local.Where(v => v.X == x && v.Y == y).FirstOrDefault();
						if (seatVertex == null)
						{
							seatVertex = new Vertex(x, y);
							dbContext.Vertexes.Local.Add(seatVertex);
						}
						Seat seat = new Seat(seatVertex);
						seatList.Add(seat);
					}
						var Room = new Room()
					{
						FloorId = floorDto.Id,
						Walls = roomWallsList,
						Seats = seatList
					};
					dbContext.Rooms.Local.Add(Room);
					roomsList.Add(Room);
				}

				ICollection<Wall> wallsList = new List<Wall>();
				for (int i = 0; i < floorDto.Walls.Count(); i++)
				{
					var x = floorDto.Walls.ElementAt(i).StartVertex.X;
					var y = floorDto.Walls.ElementAt(i).StartVertex.Y;
					Vertex start = dbContext.Vertexes.Local.Where(v => v.X == x && v.Y == y).FirstOrDefault();
					if (start == null)
					{
						start = new Vertex(x, y);
						dbContext.Vertexes.Local.Add(start);
					}

					x = floorDto.Walls.ElementAt(i).EndVertex.X;
					y = floorDto.Walls.ElementAt(i).EndVertex.Y;
					Vertex end = dbContext.Vertexes.Local.Where(v => v.X == x && v.Y == y).FirstOrDefault();
					if (end == null)
					{
						end = new Vertex(x, y);
						dbContext.Vertexes.Local.Add(end);
					}
					Wall wall = new Wall(start, end);
					dbContext.Walls.Local.Add(wall);
					wallsList.Add(wall);
				}

				Floor floor = new Floor()
				{
					Rooms = roomsList,
					Walls = wallsList,
					BuildingId = floorDto.BuildingId,
					Description = floorDto.Description,	
				};

				dbContext.Floors.Add(floor);
				dbContext.SaveChanges();
				return floor.FloorId;
			}
		}

		public void Delete(int floorId)
		{
			throw new NotImplementedException();
		}

		public Floor Get(int id)
		{
			using (var dbContext = new EfDbContext())
			{
				var floor = dbContext.Floors
					.Include(f => f.Walls.Select(w => w.StartVertex))
					.Include((f => f.Walls.Select(w => w.EndVertex)))
					.Include(f => f.Rooms.Select(x => x.Walls.Select(v => v.StartVertex)))
					.Include(f => f.Rooms.Select(x => x.Walls.Select(v => v.EndVertex)))
					.Include(f => f.Building)
					.Include(f => f.Rooms.Select(s => s.Seats.Select(v => v.Vertex)))
					.FirstOrDefault(x => x.FloorId == id);
				return floor;

			}
		}

		public void Update(FloorDto floorDto)
		{
			throw new NotImplementedException();
		}
	}
}