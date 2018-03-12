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
            return Ok(_service.GetAllTechnologies().Select(dto =>
            {
                dto.Url = Url.Link("GetTechnologyById", new {id = dto.Id});
                return dto;
            }).ToList());
        }

        [HttpGet]
        [Route("{id:int}", Name = "GetTechnologyById")]
        public IHttpActionResult Get(int id)
        {
            var dto = _service.GetTechnology(id);

            if (dto != null)
            {
                dto.Url = Url.Link("GetTechnologyById", new { id = dto.Id });
                return Ok(dto);
            }
            else
                return NotFound();
        }

        [HttpGet]
        [Route("like/{technology}")]
        public IHttpActionResult Get(string technology)
        {
            return Ok(_service.GetTechnologiesByName(technology).Select(dto =>
            {
                dto.Url = Url.Link("GetTechnologyById", new {id = dto.Id});
                return dto;
            }).ToList());
        }

        [HttpPost]
        [Route("")]
        public IHttpActionResult Post([FromBody] TechnologyDto technology)
        {
            int createdId = _service.Post(technology);
            return CreatedAtRoute("GetTechnologyById", new {id = createdId}, technology);
        }


    }
 }
