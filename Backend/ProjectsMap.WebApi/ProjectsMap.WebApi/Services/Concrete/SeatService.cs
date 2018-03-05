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
            return _seatRepository.Seats.Select(s => DTOMapper.GetSeatDto(s)).ToList();
        }

        public SeatDto GetSeat(int id)
        {
            return DTOMapper.GetSeatDto(_seatRepository.Seats.FirstOrDefault(s => s.SeatId == id));
        }

        public void Post(SeatDto seatDto)
        {

            throw new NotImplementedException();
        }

        public void Update(SeatDto seatDto)
        {
            throw new NotImplementedException();
        }
    }
}