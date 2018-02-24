using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjectsMap.WebApi.Models
{
    public class Room
    {
        [Key]
        public int RoomId { get; set; }

        //Shape of the room is described by the list of vertexes (x,y)
        public IEnumerable<Tuple<int,int>> Vertexes { get; set; }

        public IEnumerable<Seat> Seats { get; set; }

        public IEnumerable<Project> Projects { get; set; }

        public IEnumerable<Developer> Developers { get; set; }
        
    }
}