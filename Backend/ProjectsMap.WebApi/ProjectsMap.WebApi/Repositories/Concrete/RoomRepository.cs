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
        public IEnumerable<Room> Rooms { get; }
        public Room Get(int id)
        {
            throw new NotImplementedException();
        }

        public void Add(Room room)
        {
            throw new NotImplementedException();
        }

        public void Delete(Room room)
        {
            throw new NotImplementedException();
        }

        public void Update(Room room)
        {
            throw new NotImplementedException();
        }
    }
}