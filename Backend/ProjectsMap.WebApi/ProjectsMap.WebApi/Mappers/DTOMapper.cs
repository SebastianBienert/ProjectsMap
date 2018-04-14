using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.ExceptionServices;
using System.Security.Policy;
using System.Web;
using ProjectsMap.WebApi.DTOs;
using ProjectsMap.WebApi.Models;
using System.Web.Http.Routing;
using ProjectsMap.WebApi.Repositories.EntityFramework;


namespace ProjectsMap.WebApi.Mappers
{
    public class DTOMapper
    {
        private static UrlHelper Url
        {
            get
            {
               var httpRequestMessage = HttpContext.Current.Items["MS_HttpRequestMessage"] as HttpRequestMessage;
               var _urlHelper = new UrlHelper(httpRequestMessage);
               return _urlHelper;
            }
        }

        public static ProjectDto GetProjectDto(Project project)
        {
            return new ProjectDto()
            {
                CompanyId = project.CompanyId,
                Description = project.Description,
                EmployeesNames = project.ProjectRoles.Select(x => x.Employee.Surname).ToList(),
                DocumentationLink = project.DocumentationLink,
                Id = project.ProjectId,
                RepositoryLink = project.RepositoryLink,
                Technologies = project.Technologies?.Select(x => GeTechnologyDto(x)).ToList()
            };
        }

        public static EmployeeDto GetEmployeeDto(Employee employee)
        {
            var dto = new EmployeeDto()
            {
                ManagerCompanyId = employee.ManagerCompanyId,
                Id = employee.EmployeeId,     
                Url = Url.Link("GetEmployeeById", new { id = employee.EmployeeId }),
                PhotoUrl = employee.Photo == null ? null : Url.Link("GetEmployeePhoto", new { id = employee.EmployeeId }),
                ManagerId = employee.ManagerId,
                CompanyId = employee.CompanyId,
                FirstName = employee.FirstName,
                Surname = employee.Surname,
                Email = employee.Email,
                CompanyName = employee.Company.Name,
                JobTitle = employee.JobTitle,
                Technologies = employee.Technologies.Select(x => x.Name).ToList(),
            };

            if (employee.Seat == null)
                dto.Seat = null;
            else
            {
                var seatDto = new SeatDto()
                {
                    Id = employee.Seat.SeatId,
                    DeveloperId = employee.EmployeeId,
                    Vertex = employee.Seat == null ? null : new Vertex(employee.Seat.X, employee.Seat.Y),

                    RoomId = employee.Seat.SeatId
                };
                dto.Seat = seatDto;
            }


            return dto;
        }

        public static TechnologyDto GeTechnologyDto(Technology technology)
        {
            var dto = new TechnologyDto()
            {
                Name = technology.Name,
                Id = technology.TechnologyId,
                EmployeesId = technology.Employees.Select(x => x.EmployeeId).ToList(),
                ProjectsId = technology.Projects.Select(p => p.ProjectId).ToList()
            };

            return dto;
        }

        public static WallDto GetWallDto(Wall wall)
        {
            var dto = new WallDto()
            {
                Id = wall.WallId,
                StartVertex = new Vertex(wall.StartVertexX, wall.StartVertexY),
                EndVertex = new Vertex(wall.EndVertexX, wall.EndVertexY)
			};
            return dto;
        }

        public static RoomDto GetRoomDto(Room room)
        {
            var dto = new RoomDto()
            {
                Id = room.RoomId,
                Walls = GetWallsDtoList(room.Walls.ToList()),
                Seats = room.Seats.Select(s => GetSeatDto(s)).ToList()
            };
            return dto;
        }

        public static List<WallDto> GetWallsDtoList(List<Wall> walls)
        {
            var list = new List<WallDto>();
            var current = walls[0];
            do
            {
                list.Add(GetWallDto(current));
                var endX = current.EndVertexX;
                var endY = current.EndVertexY;
                var next = walls.FirstOrDefault(w => w.StartVertexX == endX && w.StartVertexY == endY);
                current = next;
            } while (list.Count < walls.Count);

            return list;
        }

        public static SeatDto GetSeatDto(Seat seat)
        {
			var result = new SeatDto()
			{
				Id = seat.SeatId,
				DeveloperId = seat.EmployeeId,
				RoomId = seat.RoomId,
				Vertex = new Vertex(seat.X, seat.Y)
            };
            return result;
        }

        public static BuildingDto GetBuildingDto(Building building)
        {
            return new BuildingDto()
            {
                Id = building.BuildingId,
                Address = building.Address,
				FloorsIds = building.Floors.Select(f => f.FloorId).ToList(),
				CompanyId = building.CompanyId
            };
        }

        public static CompanyDto GetCompanyDto(Company company)
        {
            return new CompanyDto()
            {
                Id = company.CompanyId,
                Name = company.Name,
                Buildings = company.Buildings.Select(b => GetBuildingDto(b)).ToList(),
                Developers = company.Employees.Select(d => GetEmployeeDto(d)).ToList(),
                ProjectsId = company.Projects.Select(p =>
                {
                    return new ProjectDtoShort()
                    {
                        Id = p.ProjectId,
                        Name = p.Description
                    };
                }).ToList()  
            };

        }


		public static FloorDto GetFloorDto(Floor floor)
		{
			var result = new FloorDto()
			{
				Id = floor.FloorId,
				Description = floor.Description,
				BuildingId = floor.BuildingId,
				Walls = GetWallsDtoListNotSorted(floor.Walls.ToList()),
				Rooms = GetRoomsDtoList(floor.Rooms.ToList()),
				FloorNumber = floor.FloorNumber
			};
			return result;
		}
		public static FloorDto GetFloorDtoListElement(Floor floor)
		{
			var result = new FloorDto()
			{
				Id = floor.FloorId,
				Description = floor.Description,
				BuildingId = floor.BuildingId,
                Walls = GetWallsDtoListNotSorted(floor.Walls.ToList()),
                Rooms = GetRoomsDtoList(floor.Rooms.ToList()),
                FloorNumber = floor.FloorNumber
			};
			return result;
		}
		public static List<RoomDto> GetRoomsDtoList(List<Room> rooms)
		{
			var list = new List<RoomDto>();
			foreach(Room room in rooms)
			{
				list.Add(GetRoomDto(room));
			}
			return list;
		}

		private static IEnumerable<WallDto> GetWallsDtoListNotSorted(List<Wall> listWalls)
		{
			var listWallsDto = new List<WallDto>();
			foreach(Wall wall in listWalls)
			{
				listWallsDto.Add(GetWallDto(wall));
			}
			return listWallsDto;
		}

	}
}