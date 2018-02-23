using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectsMap.WebApi.Models
{
    public class Project
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public IEnumerable<string> UsedTechnologies { get; set; }

        public IEnumerable<Developer> Developers { get; set; }
    }
}