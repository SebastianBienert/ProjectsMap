using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectsMap.WebApi.Models;

namespace ProjectsMap.WebApi.UnitTests
{
    public class TestsData
    {
        public static IEnumerable<Developer> DevList
        {
            get
            {
                return new List<Developer>()
                {
                    new Developer()
                    {
                        DeveloperId = 1,
                        FirstName = "Jan",
                        Surname = "Kowalski",
                        Technologies = new List<string>() {"AngularJS", "VueJS"}
                    },
                    new Developer()
                    {
                        DeveloperId = 2,
                        FirstName = "Karol",
                        Surname = "Nowak",
                        Technologies = new List<string>() {"AngularJS", ".NET Framework"}
                    },
                    new Developer()
                    {
                        DeveloperId = 3,
                        FirstName = "Piotr",
                        Surname = "Nowak",
                        Technologies = new List<string>() {"JavaScript", "PHP"}
                    }
                };
            }
        }

        public static IEnumerable<Project> ProjectsList
        {
            get
            {
                var devList = DevList.ToList();
                return new List<Project>()
                {
                    new Project()
                    {
                        ProjectId = 1,
                        Description = "Projects Map",
                        Developers = new List<Developer> {devList[0], devList[1]},
                        UsedTechnologies = new List<string> {"AngularJS"}
                    },
                    new Project()
                    {
                        ProjectId = 2,
                        Description = "Simple store",
                        Developers = new List<Developer> {devList[2], devList[1]},
                        UsedTechnologies = new List<string> {".NET Framework"}
                    },
                    new Project()
                    {
                        ProjectId = 3,
                        Description = "Web browser game",
                        Developers = new List<Developer> {devList[2], devList[1]},
                        UsedTechnologies = new List<string> {"Unity"}
                    }
                };
            }
        }

        public static IEnumerable<Room> RoomList
        {        
            get
            {
                var devList = DevList.ToList();
                var projectList = ProjectsList.ToList();
                return new List<Room>()
                {
                    new Room()
                    {
                        RoomId = 1,
                        Vertexes = new List<Tuple<int, int>>()
                        {
                            new Tuple<int, int>(10,10),
                            new Tuple<int, int>(0,0),
                            new Tuple<int, int>(0,10),
                            new Tuple<int, int>(10,0)
                        },
                        Seats = new List<Seat>()
                        {
                            new Seat(devList[0], 5,5),
                            new Seat(devList[1], 8,8)
                        },
                        Projects = new List<Project>(){projectList[0], projectList[1], projectList[2]},
                        Developers = new List<Developer>(){devList[0], devList[1]},
                    },
                    new Room()
                    {
                        RoomId = 2,
                        Vertexes = new List<Tuple<int, int>>()
                        {
                            new Tuple<int, int>(100,100),
                            new Tuple<int, int>(0,0),
                            new Tuple<int, int>(0,100),
                            new Tuple<int, int>(100,0)
                        },
                        Seats = new List<Seat>()
                        {
                            new Seat(devList[1], 25,25),
                            new Seat(devList[2], 84,84)
                        },
                        Projects = new List<Project>(){projectList[0], projectList[1], projectList[2]},
                        Developers = new List<Developer>(){devList[1], devList[2]},
                    }
                };
            }
        }
    }
}
