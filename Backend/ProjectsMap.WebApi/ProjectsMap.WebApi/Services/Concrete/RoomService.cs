using ProjectsMap.WebApi.DTOs;
using ProjectsMap.WebApi.Mapper;
using ProjectsMap.WebApi.Models;
using ProjectsMap.WebApi.Repositories.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectsMap.WebApi.Services.Concrete
{
    public class RoomService : IRoomService
    {
        private readonly IRoomRepository _repository;

        public RoomService(IRoomRepository roomRepository)
        {
            _repository = roomRepository;
        }

        public IEnumerable<RoomDto> GetAllRooms()
        {
            return _repository.Rooms.Select(x => RoomMapper.GetRoomDto(x));
        }

        public RoomDto GetRoom(int id)
        {
            var room = _repository.Get(id);
            if (room == null)
                return null;

            return RoomMapper.GetRoomDto(room);
        }

        public void Post(Room room)   
        {
            _repository.Add(room);
        }

        public void Delete(Room room)
        {
            _repository.Delete(room);
        }

        public void Update(Room room)
        {
            _repository.Update(room);
        }
    }
}