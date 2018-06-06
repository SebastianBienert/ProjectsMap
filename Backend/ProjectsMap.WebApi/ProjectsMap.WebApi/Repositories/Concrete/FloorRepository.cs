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
					FloorNumber = floorDto.FloorNumber,
					XPhoto = floorDto.XPhoto,
					YPhoto = floorDto.YPhoto
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

		public void Update(Floor floor)
		{
			using (var dbContext = new EfDbContext())
			{
				dbContext.Entry(floor).State = EntityState.Modified;
				dbContext.SaveChanges();
			}
		}

		public void Update(FloorDto floorDto)
		{
			using (var dbContext = new EfDbContext())
			{

				var existingFloor = dbContext.Floors.Include(f => f.Walls)
					.Include(f => f.Rooms.Select(x => x.Walls))
					.Include(f => f.Building)
					.Include(f => f.Rooms.Select(s => s.Seats)).Where(F => F.FloorId == floorDto.Id).FirstOrDefault();
				if (existingFloor != null)
				{
					dbContext.Walls.Load();
					dbContext.Rooms.Load();
					dbContext.Seats.Load();

					ICollection<Wall> wallsList = new List<Wall>();
					foreach (WallDto wallDto in floorDto.Walls)
					{
						if(wallDto.StateChanged != null)
						{
							if(wallDto.StateChanged.Equals("deleted"))
							{
								var wallToDelete = dbContext.Walls.FirstOrDefault(w => w.WallId == wallDto.Id);
								if(wallToDelete != null)
								{
									dbContext.Walls.Remove(wallToDelete);
								}
							}
							else if(wallDto.StateChanged.Equals("added"))
							{
								Wall wall = new Wall()
								{
									StartVertexX = wallDto.StartVertex.X,
									StartVertexY = wallDto.StartVertex.Y,
									EndVertexX = wallDto.EndVertex.X,
									EndVertexY = wallDto.EndVertex.Y,
								};
								dbContext.Walls.Local.Add(wall);
								existingFloor.Walls.Add(wall);
							}
						}
					}

					ICollection<Room> roomsList = new List<Room>();
					foreach (RoomDto roomDto in floorDto.Rooms)
					{
						if (roomDto.StateChanged != null)
						{
							if (roomDto.StateChanged.Equals("deleted"))
							{
								var roomToDelete = dbContext.Rooms.Include(f => f.Walls).Include(f => f.Seats).FirstOrDefault(x => x.RoomId == roomDto.Id);
								if (roomToDelete != null)
								{
									foreach (var wall in roomToDelete.Walls.ToList())
										dbContext.Walls.Remove(wall);
									foreach (var seat in roomToDelete.Seats.ToList())
										dbContext.Seats.Remove(seat);
									dbContext.Entry(roomToDelete).State = EntityState.Deleted;
								}
							} else if(roomDto.StateChanged.Equals("added"))
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
								existingFloor.Rooms.Add(Room);
							} else if(roomDto.StateChanged.Equals("modified"))
							{
								var modifiedRoom = dbContext.Rooms.Include(f => f.Walls).Include(f => f.Seats).FirstOrDefault(x => x.RoomId == roomDto.Id);
								if (modifiedRoom != null)
								{
									foreach(var wall in roomDto.Walls)
									{
										if(wall.StateChanged != null)
										{
											if(wall.StateChanged.Equals("modified"))
											{
												var modifiedWall = dbContext.Walls.Include(w => w.Floor).Include(w => w.Rooms).FirstOrDefault(w => w.WallId == wall.Id);
												if(modifiedWall != null)
												{
													modifiedWall.StartVertexX = wall.StartVertex.X;
													modifiedWall.StartVertexY = wall.StartVertex.Y;

													modifiedWall.EndVertexX = wall.EndVertex.X;
													modifiedWall.EndVertexY = wall.EndVertex.Y;
												}
											}
										}
									}
									foreach (var seat in roomDto.Seats)
									{
										if (seat.StateChanged != null)
										{
											if (seat.StateChanged.Equals("deleted"))
											{
												var seatToDelete = dbContext.Seats.FirstOrDefault(s => s.SeatId == seat.Id);
												if (seatToDelete != null)
												{
													dbContext.Seats.Remove(seatToDelete);
												}
											} else if (seat.StateChanged.Equals("added"))
											{

												Vertex seatVertex = new Vertex(seat.Vertex.X, seat.Vertex.Y);

												Seat seato = new Seat(seatVertex);
												dbContext.Seats.Local.Add(seato);
												modifiedRoom.Seats.Add(seato);

											} else if (seat.StateChanged.Equals("modified"))
											{
												var modifiedSeat = dbContext.Seats.FirstOrDefault(s => s.SeatId == seat.Id);
												if(modifiedSeat != null)
												{
													modifiedSeat.X = seat.Vertex.X;
													modifiedSeat.Y = seat.Vertex.Y;
												}
											}

										}

									}
								}
								int c = 0;
								c++;
							}
						}
						/*
						int asda = roomDto.Id;
						var existingRoom = dbContext.Rooms.FirstOrDefault(S => S.RoomId == asda);

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
							int asda2 = roomDto.Seats.ElementAt(i).Id;
							var existingSeat = dbContext.Seats.FirstOrDefault(S => S.SeatId == asda);
							if (existingSeat == null)
							{
								Vertex seatVertex = new Vertex(roomDto.Seats.ElementAt(i).Vertex.X, roomDto.Seats.ElementAt(i).Vertex.Y);

								Seat seat = new Seat(seatVertex);
								//if (roomDto.Seats.ElementAt(i).Id != 0) seat.SeatId = roomDto.Seats.ElementAt(i).Id;
								seatList.Add(seat);
							} else
							{
								existingSeat.Room = dbContext.Rooms.Where(R => R.RoomId == roomDto.Id).FirstOrDefault();
								existingSeat.X = roomDto.Seats.ElementAt(i).Vertex.X;
								existingSeat.Y = roomDto.Seats.ElementAt(i).Vertex.Y;
								existingSeat.Vertex = new Vertex(roomDto.Seats.ElementAt(i).Vertex.X, roomDto.Seats.ElementAt(i).Vertex.Y);
								existingSeat.EmployeeId = roomDto.Seats.ElementAt(i).DeveloperId;
								seatList.Add(existingSeat);
							}
						}
						if (existingRoom == null)
						{
							var Room = new Room()
							{
								FloorId = floorDto.Id,
								Walls = roomWallsList,
								Seats = seatList
							};
							dbContext.Rooms.Local.Add(Room);
							roomsList.Add(Room);
						} else
						{
							existingRoom.Walls = roomWallsList;
							existingRoom.Seats = seatList;
							dbContext.Rooms.Local.Add(existingRoom);

						}*/
					}
					
					/*ICollection<Wall> wallsList = new List<Wall>();
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
					dbContext.Rooms.RemoveRange(existingFloor.Rooms);
					existingFloor.Rooms = roomsList;
					existingFloor.Walls = wallsList;*/
					int count = dbContext.SaveChanges();
					count++;
				}
			}
		}
	}
}