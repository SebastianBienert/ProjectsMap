using ProjectsMap.WebApi.DTOs;
using ProjectsMap.WebApi.Models;
using ProjectsMap.WebApi.Repositories.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProjectsMap.WebApi.Mappers;

namespace ProjectsMap.WebApi.Services.Concrete
{
    public class RoomService : IRoomService
    {
        private readonly IRoomRepository _roomRepository;
        private readonly IVertexRepository _vertexRepository;
        private readonly ISeatRepository _seatRepository;

        public RoomService(IRoomRepository roomRepository, IVertexRepository vertexRepository,
            ISeatRepository seatRepository)
        {
            _roomRepository = roomRepository;
            _vertexRepository = vertexRepository;
            _seatRepository = seatRepository;
        }

        public IEnumerable<RoomDto> GetAllRooms()
        {
            return _roomRepository.Rooms.Select(x => DTOMapper.GetRoomDto(x));
        }

        public RoomDto GetRoom(int id)
        {
            var room = _roomRepository.Get(id);
            if (room == null)
                return null;

            return DTOMapper.GetRoomDto(room);
        }

        public void Post(Room room)
        {
            //var result = _roomRepository.Rooms.FirstOrDefault(r => r.RoomId == room.Id);
           /* if (result != null)
                return;
            else
            {
                var vertexes = new List<Vertex>();
                foreach (var verDto in room.Vertexes)
                {
                    var ver = _vertexRepository.Vertices.FirstOrDefault(v => v.VertexId == verDto.Id);
                    if (ver != null)
                        vertexes.Add(ver);
                    else
                    {
                        ver = new Vertex()
                        {
                            X = verDto.X,
                            Y = verDto.Y,
                        };
                        _vertexRepository.Add(ver);
                        vertexes.Add(ver);
                    }
                }

                var seats = new List<Seat>();
                foreach (var seatDto in room.Seats)
                {
                    var seat = _seatRepository.Get(seatDto.Id);
                    if (seat != null)
                        seats.Add(seat);
                    else
                    {
                        seat = new Seat()
                        {

                        }
                    }
                }
                


               result = new Room()
               {
                   Vertexes = vertexes,

               }
            }*/




        }

        public void Delete(Room room)
        {
            _roomRepository.Delete(room);
        }

        public void Update(Room room)
        {
            _roomRepository.Update(room);
        }
    }
}