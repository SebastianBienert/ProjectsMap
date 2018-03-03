using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProjectsMap.WebApi.Models;
using ProjectsMap.WebApi.Repositories.Abstract;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using ProjectsMap.WebApi.DTOs;
using ProjectsMap.WebApi.Migrations;

namespace ProjectsMap.WebApi.Repositories.Concrete
{
    public class DeveloperRepository : IDeveloperRepository
    {
        public IEnumerable<Developer> Developers
        {
            get
            {
                using (var dbContext = new EfDbContext())
                {
                    return dbContext.Developers.Include(d => d.Technologies)
                        .Include(d => d.Seat.Select(s => s.Vertex)).ToList();
                }
            }
        }

        public Developer Get(int id)
        {
            using (var dbContext = new EfDbContext())
            {
                return dbContext.Developers.
                    Include(d => d.Technologies)
                    .Include(d => d.Seat.Select(s => s.Vertex))
                    .FirstOrDefault(x => x.DeveloperId == id);
            }
        }

        public int Add(DeveloperDto dto)
        {
            using (var dbContext = new EfDbContext())
            {
                var dev = new Developer(dto.FirstName, dto.Surname)
                {
                    Technologies = dbContext.Technologies.Where(x => dto.Technologies.Contains(x.Name)).ToList(),
                    Seat = dto.Seat == null ? null : new List<Seat>() { dbContext.Seats.FirstOrDefault(s => s.SeatId == dto.Seat.Id)}
                };
                if (dto.Id == 0)
                {
                    var user = new User
                    {
                        Created = DateTime.Now,
                        Developer = dev
                    };
                    dev.User = user;
                 }
                else
                {
                    var user = dev.User = dbContext.Users.FirstOrDefault(u => u.UserId == dto.Id);
                }
                dbContext.Developers.Add(dev);
                dbContext.SaveChanges();

                return dev.DeveloperId;
            }
        }

        public int Add(Developer developer)
        {
            using (var dbContext = new EfDbContext())
            {
                dbContext.Developers.Add(developer);
                dbContext.SaveChanges();

                return developer.DeveloperId;
            }
        }

        public void Delete(Developer developer)
        {
            using (var dbContext = new EfDbContext())
            {
                dbContext.Developers.Remove(developer);
                dbContext.SaveChanges();
            }
        }

        public void Update(Developer developer)
        {
            using (var dbContext = new EfDbContext())
            {
                var dev = dbContext.Developers.FirstOrDefault(x => x.DeveloperId == developer.DeveloperId);
                dev = developer;
                dbContext.SaveChanges();
            }
        }
    }
}