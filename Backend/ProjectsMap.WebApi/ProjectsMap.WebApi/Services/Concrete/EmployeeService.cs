using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Web;
using System.Web.Hosting;
using ProjectsMap.WebApi.DTOs;
using ProjectsMap.WebApi.Infrastructure;
using ProjectsMap.WebApi.Mappers;
using ProjectsMap.WebApi.Models;
using ProjectsMap.WebApi.Repositories.Abstract;
using ProjectsMap.WebApi.Services.Abstract;

namespace ProjectsMap.WebApi.Services
{
    public class EmployeeService : IEmployeeService
    {
        private IEmployeeRepository _repository;
        private IFloorRepository _floorRepository;

        public EmployeeService(IEmployeeRepository repository, IFloorRepository floorRepository)
        {
            _repository = repository;
            _floorRepository = floorRepository;
        }

        public IEnumerable<EmployeeDto> GetAllEmployees()
        {
            var list = new List<EmployeeDto>();
            foreach (var dev in _repository.Employees)
            {
                list.Add(DTOMapper.GetEmployeeDto(dev));
            }

            return list;
        }

        public EmployeeDto GetEmployee(int id)
        {
            var developer = _repository.Get(id);
            if (developer == null)
                return null;

            return DTOMapper.GetEmployeeDto(developer);
        }

        public Employee GetEmployeeEntity(int id)
        {
            return _repository.Get(id);
        }

        public IEnumerable<EmployeeDto> GetDevelopersByTechnology(string technology)
        {
            technology = technology.ToLower();
            var list = _repository.Employees.Where(x => x.Technologies.Select(t => t.Name.ToLower()).ToList().Any(s => s.Contains(technology))).ToList();

            if (list.Count() > 0)
            {
                var dtoS = list.Select(dev => DTOMapper.GetEmployeeDto(dev)).ToList();
                return dtoS;
            }
            else
            {
                return null;
            }
        }

        public FloorDto GetEmployeeFloor(int id)
        {
            var employee = _repository.Get(id);
            var floors = _floorRepository.Floors.Select(x => DTOMapper.GetFloorDto(x));
            if (employee == null || floors == null)
                return null;
            var employeeRoomId = employee.Seat.RoomId;
            var employeeFloor = floors.Where(f => f.Rooms.Where(r => r.Id == employeeRoomId).First().Id == employeeRoomId).First();
            if (employeeFloor == null)
                return null;
            return employeeFloor;
        }

        public IEnumerable<EmployeeDto> GetEmployeesByName(string name)
        {
            List<Employee> list;
            name = name.ToLower().TrimStart();
            String[] fullName = null;
            fullName = name.Split(' ');
            if (fullName.Length >= 2)
            {
                list = _repository.Employees
                .Where(x => (x.Surname.ToLower().Contains(fullName[0]) && x.FirstName.ToLower().Contains(fullName[1]) || x.Surname.ToLower().Contains(fullName[1]) && x.FirstName.ToLower().Contains(fullName[0])))
                .ToList();
            }else
            {
                list = _repository.Employees
                .Where(x => (x.Surname.ToLower().Contains(fullName[0]) || x.FirstName.ToLower().Contains(fullName[0])))
                .ToList();
            }
            //var list = _repository.Emloyees.Where(x => (x.Surname.ToLower().Contains(name) || x.FirstName.ToLower().Contains(name)).ToList();

            if (list.Count() > 0)
            {
                var dtoS = list.Select(dev => DTOMapper.GetEmployeeDto(dev)).ToList();
                return dtoS;
            }
            else
            {
                return null;
            }
        }



        public bool AddPhotoToEmployee(int id, string path)
        {
           var employee = _repository.Employees.FirstOrDefault(e => e.EmployeeId == id);
            if (employee != null)
            {
                employee.Photo = path;
                _repository.Update(employee);
                return true;
            }

            return false;
        }

        public string GetPhotoPath(int id)
        {
            var path =  _repository.Employees.FirstOrDefault(e => e.EmployeeId == id)?.Photo;
     
            return path == null ? null : HostingEnvironment.MapPath(path);
        }

        public bool DeletePhoto(int id)
        {
            var employee = _repository.Employees.FirstOrDefault(e => e.EmployeeId == id);

            if (employee != null && employee.Photo != null)
            {
                var filePath = HostingEnvironment.MapPath(employee.Photo);
                File.Delete(filePath);
                employee.Photo = null;
                _repository.Update(employee);
                return true;
            }
                return false;
        }

        public int Post(EmployeeDto employee, ApplicationUser user)
        {
           return  _repository.Add(employee, user);
        }

        public void Delete(Employee employee)
        {
            _repository.Delete(employee);
        }

        public void Update(int employeeId, EmployeeDto employee)
        {
            _repository.Update(employeeId, employee);
        }

        public IEnumerable<EmployeeDto> GetEmployeesByQuery(string query)
        {
            //Query by id
            List<Employee> result;
            int n;
            if (int.TryParse(query, out n))
            {
                result = _repository.Employees.Where(e => e.EmployeeId.ToString().StartsWith(query)).ToList();
            }
            else
            {
                result = _repository.Employees.Where(e => e.FirstName.StartsWith(query) || e.Surname.StartsWith(query))
                    .ToList();
            }

            var dtos = new List<EmployeeDto>();
            foreach (var entity in result)
            {
                dtos.Add(DTOMapper.GetEmployeeDto(entity));
            }

            return dtos;
        }

		public object GetEmployeeLocationInfo(int id)
		{
			var employee = _repository.Get(id);
			if (employee == null)
				return null;
				var employeeId = employee.EmployeeId;
			if (employee.Seat != null)
			{
				var seatId = employee.Seat.SeatId;
				var roomId = employee.Seat.RoomId;
				var floorId = employee.Seat.Room.FloorId;
				var employeeBuildingId = employee.Seat.Room.Floor.BuildingId;
			return new { RoomId = roomId, FloorId = floorId, SeatId = seatId, EmployeeId = employeeId, EmployeeBuildingId = employeeBuildingId };
			}
			return new { RoomId = 0, FloorId = 0, SeatId = 0, EmployeeId = employeeId, EmployeeBuildingId = 0 };

		}
	}
}