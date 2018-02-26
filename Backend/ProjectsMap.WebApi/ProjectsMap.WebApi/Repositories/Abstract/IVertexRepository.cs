using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectsMap.WebApi.Models;

namespace ProjectsMap.WebApi.Repositories.Abstract
{
    public interface IVertexRepository
    {
        IEnumerable<Vertex> Vertices { get; }

        Vertex Get(int id);

        void Add(Vertex vertex);

        void Delete(Vertex vertex);

        void Update(Vertex vertex);
    }
}
