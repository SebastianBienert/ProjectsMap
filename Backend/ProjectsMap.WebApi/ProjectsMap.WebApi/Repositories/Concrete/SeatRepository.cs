using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProjectsMap.WebApi.Models;
using ProjectsMap.WebApi.Repositories.Abstract;

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
    }
}