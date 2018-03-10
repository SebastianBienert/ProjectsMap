using System.Collections.Generic;
using ProjectsMap.WebApi.DTOs;
using ProjectsMap.WebApi.Models;

namespace ProjectsMap.WebApi.Services.Concrete
{
    public interface IRoomService
    {
        void Delete(Room room);
        IEnumerable<RoomDto> GetAllRooms();
        RoomDto GetRoom(int id);
        int Post(Room room);
        void Update(Room room);
    }
}