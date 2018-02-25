using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProjectsMap.WebApi.Models;
using ProjectsMap.WebApi.Repositories.Abstract;

namespace ProjectsMap.WebApi.Repositories.Concrete
{
    public class RoomRepository : IRoomRepository
    {
        public IEnumerable<Room> Rooms
        {
            get
            {
                var dbContext = new EfDbContext();
                return dbContext.Rooms.ToList();
            }
        }
        public Room Get(int id)
        {
            var dbContext = new EfDbContext();
            return dbContext.Rooms.FirstOrDefault(x => x.RoomId == id);
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