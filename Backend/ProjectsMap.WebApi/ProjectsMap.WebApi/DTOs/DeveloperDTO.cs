using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectsMap.WebApi.DTOs
{
    public class DeveloperDto
    {
        public string FirstName { get; set; }

        public string Surname { get; set; }

        public IEnumerable<string> Technologies { get; set; }

        public SeatDto Seat { get; set; }
    }
}