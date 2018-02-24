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

        public IEnumerable<string> UsedTechnologies { get; set; }

        public IEnumerable<Developer> Developers { get; set; }
    }
}