using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ProjectsMap.WebApi.DTOs;
using ProjectsMap.WebApi.DTOs.POST;
using ProjectsMap.WebApi.Models;
using ProjectsMap.WebApi.Repositories.Abstract;
using ProjectsMap.WebApi.Services.Abstract;

namespace ProjectsMap.WebApi.Controllers
{
    [RoutePrefix("api/project")]
    public class ProjectController : ApiController
    {
        private IProjectService _service;

        public ProjectController(IProjectService service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("")]
        public IHttpActionResult GetAll()
        {
            return Ok(_service.GetAllProjects());
        }

        [Route("{id:int}", Name = "GetProjectById")]
        public IHttpActionResult Get(int id)
        {
            var project = _service.GetProject(id);

            if (project != null)
                return Ok(project);
            else
                return NotFound();
        }


        [HttpPost]
        [Route("")]
        public IHttpActionResult Post(CreateProject dtoProject)
        {
            int createdId = _service.Post(dtoProject);
            return CreatedAtRoute("GetProjectById", new { id = createdId }, dtoProject);
        }

    }
}
