using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectsMap.WebApi.Models
{
    public class Seat
    {
        public int Id { get; set; }

        public Developer Owner { get; set; }

        //Coordinate of the Seat on the given floor is described by (x,y) - left top corner of a square
        public Tuple<int, int> Coordinates { get; set; }

    }
}