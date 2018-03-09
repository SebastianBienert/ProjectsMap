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
            foreach (var dev in _repository.Developers)
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
            var list = _repository.Developers.Where(x => x.Technologies.Select(t => t.Name).ToList().Contains(technology)).ToList();

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
    }
}