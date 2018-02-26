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

        public Vertex Get(int id)
        {
            var ctx = new EfDbContext();
            return ctx.Vertexes.FirstOrDefault(v => v.VertexId == id);
        }

        public void Add(Vertex vertex)
        {
            using (var ctx = new EfDbContext())
            {
                ctx.Vertexes.Add(vertex);
                ctx.SaveChanges();
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