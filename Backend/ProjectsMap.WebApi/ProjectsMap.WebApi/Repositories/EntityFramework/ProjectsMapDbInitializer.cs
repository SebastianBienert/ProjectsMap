using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using ProjectsMap.WebApi.Models;

namespace ProjectsMap.WebApi.Repositories.EntityFramework
{
    public class ProjectsMapDbInitializer : DropCreateDatabaseAlways<EfDbContext>
    {
        protected override void Seed(EfDbContext context)
        {
            base.Seed(context);
            IList<Vertex> vertices = new List<Vertex>()
            {
                new Vertex(0,0),
                new Vertex(10,0),
                new Vertex(0,10),
                new Vertex(10,10),
                new Vertex(2,2),
                new Vertex(5,5)
            };

            IList<Room> rooms = new List<Room>()
            {
                new Room()
                {
                    RoomId = 1,
                    Projects = new List<Project>(),
                    Vertexes = new List<Vertex>(vertices.Take(4).ToList())
                }
            };

            IList<Developer> developers = new List<Developer>()
            {
                new Developer()
                {
                    FirstName = "Witkor",
                    Surname = "Bukowski",
                    DeveloperId = 1,
                    Technologies = new List<Technology>()
                    {
                        new Technology()
                        {
                            TechnologyId = 1,
                            Name = "#ObijanieSie"
                        },
                        new Technology()
                        {
                            TechnologyId = 2,
                            Name = "#NicNieRobienie"
                        }
                    },
                    Projects = new List<Project>()
                },
                new Developer()
                {
                    FirstName = "Michal",
                    Surname = "Radziwilko",
                    DeveloperId = 2,
                    Technologies = new List<Technology>()
                    {
                        new Technology()
                        {
                            TechnologyId = 1,
                            Name = "#ObijanieSie"
                        },
                        new Technology()
                        {
                            TechnologyId = 2,
                            Name = "#NicNieRobienie"
                        }
                    },
                    Projects = new List<Project>()
                }
            };

            IList<User> users = new List<User>
            {
                new User()
                {
                    UserId = 1,
                    Created = DateTime.Now,
                    Developer = developers[0],
                    Username = "Wiktor",
                    Password = "secret",
                    Email = "michal@gmail.com"
                },
                new User()
                {
                    UserId = 2,
                    Created = DateTime.Now,
                    Developer = developers[1],
                    Username = "Michal",
                    Password = "secret2",
                    Email = "wiktor@gmail.com"
                },
            };

            IList<Seat> seats = new List<Seat>()
            {
                new Seat()
                {
                    SeatId = 1,
                    Developer = developers[0],
                    Vertex = vertices[4]
                },
                new Seat()
                {
                    SeatId = 2,
                    Developer = developers[1],
                    Vertex = vertices[5]
                },
            };

            rooms[0].Seats = seats;

            seats[0].RoomId = 1;
            seats[0].Room = rooms[0];

            seats[1].RoomId = 1;
            seats[1].Room = rooms[0];

            developers[0].User = users[0];
            developers[0].Seat = new List<Seat>() {seats[0]};

            developers[1].User = users[1];
            developers[1].Seat = new List<Seat>() { seats[1]};


            foreach (var ver in vertices)
                context.Vertexes.Add(ver);

            foreach (var room in rooms)
                context.Rooms.Add(room);

            foreach (var seat in seats)
                context.Seats.Add(seat);

            foreach (var user in users)
                context.Users.Add(user);

            foreach (var dev in developers)
                context.Developers.Add(dev);

            context.SaveChanges();

        }
    }
}