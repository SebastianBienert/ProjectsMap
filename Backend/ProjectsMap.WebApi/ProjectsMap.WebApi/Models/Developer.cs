using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ProjectsMap.WebApi.Models
{
    public class Developer
    {
        public Developer()
        {
        }

        public Developer(string firstName, string surname)
        {
            FirstName = firstName;
            Surname = surname;
        }

        [Key]
        public int DeveloperId { get; set; }

        public virtual User User { get; set; }

        public string FirstName { get; set; }

        public string Surname { get; set; }

        public string Email { get; set; }

        public bool WantToHelp { get; set; }

        public byte[] Photo { get; set; }

        public string JobTitle { get; set; }

        public virtual Company Company { get; set; }

        public int? CompanyId { get; set; }

        //Many to many relation (Technology - Developer)
        public virtual ICollection<Technology> Technologies { get; set; } 

        //Many to many relation (Project - Developer)
        public virtual ICollection<Project> Projects { get; set; }

        //One to one relation(Seat-Developer)
        public virtual ICollection<Seat> Seat { get; set; }
    }
}