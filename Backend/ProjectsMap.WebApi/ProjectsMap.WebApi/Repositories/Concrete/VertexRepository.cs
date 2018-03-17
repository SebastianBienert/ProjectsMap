using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProjectsMap.WebApi.Models;
using ProjectsMap.WebApi.Repositories.Abstract;

namespace ProjectsMap.WebApi.Repositories.Concrete
{
    public class VertexRepository : IVertexRepository
    {
        public IEnumerable<Vertex> Vertices
        {
            get
            {
                var ctx = new EfDbContext();
                return ctx.Vertexes.ToList();
            }
        }

        public Vertex Get(int x, int y)
        {
            var ctx = new EfDbContext();
            return ctx.Vertexes.FirstOrDefault(v => v.X == x && v.Y == y);
        }

        public void Add(Vertex vertex)
        {
            using (var ctx = new EfDbContext())
            {
				if (Get(vertex.X, vertex.Y) == null)
				{
					ctx.Vertexes.Add(vertex);
					ctx.SaveChanges();
				}
            }
        }

        public void Delete(Vertex vertex)
        {
            throw new NotImplementedException();
        }

        public void Update(Vertex vertex)
        {
            throw new NotImplementedException();
        }
    }
}