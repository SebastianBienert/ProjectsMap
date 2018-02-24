using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjectsMap.WebApi.Models
{
    public class Vertex
    {
        public Vertex(double x, double y)
        {
            X = x;
            Y = y;
        }

        public Vertex() { }

        [Key]
        public int VertexId { get; set; }

        public double X { get; set; }

        public double Y { get; set; }

        //Many to many relation (Vertex - Room)
        public virtual ICollection<Room> Rooms { get; set; }

        //One to one or zero relation (Vertex - Seats)
        public virtual Seat Seat{ get; set; }

    }
}