using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjectsMap.WebApi.Models
{
    public class Vertex
    {
        public Vertex(int x, int y)
        {
            X = x;
            Y = y;
        }

        public Vertex() { }

        public int X { get; set; }

        public int Y { get; set; }

        //Many to many relation (Vertex - Room)
        public virtual ICollection<Wall> StartWalls { get; set; }

        //Many to many relation (Vertex - Room)
        public virtual ICollection<Wall> EndWalls { get; set; }

        //One to one or zero relation (Vertex - Seats)
        public virtual Seat Seat{ get; set; }
    }
}