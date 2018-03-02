using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ProjectsMap.WebApi.DTOs;
using ProjectsMap.WebApi.Services.Abstract;

namespace ProjectsMap.WebApi.Controllers
{
    [RoutePrefix("api/technology")]
    public class TechnologyController : ApiController
    {
        private ITechnologyService _service;

        public TechnologyController(ITechnologyService service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("")]
        public IHttpActionResult GetAll()
        {
            return Ok(_service.GetAllTechnologies());
        }

        [HttpGet]
        [Route("{id:int}")]
        public IHttpActionResult Get(int id)
        {
            var dto = _service.GetTechnology(id);

            if (dto != null)
                return Ok(dto);
            else
                return NotFound();
        }

        [HttpGet]
        [Route("like/{technology}")]
        public IHttpActionResult Get(string technology)
        {
            return Ok(_service.GetTechnologiesByName(technology));
        }

        [HttpPost]
        [Route("")]
        public IHttpActionResult Post([FromBody] TechnologyDto technology)
        {
            _service.Post(technology);
            return Ok();
        }


    }
 }
