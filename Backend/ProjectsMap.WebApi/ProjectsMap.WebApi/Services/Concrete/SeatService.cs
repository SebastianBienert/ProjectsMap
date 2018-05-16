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
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IRoomRepository _roomRepository;

        public SeatService(ISeatRepository seatRepository, IEmployeeRepository employeeRepository,
                IRoomRepository roomRepository)
        {
            _seatRepository = seatRepository;
            _employeeRepository = employeeRepository;
            _roomRepository = roomRepository;
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

		public bool assignSeat(int seatId, string userId)
		{
			return _seatRepository.assignSeat(seatId, userId);
		}
	}
}