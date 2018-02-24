using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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


        [Key]
        public int DeveloperId { get; set; }

        public int UserId { get; set; }

        public virtual User User { get; set; }

        public string FirstName { get; set; }

        public string Surname { get; set; }

        public IEnumerable<string> Technologies { get; set; }

        
    }
}