using System;
using System.Collections.Generic;
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
    public class DeveloperController : ApiController
    {
        private IDeveloperService _service;

        public DeveloperController(IDeveloperService service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("")]
        public IHttpActionResult GetAll()
        {
            return Ok(_service.GetAllDevelopers());
        }

        [HttpGet]
        [Route("{id:int}", Name = "GetDeveloperById")]
        public IHttpActionResult Get(int id)
        {
            var developerDto = _service.GetDeveloper(id);

            if (developerDto != null)
                return Ok(developerDto);
            else
                return NotFound();
        }

        [HttpGet]
        [Route("technology/{technology}")]
        public IHttpActionResult Get(string technology)
        {
            var result = _service.GetDevelopersByTechnology(technology);
            if (result != null)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }


        [HttpPost]
        [Route("")]
        public IHttpActionResult Post([FromBody] DeveloperDto developer)
        {
            int createdId = _service.Post(developer);

            return CreatedAtRoute("GetDeveloperById", new { id = createdId }, developer);
        }

        [HttpDelete]
        [Route("")]
        public IHttpActionResult Delete(Developer developer)
        {
            _service.Delete(developer);
            return Ok();
        }

        [HttpPut]
        [Route("")]
        public IHttpActionResult Update(Developer developer)
        {
            _service.Update(developer);
            return Ok();
        }


    }
}
