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
            return Ok( _repository.Developers);
        }

        [Route("{id:int}")]
        public IHttpActionResult Get(int id)
        {
            var developer = _repository.Get(id);

            if (developer != null)
                return Ok(developer);
            else
                return NotFound();
        }

        [Route("technology/{technology}")]
        public IHttpActionResult Get(string technology)
        {
            var list = _repository.Developers.Where(x => x.Technologies.Contains(technology));
            if (list.Count() > 0)
            {
                return Ok(list);
            }
            else
            {
                return NotFound();
            }
        }

        

    }
}
