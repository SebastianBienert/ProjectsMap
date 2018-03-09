using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectsMap.WebApi.DTOs
{
    public class EmployeeDto
    {
        public int Id { get; set; }

        public int CompanyId { get; set; }

        public int? ManagerId { get; set; }
        public int? ManagerCompanyId { get; set; }

        public string FirstName { get; set; }

        public string Surname { get; set; }

        public string Email { get; set; }

        public string JobTitle { get; set; }

        public string CompanyName { get; set; }

        public IEnumerable<string> Technologies { get; set; }

        public SeatDto Seat { get; set; }
    }



}