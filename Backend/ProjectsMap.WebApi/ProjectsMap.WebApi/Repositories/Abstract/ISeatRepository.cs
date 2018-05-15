using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectsMap.WebApi.Models;

namespace ProjectsMap.WebApi.Repositories.Abstract
{
    public interface ISeatRepository
    {
        IEnumerable<Seat> Seats { get; }

        Seat Get(int id);

        void Add(Seat seats);

        void Delete(Seat seats);

        void Update(Seat seats);

		bool assignSeat(int seatId, string userId);
	}
}
