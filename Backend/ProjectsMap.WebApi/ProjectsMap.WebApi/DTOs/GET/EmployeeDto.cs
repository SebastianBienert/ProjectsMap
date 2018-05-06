using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectsMap.WebApi.DTOs
{
    public class EmployeeDto
    {
        public int Id { get; set; }

        public string PhotoUrl { get; set; }

        public string Url { get; set; }

        public int? ManagerId { get; set; }

        public string FirstName { get; set; }

        public string Surname { get; set; }

        public string Email { get; set; }

        public string JobTitle { get; set; }
  
        public IEnumerable<string> Technologies { get; set; }

        public SeatDto Seat { get; set; }

        public IEnumerable<ProjectShortDto> Projects { get; set; }
    }


    public class ProjectShortDto
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }



}