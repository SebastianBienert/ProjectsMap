using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ProjectsMap.WebApi.Models
{
    public class Employee
    {
        public Employee()
        {
        }

        public Employee(string firstName, string surname)
        {
            FirstName = firstName;
            Surname = surname;
        }

        [Key]
        public int EmployeeId { get; set; }

        public virtual User User { get; set; }

        public string FirstName { get; set; }

        public string Surname { get; set; }

        public string Email { get; set; }

        public bool WantToHelp { get; set; }

        public int? ManagerId { get; set; }
        public int? ManagerCompanyId { get; set; }
        public virtual Employee Manager { get; set; }

        public byte[] Photo { get; set; }

        public string JobTitle { get; set; }

        public virtual Company Company { get; set; }

        public int CompanyId { get; set; }

        //Many to many relation (Technology - Employee)
        public virtual ICollection<Technology> Technologies { get; set; } 

        //Many to many relation (Project - Employee)
        public virtual ICollection<ProjectRole> ProjectRoles { get; set; }

        //One to many relation(Seat-Employee)
        public virtual ICollection<Seat> Seat { get; set; }
    }
}