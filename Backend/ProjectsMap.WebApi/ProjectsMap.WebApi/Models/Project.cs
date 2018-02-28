using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjectsMap.WebApi.Models
{
    public class Project
    {
        [Key]
        public int ProjectId { get; set; }

        public string Description { get; set; }

        public string RepositoryLink { get; set; }

        public string DocumentationLink { get; set; }

        public virtual Developer ProductOwner { get; set; }

        public virtual Company Company { get; set; }

        public int? CompanyId { get; set; }

        //Many to many relation (Technology - Project)
        public virtual ICollection<Technology> Technologies { get; set; }

        //Many to many relation (Project - Developer)
        public virtual ICollection<Developer> Developers { get; set; }

        //Many to many relation (Room - Project)
        public virtual ICollection<Room> Rooms { get; set; }
    }
}