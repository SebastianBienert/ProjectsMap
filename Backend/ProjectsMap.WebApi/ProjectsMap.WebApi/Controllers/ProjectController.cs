using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ProjectsMap.WebApi.DTOs;
using ProjectsMap.WebApi.Models;
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

        [HttpGet]
        [Route("")]
        public IHttpActionResult GetAll()
        {
            return Ok(_repository.Projects);
        }

        [Route("{id:int}", Name = "GetProjectById")]
        public IHttpActionResult Get(int id)
        {
            var project = _repository.Get(id);

            if (project != null)
                return Ok(project);
            else
                return NotFound();
        }


        [HttpPost]
        [Route("")]
        public IHttpActionResult Post(ProjectDto dtoProject)
        {
            int createdId = _repository.Add(dtoProject);
            return CreatedAtRoute("GetProjectById", new { id = createdId }, dtoProject);
        }

        [HttpDelete]
        [Route("")]
        public IHttpActionResult Delete(Project project)
        {
            _repository.Delete(project);
            return Ok();
        }

        [HttpPut]
        [Route("")]
        public IHttpActionResult Update(Project project)
        {
            _repository.Update(project);
            return Ok();
        }
    }
}
