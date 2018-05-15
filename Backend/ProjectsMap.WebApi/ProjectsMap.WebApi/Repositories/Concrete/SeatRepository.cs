using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProjectsMap.WebApi.Models;
using ProjectsMap.WebApi.Repositories.Abstract;
using System.Data.Entity;

namespace ProjectsMap.WebApi.Repositories.Concrete
{
    public class SeatRepository : ISeatRepository
    {
        public IEnumerable<Seat> Seats
        {
            get
            {
                var dbContext = new EfDbContext();
                return dbContext.Seats.ToList();
            }
        }

        public Seat Get(int id)
        {
            var dbContext = new EfDbContext();
            return dbContext.Seats.FirstOrDefault(s => s.SeatId == id);
        }

        public void Add(Seat seat)
        {
            using (var ctx = new EfDbContext())
            {
                ctx.Seats.Add(seat);
                ctx.SaveChanges();
            }
        }

        public void Delete(Seat seat)
        {
            using (var ctx = new EfDbContext())
            {
                ctx.Seats.Remove(seat);
                ctx.SaveChanges();
            }
        }

        public void Update(Seat seat)
        {
            throw new NotImplementedException();
        }

		public bool assignSeat(int seatId, string userId)
		{
			using (var ctx = new EfDbContext())
			{
				int employeeId = ctx.Users.Include(u => u.Employee).FirstOrDefault(u => u.Id == userId).Employee.EmployeeId;//TODO make it null-safe
				if (ctx.Seats.FirstOrDefault(s => s.SeatId == seatId).Employee != null)
					return false;
				else
				{
					//var Emp = ctx.Employees.Include(s => s.Seat).FirstOrDefault(s => s.EmployeeId == employeeId);
					var oldSeat = ctx.Employees.Include(s => s.Seat).FirstOrDefault(s => s.EmployeeId == employeeId).Seat;
					if(oldSeat != null)
					{
						ctx.Seats.Include(s => s.Employee).FirstOrDefault(s => s.SeatId == oldSeat.SeatId).Employee = null;
						ctx.Seats.Include(s => s.Employee).FirstOrDefault(s => s.SeatId == oldSeat.SeatId).EmployeeId = null;
					}
					ctx.Seats.Include(s => s.Employee).FirstOrDefault(s => s.SeatId == seatId).Employee = ctx.Employees.FirstOrDefault(s => s.EmployeeId == employeeId);
					ctx.Seats.Include(s => s.Employee).FirstOrDefault(s => s.SeatId == seatId).EmployeeId = ctx.Employees.FirstOrDefault(s => s.EmployeeId == employeeId).EmployeeId;
				}
				ctx.SaveChanges();
				return true;
			}
		}
	}
}