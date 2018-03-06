using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProjectsMap.WebApi.DTOs;
using ProjectsMap.WebApi.Mappers;
using ProjectsMap.WebApi.Repositories.Abstract;
using ProjectsMap.WebApi.Services.Abstract;

namespace ProjectsMap.WebApi.Services.Concrete
{
    public class CompanyService : ICompanyService
    {
        private ICompanyRepository _repository;

        public CompanyService(ICompanyRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<CompanyDto> GetAllCompanies()
        {
            return _repository.Companies.Select(c => DTOMapper.GetCompanyDto(c)).ToList();
        }

        public CompanyDto GetCompany(int id)
        {
            return DTOMapper.GetCompanyDto(_repository.Get(id));
        }

        public int Post(CompanyDto company)
        {
            return _repository.Add(company);
        }

        public void Delete(CompanyDto company)
        {
            throw new NotImplementedException();
        }

        public void Update(CompanyDto company)
        {
            throw new NotImplementedException();
        }
    }
}