using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using ProjectsMap.WebApi.Models;

namespace ProjectsMap.WebApi.Repositories.EntityFramework
{
    public class ProjectsMapDbInitializer : DropCreateDatabaseAlways<EfDbContext>
    {
        protected override void Seed(EfDbContext context)
        {
           // base.Seed(context);
            IList<Vertex> vertices = new List<Vertex>()
            {
                new Vertex(0,0),
                new Vertex(70,0),//seat free
                new Vertex(70,70),
                new Vertex(0,70),
                new Vertex(20,20),//seat
                new Vertex(40,20),//seat
                new Vertex(140,70),
                new Vertex(140,0), //7
                new Vertex(210,70),
                new Vertex(210,0), //9
                new Vertex(280,70),
                new Vertex(280,0), //11
                new Vertex(350,70),
                new Vertex(350,0), //13
                new Vertex(420,70),
                new Vertex(420,0), //15
                new Vertex(160,20),//seat free
                new Vertex(180,20),//seat free
                new Vertex(490,70),
                new Vertex(230,20), //19 seat free
                new Vertex(560,70),
                new Vertex(560,0), //21
                new Vertex(0,210),
                new Vertex(70,210), //23
                new Vertex(490,190),
                new Vertex(560,190), //25
                new Vertex(70,350),
                new Vertex(0,350), //27
                new Vertex(490,230),
                new Vertex(560,230), //29
                new Vertex(560,350),
                new Vertex(490,350), //31
                new Vertex(0,510),
                new Vertex(140,510), //33
                new Vertex(140,440),
                new Vertex(70,440), //35
                new Vertex(210,440),
                new Vertex(210,510), //37
                new Vertex(280,440),
                new Vertex(280,510), //39
                new Vertex(350,440),
                new Vertex(350,510), //41
                new Vertex(420,440),
                new Vertex(420,510), //43
                new Vertex(560,510),
                new Vertex(560,351), //45                                           //NOT USED
                new Vertex(490,351),                                                //NOT USED
                new Vertex(490,440), //47
                new Vertex(100,100),
                new Vertex(280,100), //49
                new Vertex(280,210),
                new Vertex(100,210), //51
                new Vertex(450,100),
                new Vertex(450,210), //53
                new Vertex(210,240),
                new Vertex(210,410), //55
                new Vertex(100,410),
                new Vertex(340,240), //57
                new Vertex(340,410),
                new Vertex(450,410), //59
                new Vertex(450,240),
                new Vertex(100,240), // 61
            };

            IList<Room> rooms = new List<Room>()
            {
                new Room()
                {
                    RoomId = 1,
                    Projects = new List<Project>(),
                    Walls = new List<Wall>
                    {
                        new Wall()
                        {
                            StartVertex = vertices[3],
                            EndVertex = vertices[6]
                        },
                        new Wall()
                        {
                            StartVertex = vertices[0],
                            EndVertex = vertices[3]
                        },

                        new Wall()
                        {
                            StartVertex = vertices[6],
                            EndVertex = vertices[7]
                        },
                        new Wall()
                        {
                            StartVertex = vertices[7],
                            EndVertex = vertices[0]
                        },
                    }
                    //new List<Vertex>() {vertices[0],vertices[3],vertices[6],vertices[7]}
                },
                new Room()
                {
                    RoomId = 2,
                    Projects = new List<Project>(),
                    Walls = new List<Wall>
                    {
                        new Wall()
                        {
                            StartVertex = vertices[48],
                            EndVertex = vertices[49]
                        },
                        new Wall()
                        {
                            StartVertex = vertices[49],
                            EndVertex = vertices[50]
                        },
                        new Wall()
                        {
                            StartVertex = vertices[50],
                            EndVertex = vertices[51]
                        },
                        new Wall()
                        {
                            StartVertex = vertices[51],
                            EndVertex = vertices[48]
                        },
                    }
                    //Vertexes = new List<Vertex>() {vertices[48],vertices[49],vertices[50],vertices[51]}
                },
                new Room()
                {
                    RoomId = 3,
                    Projects = new List<Project>(),
                    Walls = new List<Wall>
                    {
                        new Wall()
                        {
                            StartVertex = vertices[6],
                            EndVertex = vertices[7]
                        },
                        new Wall()
                        {
                            StartVertex = vertices[7],
                            EndVertex = vertices[9]
                        },
                        new Wall()
                        {
                            StartVertex = vertices[9],
                            EndVertex = vertices[8]
                        },
                        new Wall()
                        {
                            StartVertex = vertices[8],
                            EndVertex = vertices[6]
                        },
                    }
                   // Vertexes = new List<Vertex>() {vertices[6],vertices[7],vertices[9],vertices[8]}
                }
                /*
                new Room()
                {
                    RoomId = 4,
                    Projects = new List<Project>(),
                    Vertexes = new List<Vertex>() {vertices[9],vertices[8],vertices[10],vertices[11]}
                },
                new Room()
                {
                    RoomId = 5,
                    Projects = new List<Project>(),
                    Vertexes = new List<Vertex>() {vertices[11],vertices[10],vertices[13],vertices[12]}
                },
                new Room()
                {
                    RoomId = 6,
                    Projects = new List<Project>(),
                    Vertexes = new List<Vertex>() {vertices[13],vertices[12],vertices[14],vertices[15]}
                },
                new Room()
                {
                    RoomId = 7,
                    Projects = new List<Project>(),
                    Vertexes = new List<Vertex>() {vertices[15],vertices[14], vertices[21], vertices[20] }
                },
                new Room()
                {
                    RoomId = 8,
                    Projects = new List<Project>(),
                    Vertexes = new List<Vertex>() {vertices[3],vertices[2],vertices[23],vertices[22]}
                },
                new Room()
                {
                    RoomId = 9,
                    Projects = new List<Project>(),
                    Vertexes = new List<Vertex>() {vertices[20],vertices[18],vertices[24],vertices[25]}
                },
                new Room()
                {
                    RoomId = 10,
                    Projects = new List<Project>(),
                    Vertexes = new List<Vertex>() {vertices[27],vertices[26],vertices[23],vertices[22]}
                },
                new Room()
                {
                    RoomId = 11,
                    Projects = new List<Project>(),
                    Vertexes = new List<Vertex>() {vertices[28],vertices[29],vertices[30],vertices[31]}
                },
                new Room()
                {
                    RoomId = 12,
                    Projects = new List<Project>(),
                    Vertexes = new List<Vertex>() {vertices[27],vertices[26],vertices[35],vertices[34], vertices[33], vertices[32] }
                },
                new Room()
                {
                    RoomId = 13,
                    Projects = new List<Project>(),
                    Vertexes = new List<Vertex>() {vertices[33],vertices[34],vertices[37],vertices[36]}
                },
                new Room()
                {
                    RoomId = 14,
                    Projects = new List<Project>(),
                    Vertexes = new List<Vertex>() {vertices[37],vertices[36],vertices[38],vertices[39]}
                },
                new Room()
                {
                    RoomId = 15,
                    Projects = new List<Project>(),
                    Vertexes = new List<Vertex>() {vertices[39],vertices[38],vertices[41],vertices[40]}
                },
                new Room()
                {
                    RoomId = 16,
                    Projects = new List<Project>(),
                    Vertexes = new List<Vertex>() {vertices[41],vertices[40],vertices[42],vertices[43]}
                },
                new Room()
                {
                    RoomId = 17,
                    Projects = new List<Project>(),
                    Vertexes = new List<Vertex>() {vertices[43],vertices[42],vertices[44],vertices[30], vertices[31], vertices[47] }
                },
                new Room()
                {
                    RoomId = 18,
                    Projects = new List<Project>(),
                    Vertexes = new List<Vertex>() {vertices[50],vertices[49],vertices[53],vertices[52]}
                },
                new Room()
                {
                    RoomId = 19,
                    Projects = new List<Project>(),
                    Vertexes = new List<Vertex>() {vertices[61],vertices[54],vertices[55],vertices[56]}
                },
                new Room()
                {
                    RoomId = 20,
                    Projects = new List<Project>(),
                    Vertexes = new List<Vertex>() {vertices[54],vertices[55],vertices[58],vertices[57]}
                },
                new Room()
                {
                    RoomId = 21,
                    Projects = new List<Project>(),
                    Vertexes = new List<Vertex>() {vertices[57],vertices[58],vertices[60],vertices[59]}
                },*/
            };

            IList<Project> projects  = new List<Project>()
            {
                new Project()
                {
                    Description = "ProjectsMap - projekt zespolowy",
                    DocumentationLink = "documentationlink",
                    RepositoryLink = "repositoryLink",
                    Rooms = rooms,
                }
            };

            IList<Developer> developers = new List<Developer>()
            {
                new Developer()
                {
                    FirstName = "Witkor",
                    Surname = "Bukowski",
               //     DeveloperId = 1,
                    Technologies = new List<Technology>()
                    {
                        new Technology()
                        {
                            TechnologyId = 1,
                            Name = "Angular"
                        },
                        new Technology()
                        {
                            TechnologyId = 2,
                            Name = "Java"
                        }
                    },
                    Projects = new List<Project>()
                },
                new Developer()
                {
                    FirstName = "Michal",
                    Surname = "Radziwilko",
            //        DeveloperId = 2,
                    Technologies = new List<Technology>()
                    {
                        new Technology()
                        {
                            TechnologyId = 1,
                            Name = "HTML"
                        },
                        new Technology()
                        {
                            TechnologyId = 2,
                            Name = "CSS"
                        }
                    },
                    Projects = new List<Project>()
                }
            };

            projects[0].Developers = developers;
            projects[0].Technologies = new List<Technology>()
            {
                developers.ToList()[0].Technologies.ToList()[0],
                developers.ToList()[0].Technologies.ToList()[1],
                developers.ToList()[1].Technologies.ToList()[0],
                developers.ToList()[1].Technologies.ToList()[1]
            };


            IList<User> users = new List<User>
            {
                new User()
                {     
                    Created = DateTime.Now,
                    Developer = developers[0],
                    Username = "Wiktor",
                    Password = "secret",
                    Email = "michal@gmail.com"
                },
                new User()
                {
                    Created = DateTime.Now,
                    Developer = developers[1],
                    Username = "Michal",
                    Password = "secret2",
                    Email = "wiktor@gmail.com"
                },
                new User()
                {
                    Created = DateTime.Now,
                    Developer = null,
                    Username = "Wiktor",
                    Password = "secret",
                    Email = "michal@gmail.com"
                },
            };

            IList<Seat> seatsFirstRoom = new List<Seat>()
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
            IList<Seat> seatsThirdRoom = new List<Seat>()
            {
                new Seat()
                {
                    SeatId = 3,
                    Developer = developers[1],
                    Vertex = vertices[16]
                },
                new Seat()
                {
                    SeatId = 4,
                    Developer = developers[1],
                    Vertex = vertices[17]
                },
            };
            //IList<Seat> seatsFourthRoom = new List<Seat>()
            //{
            //    new Seat()
            //    {
            //        SeatId = 5,
            //        Developer = developers[1],
            //        Vertex = vertices[19]
            //    },
            //};


            rooms[0].Seats = seatsFirstRoom;

            seatsFirstRoom[0].RoomId = 1;
            seatsFirstRoom[0].Room = rooms[0];

            seatsFirstRoom[1].RoomId = 1;
            seatsFirstRoom[1].Room = rooms[0];

            rooms[2].Seats = seatsThirdRoom;

            seatsThirdRoom[0].RoomId = 3;
            seatsThirdRoom[0].Room = rooms[2];

            seatsThirdRoom[1].RoomId = 3;
            seatsThirdRoom[1].Room = rooms[2];

            //seatsFourthRoom[0].RoomId = 4;
            //seatsFourthRoom[0].Room = rooms[3];

            developers[0].User = users[0];
            developers[0].Seat = new List<Seat>() { seatsFirstRoom[0]};

            developers[1].User = users[1];
            developers[1].Seat = new List<Seat>() { seatsFirstRoom[1]};


            var company = new Company()
            {
                CompanyId = 1,
                Buildings = new List<Building>()
                {
                    new Building()
                    {
                        BuildingId = 1,
                        CompanyId = 1,
                        Address = "Wroclaw, Poznanska 54A",
                        Floors = new List<Floor>()
                        {
                            new Floor()
                            {
                                BuildingId = 1,
                                Description = "Pietro 3",
                                FloorId = 1,
                                Rooms = rooms
                            }
                        }
                    }
                },
                Developers = developers,
                Projects = projects
                
            };
            projects.ToList()[0].Company = company;
            company.Buildings.ToList()[0].Floors.ToList()[0].Building = company.Buildings.ToList()[0];

            company.Buildings.ToList()[0].Company = company;

            context.Companies.Add(company);

     
           /* foreach (var ver in vertices)
                context.Vertexes.Add(ver);*/

          /*  foreach (var room in rooms)
                context.Rooms.Add(room);*/

          /*  foreach (var seat in seatsFirstRoom)
                context.Seats.Add(seat);

            foreach (var seat in seatsThirdRoom)
                context.Seats.Add(seat);*/

            //foreach (var seat in seatsFourthRoom)
            //    context.Seats.Add(seat);

            foreach (var user in users)
                context.Users.Add(user);

            /*foreach (var dev in developers)
                context.Developers.Add(dev);
            */

            context.SaveChanges();



        }
    }
}