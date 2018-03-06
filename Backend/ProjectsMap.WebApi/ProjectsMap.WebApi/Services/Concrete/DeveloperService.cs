using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Web;
using ProjectsMap.WebApi.DTOs;
using ProjectsMap.WebApi.Mappers;
using ProjectsMap.WebApi.Models;
using ProjectsMap.WebApi.Repositories.Abstract;
using ProjectsMap.WebApi.Services.Abstract;

namespace ProjectsMap.WebApi.Services
{
    public class DeveloperService : IDeveloperService
    {
        private IDeveloperRepository _repository;

        public DeveloperService(IDeveloperRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<DeveloperDto> GetAllDevelopers()
        {
            var list = new List<DeveloperDto>();
            foreach (var dev in _repository.Developers)
            {
                list.Add(DTOMapper.GetDeveloperDto(dev));
            }

            return list;
        }

        public DeveloperDto GetDeveloper(int id)
        {
            var developer = _repository.Get(id);
            if (developer == null)
                return null;

            return DTOMapper.GetDeveloperDto(developer);
        }

        public IEnumerable<DeveloperDto> GetDevelopersByTechnology(string technology)
        {
            var list = _repository.Developers.Where(x => x.Technologies.Select(t => t.Name).ToList().Contains(technology)).ToList();

            if (list.Count() > 0)
            {
                var dtoS = list.Select(dev => DTOMapper.GetDeveloperDto(dev)).ToList();
                return dtoS;
            }
            else
            {
                return null;
            }
        }

        public int Post(DeveloperDto developer)
        {
           return  _repository.Add(developer);
        }

        public void Delete(Developer developer)
        {
            _repository.Delete(developer);
        }

        public void Update(Developer developer)
        {
            _repository.Update(developer);
        }
    }
}