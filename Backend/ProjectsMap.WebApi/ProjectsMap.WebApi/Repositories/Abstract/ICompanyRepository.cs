using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectsMap.WebApi.DTOs;
using ProjectsMap.WebApi.Models;

namespace ProjectsMap.WebApi.Repositories.Abstract
{
    public interface ICompanyRepository
    {
        IEnumerable<Company> Companies { get; }

        Company Get(int id);

        int Add(CompanyDto company);

        int Add(Company company);

        void Delete(CompanyDto company);

        void Update(CompanyDto company);
	}
}
