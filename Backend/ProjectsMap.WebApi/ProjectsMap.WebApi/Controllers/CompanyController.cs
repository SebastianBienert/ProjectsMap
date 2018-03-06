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
    [RoutePrefix("api/company")]
    public class CompanyController : ApiController
    {
        private ICompanyService _service;

        public CompanyController(ICompanyService service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("")]
        public IHttpActionResult GetAll()
        {
            return Ok(_service.GetAllCompanies());
        }

        [HttpGet]
        [Route("{id:int}", Name = "GetCompanyById")]
        public IHttpActionResult Get(int id)
        {
            var companyDto = _service.GetCompany(id);

            if (companyDto != null)
                return Ok(companyDto);
            else
                return NotFound();
        }


        [HttpPost]
        [Route("")]
        public IHttpActionResult Post([FromBody] CompanyDto company)
        {
            int createdId = _service.Post(company);

            return CreatedAtRoute("GetCompanyById", new { id = createdId }, company);
        }

    }
}
