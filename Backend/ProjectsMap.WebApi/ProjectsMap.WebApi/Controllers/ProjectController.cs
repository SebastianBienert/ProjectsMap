using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ProjectsMap.WebApi.Repositories.Abstract;

namespace ProjectsMap.WebApi.Controllers
{
    [RoutePrefix("api/project")]
    public class ProjectController : ApiController
    {
        private IProjectRepository _repository;

        public ProjectController(IProjectRepository repository)
        {
            _repository = repository;
        }

        [Route("")]
        public IHttpActionResult GetAll()
        {
            return Ok(_repository.Projects);
        }

        [Route("{id:int}")]
        public IHttpActionResult Get(int id)
        {
            var project = _repository.Get(id);

            if (project != null)
                return Ok(project);
            else
                return NotFound();
        }

    }
}
