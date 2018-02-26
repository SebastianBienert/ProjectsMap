using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProjectsMap.WebApi.DTOs;
using ProjectsMap.WebApi.Mappers;
using ProjectsMap.WebApi.Models;
using ProjectsMap.WebApi.Repositories.Abstract;
using ProjectsMap.WebApi.Services.Abstract;

namespace ProjectsMap.WebApi.Services.Concrete
{
    public class SeatService : ISeatService
    {
        private readonly ISeatRepository _seatRepository;
        private readonly IDeveloperRepository _developerRepository;
        private readonly IRoomRepository _roomRepository;
        private readonly IVertexRepository _vertexRepository;

        public SeatService(ISeatRepository seatRepository, IDeveloperRepository developerRepository,
                IRoomRepository roomRepository, IVertexRepository vertexRepository)
        {
            _seatRepository = seatRepository;
            _developerRepository = developerRepository;
            _roomRepository = roomRepository;
            _vertexRepository = vertexRepository;
        }

        public void Delete(SeatDto seatDto)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<SeatDto> GetAllSeats()
        {
            return _seatRepository.Seats.Select(s => SeatMapper.GetSeatDto(s)).ToList();
        }

        public SeatDto GetSeat(int id)
        {
            return SeatMapper.GetSeatDto(_seatRepository.Seats.FirstOrDefault(s => s.SeatId == id));
        }

        public void Post(SeatDto seatDto)
        {
            var vertex = _vertexRepository.Vertices.FirstOrDefault(v => v.VertexId == seatDto.Vertex.Id);
            if (vertex == null)
            {
                vertex = new Vertex()
                {
                    VertexId = seatDto.Vertex.Id,
                    X = seatDto.Vertex.X,
                    Y = seatDto.Vertex.Y,
                    Rooms = new List<Room> {_roomRepository.Rooms.FirstOrDefault(r => r.RoomId == seatDto.RooomId)},
                };
                _vertexRepository.Add(vertex);
            }

            var seat = new Seat
            {
                SeatId = seatDto.Id,
                Developer = _developerRepository.Developers.FirstOrDefault(d => d.DeveloperId == seatDto.DeveloperId),
                DeveloperId = seatDto.DeveloperId,
                Room = _roomRepository.Rooms.FirstOrDefault(r => r.RoomId == seatDto.RooomId),
                RoomId = seatDto.RooomId,
                Vertex = vertex
            };
            vertex.Seat = seat;

            _seatRepository.Add(seat);

        }

        public void Update(SeatDto seatDto)
        {
            throw new NotImplementedException();
        }
    }
}