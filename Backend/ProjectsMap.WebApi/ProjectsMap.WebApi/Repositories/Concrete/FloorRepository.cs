using ProjectsMap.WebApi.Repositories.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProjectsMap.WebApi.Models;
using ProjectsMap.WebApi.DTOs;
using System.Data.Entity;
using ProjectsMap.WebApi.Repositories.EntityFramework;

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
						.Include(f => f.Walls)
						.Include(f => f.Rooms.Select(x => x.Walls))
						.Include(f => f.Building)
						.Include(f => f.Rooms.Select(s => s.Seats))
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
				dbContext.Walls.Load();
				dbContext.Rooms.Load();

				ICollection<Room> roomsList = new List<Room>();
				foreach (RoomDto roomDto in floorDto.Rooms)
				{
					ICollection<Wall> roomWallsList = new List<Wall>();
					for (int i = 0; i < roomDto.Walls.Count(); i++)
					{
						Wall wall = new Wall()
						{
							StartVertexX = roomDto.Walls.ElementAt(i).StartVertex.X,
							StartVertexY = roomDto.Walls.ElementAt(i).StartVertex.Y,
							EndVertexX = roomDto.Walls.ElementAt(i).EndVertex.X,
							EndVertexY = roomDto.Walls.ElementAt(i).EndVertex.Y,
						};
						//dbContext.Walls.Local.Add(wall);
						roomWallsList.Add(wall);
					}
					ICollection<Seat> seatList = new List<Seat>();
					for (int i = 0; i < roomDto.Seats.Count(); i++)
					{
						Vertex seatVertex = new Vertex(roomDto.Seats.ElementAt(i).Vertex.X, roomDto.Seats.ElementAt(i).Vertex.Y);

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
					Wall wall = new Wall()
					{
						StartVertexX = floorDto.Walls.ElementAt(i).StartVertex.X,
						StartVertexY = floorDto.Walls.ElementAt(i).StartVertex.Y,
						EndVertexX = floorDto.Walls.ElementAt(i).EndVertex.X,
						EndVertexY = floorDto.Walls.ElementAt(i).EndVertex.Y,
					};
					dbContext.Walls.Local.Add(wall);
					wallsList.Add(wall);
				}

				Floor floor = new Floor()
				{
					Rooms = roomsList,
					Walls = wallsList,
					BuildingId = floorDto.BuildingId,
					Description = floorDto.Description,	
					FloorNumber = floorDto.FloorNumber
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
					.Include(f => f.Walls)
					.Include(f => f.Rooms.Select(x => x.Walls))
					.Include(f => f.Building)
					.Include(f => f.Rooms.Select(s => s.Seats))
					.FirstOrDefault(x => x.FloorId == id);
				return floor;

			}
		}

		public void Update(FloorDto floorDto)
		{
			using (var dbContext = new EfDbContext())
			{

				var existingFloor = dbContext.Floors.Where(F => F.FloorId == floorDto.Id).FirstOrDefault();
				if (existingFloor != null)
				{
					dbContext.Walls.Load();
					dbContext.Rooms.Load();
					ICollection<Room> roomsList = new List<Room>();
					foreach (RoomDto roomDto in floorDto.Rooms)
					{
						

						
						ICollection<Wall> roomWallsList = new List<Wall>();
						for (int i = 0; i < roomDto.Walls.Count(); i++)
						{
							Wall wall = new Wall()
							{
								StartVertexX = roomDto.Walls.ElementAt(i).StartVertex.X,
								StartVertexY = roomDto.Walls.ElementAt(i).StartVertex.Y,
								EndVertexX = roomDto.Walls.ElementAt(i).EndVertex.X,
								EndVertexY = roomDto.Walls.ElementAt(i).EndVertex.Y,
							};
							//dbContext.Walls.Local.Add(wall);
							roomWallsList.Add(wall);
						}
						ICollection<Seat> seatList = new List<Seat>();
						for (int i = 0; i < roomDto.Seats.Count(); i++)
						{
							Vertex seatVertex = new Vertex(roomDto.Seats.ElementAt(i).Vertex.X, roomDto.Seats.ElementAt(i).Vertex.Y);

							Seat seat = new Seat(seatVertex);
							if (roomDto.Seats.ElementAt(i).Id != 0) seat.SeatId = roomDto.Seats.ElementAt(i).Id;
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
						Wall wall = new Wall()
						{
							StartVertexX = floorDto.Walls.ElementAt(i).StartVertex.X,
							StartVertexY = floorDto.Walls.ElementAt(i).StartVertex.Y,
							EndVertexX = floorDto.Walls.ElementAt(i).EndVertex.X,
							EndVertexY = floorDto.Walls.ElementAt(i).EndVertex.Y,
						};
						dbContext.Walls.Local.Add(wall);
						wallsList.Add(wall);
					}

					existingFloor.FloorNumber = floorDto.FloorNumber;
					existingFloor.Description = floorDto.Description;
					existingFloor.Rooms = roomsList;
					existingFloor.Walls = wallsList;
					int count = dbContext.SaveChanges();
					count++;
				}
			}
		}
	}
}