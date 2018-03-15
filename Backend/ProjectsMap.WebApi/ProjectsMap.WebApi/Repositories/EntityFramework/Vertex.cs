using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectsMap.WebApi.Repositories.EntityFramework
{
    public class Vertex
    {
        public Vertex()
        {
            
        }
        public Vertex(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int X { get; set; }

        public int Y { get; set; }

    }
}