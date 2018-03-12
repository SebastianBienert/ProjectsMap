using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjectsMap.WebApi.Models
{
    public class Company
    {
        [Key]
        public int CompanyId { get; set; }

        public string Name { get; set; }

        public virtual ICollection<Building> Buildings { get; set; }

        public virtual ICollection<Employee> Developers { get; set; }

        public virtual ICollection<Project> Projects { get; set; }
    }
}