using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjectsMap.WebApi.Models
{
    public class Technology
    {
        [Key]
        public int TechnologyId { get; set; }

        [MaxLength(50)]
        public string Name { get; set; }

        //Many to many relation (Employee - Technology)
        public virtual ICollection<Employee> Developers{ get; set; }

        //Many to many relation (Project - Technology)
        public virtual ICollection<Project> Projects { get; set; }
    }
}