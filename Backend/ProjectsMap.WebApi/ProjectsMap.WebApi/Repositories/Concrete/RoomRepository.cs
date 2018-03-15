using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProjectsMap.WebApi.Models;
using ProjectsMap.WebApi.Repositories.Abstract;
using System.Data.Entity;

namespace ProjectsMap.WebApi.Repositories.Concrete
{
    public class RoomRepository : IRoomRepository
    {
        public IEnumerable<Room> Rooms
        {
            get
            {
                using (var dbContext = new EfDbContext())
                {
                    return dbContext.Rooms
                        .Include(r => r.Walls)
                        .Include(r => r.Seats).ToList();
                }
            }
        }
        public Room Get(int id)
        {
            using (var dbContext = new EfDbContext())
            {
                return dbContext.Rooms
                    .Include(r => r.Walls)
                    .Include(r => r.Seats)
                    .FirstOrDefault(x => x.RoomId == id);
            }
        }

        /*public void Add(Room room)
        {
            using (var dbContext = new EfDbContext())
            {
                dbContext.Rooms.Add(room);
                dbContext.SaveChanges();
            }
        }*/
		/*public int Add(Room room)
		{
			using (var dbContext = new EfDbContext())
			{
				dbContext.Vertexes.Load();

				for (int i = 0; i < room.Walls.Count; i++)
				{
					var x = room.Walls.ElementAt(i).StartVertex.X;
					var y = room.Walls.ElementAt(i).StartVertex.Y;
					Vertex start = dbContext.Vertexes.Local.Where(v => v.X == x && v.Y == y).FirstOrDefault();
					if (start != null)
					{
						room.Walls.ElementAt(i).StartVertex = start;
					}
					else
					{
						dbContext.Vertexes.Local.Add(room.Walls.ElementAt(i).StartVertex);
					}

					x = room.Walls.ElementAt(i).EndVertex.X;
					y = room.Walls.ElementAt(i).EndVertex.Y;
					Vertex end = dbContext.Vertexes.Local.Where(v => v.X == x && v.Y == y).FirstOrDefault();
					if (end != null)
					{
						room.Walls.ElementAt(i).EndVertex = end;
					}
					else
					{
						dbContext.Vertexes.Local.Add(room.Walls.ElementAt(i).EndVertex);
					}
			}
				dbContext.Rooms.Add(room);
				dbContext.SaveChanges();

				return room.RoomId;
			}
		}*/


		public void Delete(Room room)
        {
            using (var dbContext = new EfDbContext())
            {
                dbContext.Rooms.Remove(room);
                dbContext.SaveChanges();
            }
        }

        public void Update(Room room)
        {
            using (var dbContext = new EfDbContext())
            {
                var dev = dbContext.Rooms.FirstOrDefault(x => x.RoomId == room.RoomId);
                dev = room;
                dbContext.SaveChanges();
            }
        }

	}
}