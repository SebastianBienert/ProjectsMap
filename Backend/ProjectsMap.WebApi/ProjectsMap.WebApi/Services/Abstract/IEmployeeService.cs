using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectsMap.WebApi.DTOs;
using ProjectsMap.WebApi.Infrastructure;
using ProjectsMap.WebApi.Models;

namespace ProjectsMap.WebApi.Services.Abstract
{
    public interface IEmployeeService
    {
        IEnumerable<EmployeeDto> GetAllEmployees();

        EmployeeDto GetEmployee(int id);

        Employee GetEmployeeEntity(int id);

        IEnumerable<EmployeeDto> GetDevelopersByTechnology(string technology);

        IEnumerable<EmployeeDto> GetEmployeesByName(string name);

        bool AddPhotoToEmployee(int id, string path);

        string GetPhotoPath(int id);

        bool DeletePhoto(int id);

        int Post(EmployeeDto employee, ApplicationUser appUser);

        void Delete(Employee employee);

        void Update(int employeeId, EmployeeDto employee);

        IEnumerable<EmployeeDto> GetEmployeesByQuery(string query);

        FloorDto GetEmployeeFloor(int id);
    }
}
