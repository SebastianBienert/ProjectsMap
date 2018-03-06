using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectsMap.WebApi.DTOs;
using ProjectsMap.WebApi.Models;

namespace ProjectsMap.WebApi.Services.Abstract
{
    public interface ICompanyService
    {
        IEnumerable<CompanyDto> GetAllCompanies();

        CompanyDto GetCompany(int id);

        int Post(CompanyDto company);

        void Delete(CompanyDto company);

        void Update(CompanyDto company);
    }
}
