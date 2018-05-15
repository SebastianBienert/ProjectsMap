using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectsMap.WebApi.DTOs;
using ProjectsMap.WebApi.Models;

namespace ProjectsMap.WebApi.Services.Abstract
{
    public interface ISeatService
    {
        void Delete(SeatDto seatDto);
        IEnumerable<SeatDto> GetAllSeats();
        SeatDto GetSeat(int id);
        void Post(SeatDto seatDto);
        void Update(SeatDto seatDto);
		bool assignSeat(int seatId, string userId);
	}
}
