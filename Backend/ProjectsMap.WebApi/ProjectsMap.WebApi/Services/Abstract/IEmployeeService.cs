using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectsMap.WebApi.DTOs;
using ProjectsMap.WebApi.Models;

namespace ProjectsMap.WebApi.Services.Abstract
{
    public interface IEmployeeService
    {
        IEnumerable<EmployeeDto> GetAllEmployees();

        EmployeeDto GetEmployee(int id);

        IEnumerable<EmployeeDto> GetDevelopersByTechnology(string technology);

        IEnumerable<EmployeeDto> GetEmployeesByName(string name);

        int Post(EmployeeDto employee);

        void Delete(Employee employee);

        void Update(Employee employee);

    }
}
