using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ProjectsMap.WebApi.DTOs;
using ProjectsMap.WebApi.Models;
using ProjectsMap.WebApi.Repositories.Abstract;
using ProjectsMap.WebApi.Services;
using ProjectsMap.WebApi.Services.Abstract;

namespace ProjectsMap.WebApi.Controllers
{
    [RoutePrefix("api/developers")]
    public class EmployeeController : ApiController
    {
        private IEmployeeService _service;
        

        public EmployeeController(IEmployeeService service)
        {
            _service = service;
        }
        
        [HttpGet]
        [Route("pagination")]
        public IHttpActionResult GetAllPaingate(int page = 0, int pageSize = 10)
        {
            var allEmployees = _service.GetAllEmployees().ToList();
            var totalCount = allEmployees.Count;
            var pageCount = Math.Ceiling((double)totalCount / pageSize);
            var prevPage = page > 0 ? Url.Link("Employees", new { page = page - 1, pageSize = pageSize }) : "";
            var nextPage = page < pageCount - 1 ? Url.Link("Employees", new { page = page + 1, pageSize = pageSize }) : "";

            var filtered = allEmployees.Skip(page * pageSize).Take(pageSize);
            var result = filtered.Select(dto =>
            {
                dto.Url = Url.Link("GetEmployeeById", new { id = dto.Id });
                return dto;
            }).ToList();

            return Ok(new
            {
                TotalEmployees = totalCount,
                TotalPages = pageCount,
                PreviousPage = prevPage,
                NextPage = nextPage,
                Result = result
            });
        }


        [HttpGet]
        [Route("")]
        public IHttpActionResult GetAll()
        {
            var allEmployees = _service.GetAllEmployees().ToList();
            var result = allEmployees.Select(dto =>
            {
                dto.Url = Url.Link("GetEmployeeById", new {id = dto.Id});
                return dto;
            }).ToList();

            return Ok(result);
        }
    
        [HttpGet]
        [Route("{id:int}", Name = "GetEmployeeById")]
        public IHttpActionResult Get(int id)
        {
            var developerDto = _service.GetEmployee(id);

            if (developerDto != null)
            {
                developerDto.Url = Url.Link("GetEmployeeById", new {id = developerDto.Id});
                return Ok(developerDto);
            }
                return NotFound();
        }


        [HttpGet]
        [Route("technology/{technology}")]
        public IHttpActionResult Get(string technology)
        {
            var allEmployees = _service.GetDevelopersByTechnology(technology);

            if (allEmployees != null)
                return Ok(allEmployees);

            return NotFound();
        }

        [HttpGet]
        [Route("technology/pagination/{technology}", Name = "GetEmployeesByTechnology")]
        public IHttpActionResult Get(string technology, int page = 0, int pageSize = 10)
        {
            var allEmployees = _service.GetDevelopersByTechnology(technology);

            if (allEmployees == null)
                return NotFound();

            var dtos = allEmployees.ToList();
            var totalCount = dtos.ToList().Count;
            var pageCount = Math.Ceiling((double)totalCount / pageSize);
            var prevPage = page > 0 ? Url.Link("GetEmployeesByTechnology", new {technology = technology, page = page - 1, pageSize = pageSize }) : "";
            var nextPage = page < pageCount - 1 ? Url.Link("GetEmployeesByTechnology", new { technology = technology, page = page + 1, pageSize = pageSize }) : "";

            var filtered = dtos.Skip(page * pageSize).Take(pageSize);
            var result = filtered.Select(dto =>
            {
                dto.Url = Url.Link("GetEmployeeById", new { id = dto.Id });
                return dto;
            }).ToList();

            return Ok(result);
                return Ok(new
                {
                    TotalEmployees = totalCount,
                    TotalPages = pageCount,
                    PreviousPage = prevPage,
                    NextPage = nextPage,
                    Result = result
                });
        }

        [HttpGet]
        [Route("{name}")]
        public IHttpActionResult GetEmployeeByName(string name)
        {
            var allEmployees = _service.GetEmployeesByName(name);

            if (allEmployees != null)
                return Ok(allEmployees);

            return NotFound();
        }

        [HttpGet]
        [Route("pagination/{name}", Name = "GetEmployeesByName")]
        public IHttpActionResult GetEmployeeByName(string name, int page = 0, int pageSize = 10)
        {
            var allEmployees = _service.GetEmployeesByName(name);

            if (allEmployees == null)
                return NotFound();

            var dtos = allEmployees.ToList();
            var totalCount = dtos.ToList().Count;
            var pageCount = Math.Ceiling((double)totalCount / pageSize);
            var prevPage = page > 0 ? Url.Link("GetEmployeesByTechnology", new { name = name, page = page - 1, pageSize = pageSize }) : "";
            var nextPage = page < pageCount - 1 ? Url.Link("GetEmployeesByTechnology", new { name = name, page = page + 1, pageSize = pageSize }) : "";

            var filtered = dtos.Skip(page * pageSize).Take(pageSize);
            var result = filtered.Select(dto =>
            {
                dto.Url = Url.Link("GetEmployeeById", new { id = dto.Id });
                return dto;
            }).ToList();

            return Ok(result);
            return Ok(new
            {
                TotalEmployees = totalCount,
                TotalPages = pageCount,
                PreviousPage = prevPage,
                NextPage = nextPage,
                Result = result
            });
        }


        [HttpPost]
        [Route("")]
        public IHttpActionResult Post([FromBody] EmployeeDto employee)
        {
            int createdId = _service.Post(employee);
            return CreatedAtRoute("GetEmployeeById", new { id = createdId }, employee);
        }

    }
}
