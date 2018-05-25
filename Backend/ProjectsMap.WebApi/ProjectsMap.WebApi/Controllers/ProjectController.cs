using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ProjectsMap.WebApi.DTOs;
using ProjectsMap.WebApi.DTOs.POST;
using ProjectsMap.WebApi.Infrastructure;
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

        [ClaimsAuthorization(ClaimType = "canReadProjects", ClaimValue = "true")]
        [HttpGet]
        [Route("")]
        public IHttpActionResult GetAll()
        {
            return Ok(_service.GetAllProjects());
        }

        [ClaimsAuthorization(ClaimType = "canReadProjects", ClaimValue = "true")]
        [Route("{id:int}", Name = "GetProjectById")]
        public IHttpActionResult Get(int id)
        {
            var project = _service.GetProject(id);

            if (project != null)
                return Ok(project);
            else
                return NotFound();
        }

        [ClaimsAuthorization(ClaimType = "canReadProjects", ClaimValue = "true")]
        [HttpGet]
        [Route("name/{name}")]
        public IHttpActionResult Get(string name)
        {
            var result = _service.GetProjectsByName(name);
            if (result != null)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }

        [ClaimsAuthorization(ClaimType = "canReadProjects", ClaimValue = "true")]
        [HttpGet]
        [Route("{id}/employees")]
        public IHttpActionResult GetEmployeesInProject(int id)
        {
            var result = _service.GetEmployeesInProjectByProjectId(id);
            if (result != null)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }

        [ClaimsAuthorization(ClaimType = "canReadProjects", ClaimValue = "true")]
        [HttpGet]
        [Route("technology/{technology}")]
        public IHttpActionResult GetProjectsByTechnology(string technology)
        {
            var allProjects = _service.GetProjectsByTechnology(technology);

            if (allProjects != null)
                return Ok(allProjects);

            return NotFound();
        }

        [ClaimsAuthorization(ClaimType = "canReadProjects", ClaimValue = "true")]
        [HttpGet]
        [Route("pagination/technology/{technology}", Name = "GetProjectsByTechnology")]
        public IHttpActionResult GetProjectsByTechnology(string technology, int page = 0, int pageSize = 10)
        {
            var projects = _service.GetProjectsByTechnology(technology);

            if(projects == null)
                return NotFound();

            var dtos = projects.ToList();
            var totalCount = dtos.ToList().Count;
            var pageCount = Math.Ceiling((double)totalCount / pageSize);
            var prevPage = page > 0 ? Url.Link("GetProjectsByTechnology", new { technology = technology, page = page - 1, pageSize = pageSize }) : "";
            var nextPage = page < pageCount - 1 ? Url.Link("GetProjectsByTechnology", new { technology = technology, page = page + 1, pageSize = pageSize }) : "";

            var filtered = dtos.Skip(page * pageSize).Take(pageSize);
            var result = filtered.Select(dto =>
            {
                dto.Url = Url.Link("GetProjectById", new { id = dto.Id });
                return dto;
            }).ToList();

            return Ok(new
            {
                TotalProjects = totalCount,
                TotalPages = pageCount,
                PreviousPage = prevPage,
                NextPage = nextPage,
                Result = result
            });
        }

        [ClaimsAuthorization(ClaimType = "canReadProjects", ClaimValue = "true")]
        [HttpGet]
        [Route("pagination/{name}", Name = "GetProjectsByName")]
        public IHttpActionResult Get(string name, int page = 0, int pageSize = 10)
        {
            var projects = _service.GetProjectsByName(name);

            if (projects == null)
                return NotFound();

            var dtos = projects.ToList();
            var totalCount = dtos.ToList().Count;
            var pageCount = Math.Ceiling((double)totalCount / pageSize);
            var prevPage = page > 0 ? Url.Link("GetProjectByName", new { name = name, page = page - 1, pageSize = pageSize }) : "";
            var nextPage = page < pageCount - 1 ? Url.Link("GetProjectByName", new { name = name, page = page + 1, pageSize = pageSize }) : "";

            var filtered = dtos.Skip(page * pageSize).Take(pageSize);
            var result = filtered.Select(dto =>
            {
                dto.Url = Url.Link("GetProjectById", new { id = dto.Id });
                return dto;
            }).ToList();
            
            return Ok(new
            {
                TotalProjects = totalCount,
                TotalPages = pageCount,
                PreviousPage = prevPage,
                NextPage = nextPage,
                Result = result
            });
        }

        [ClaimsAuthorization(ClaimType = "canWriteProjects", ClaimValue = "true")]
        [HttpPost]
        [Route("")]
        public IHttpActionResult Post(CreateProject dtoProject)
        {
            int createdId = _service.Post(dtoProject);
            return CreatedAtRoute("GetProjectById", new { id = createdId }, dtoProject);
        }

        [ClaimsAuthorization(ClaimType = "canWriteProjects", ClaimValue = "true")]
        [HttpPut]
        [Route("{id}")]
        public IHttpActionResult Edit(int id, CreateProject dtoProject)
        {
                var editedProject = _service.EditProject(dtoProject);
                return Ok(editedProject);
        }

    }
}
