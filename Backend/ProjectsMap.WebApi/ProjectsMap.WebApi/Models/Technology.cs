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

        public string Name { get; set; }

        //Many to many relation (Developer - Technology)
        public virtual ICollection<Developer> Developers{ get; set; }

        //Many to many relation (Project - Technology)
        public virtual ICollection<Project> Projects { get; set; }
    }
}