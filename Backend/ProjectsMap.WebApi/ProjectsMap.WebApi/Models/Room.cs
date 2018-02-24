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
        public virtual ICollection<Vertex> Vertexes { get; set; }

        //One to many relation (Room - Seats)
        public virtual ICollection<Seat> Seats { get; set; }

        //many to many relation (Room - Project)
        public virtual ICollection<Project> Projects { get; set; }   
    }
}