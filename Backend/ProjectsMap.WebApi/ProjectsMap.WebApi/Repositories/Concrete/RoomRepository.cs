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
                        .Include(r => r.Projects)
                        .Include(r => r.Walls.Select(w => w.StartVertex))
                        .Include((r => r.Walls.Select(w => w.EndVertex)))
                        .Include(r => r.Seats.Select(s => s.Vertex)).ToList();
                }
            }
        }
        public Room Get(int id)
        {
            using (var dbContext = new EfDbContext())
            {
                return dbContext.Rooms
                    .Include(r => r.Projects)
                    .Include(r => r.Walls.Select(w => w.StartVertex))
                    .Include((r => r.Walls.Select(w => w.EndVertex)))
                    .Include(r => r.Seats.Select(s => s.Vertex))
                    .FirstOrDefault(x => x.RoomId == id);
            }
        }

        public void Add(Room room)
        {
            using (var dbContext = new EfDbContext())
            {
                dbContext.Rooms.Add(room);
                dbContext.SaveChanges();
            }
        }

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