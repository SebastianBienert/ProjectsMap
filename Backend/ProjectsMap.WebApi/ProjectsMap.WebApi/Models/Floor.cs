using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectsMap.WebApi.Models
{
    public class Floor
    {
        public int Id { get; set; }

        //Shape of the floor is described by the list of vertexes (x,y)
        public IEnumerable<Tuple<int,int>> Vertexes { get; set; }

        public IEnumerable<Seat> Seats { get; set; }

        public IEnumerable<Project> Projects { get; set; }

        public IEnumerable<Developer> Developers { get; set; }
        
    }
}