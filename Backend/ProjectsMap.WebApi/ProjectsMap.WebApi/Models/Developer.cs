using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectsMap.WebApi.Models
{
    public class Developer
    {
        public Developer() { }

        public Developer(string firstName, string surname)
        {
            FirstName = firstName;
            Surname = surname;
        }

        public int Id { get; set; }

        public string FirstName { get; set; }

        public string Surname { get; set; }

        public IEnumerable<string> Technologies { get; set; }

        
    }
}