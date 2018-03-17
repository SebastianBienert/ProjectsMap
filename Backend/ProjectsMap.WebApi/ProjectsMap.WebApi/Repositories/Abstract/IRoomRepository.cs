using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectsMap.WebApi.Models;

namespace ProjectsMap.WebApi.Repositories.Abstract
{
    public interface IRoomRepository
    {
        IEnumerable<Room> Rooms { get; }

        Room Get(int id);

       // int Add(Room room);

        void Delete(Room room);

        void Update(Room room);
    }
}
