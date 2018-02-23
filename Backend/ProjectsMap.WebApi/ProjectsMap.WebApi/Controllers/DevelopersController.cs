using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ProjectsMap.WebApi.Models;
using ProjectsMap.WebApi.Repositories.Abstract;

namespace ProjectsMap.WebApi.Controllers
{
    [RoutePrefix("api/developers")]
    public class DevelopersController : ApiController
    {
        private IDeveloperRepository _repository;

        public DevelopersController(IDeveloperRepository developerRepository)
        {
            _repository = developerRepository;
        }

        [Route("")]
        public IHttpActionResult GetAll()
        {
            return Ok( new {results = _repository.Developers});
        }

        [Route("{id:int}")]
        public IHttpActionResult Get(int id)
        {
            var developer = _repository.Get(id);

            if (developer != null)
                return Ok(new {results = _repository.Get(id)});
            else
                return NotFound();


        }
    }
}
