using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Web;
using ProjectsMap.WebApi.DTOs;
using ProjectsMap.WebApi.Mappers;
using ProjectsMap.WebApi.Models;
using ProjectsMap.WebApi.Repositories.Abstract;
using ProjectsMap.WebApi.Services.Abstract;

namespace ProjectsMap.WebApi.Services
{
    public class EmployeeService : IEmployeeService
    {
        private IEmployeeRepository _repository;

        public EmployeeService(IEmployeeRepository repository)
        {
            _repository = repository;
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

        public int Post(EmployeeDto employee)
        {
           return  _repository.Add(employee);
        }

        public void Delete(Employee employee)
        {
            _repository.Delete(employee);
        }

        public void Update(Employee employee)
        {
            _repository.Update(employee);
        }

        public IEnumerable<EmployeeDto> GetEmployeesByQuery(string query)
        {
            //Query by id
            List<Employee> result;
            if (int.TryParse(query, out int n))
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
    }
}