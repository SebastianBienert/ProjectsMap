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
    public class TechnologyService : ITechnologyService
    {
        private ITechnologyRepository _repository;

        public TechnologyService(ITechnologyRepository repository)
        {
            _repository = repository;
        }
        
        public IEnumerable<TechnologyDto> GetAllTechnologies()
        {
            var list = new List<TechnologyDto>();

            foreach (var technology in _repository.Technologies)
            {
                list.Add(TechnologyMapper.GeTechnologyDto(technology));
            }
            return list;
        }

        public TechnologyDto GetTechnology(int id)
        {
            var tech = _repository.Get(id);
            if (tech != null)
                return TechnologyMapper.GeTechnologyDto(tech);

                return null;
        }

        public IEnumerable<TechnologyDto> GetTechnologiesByName(string name)
        {
            var list = new List<TechnologyDto>();

            foreach (var technology in _repository.GetTechnologiesByName(name))
            {
                list.Add(TechnologyMapper.GeTechnologyDto(technology));
            }
            return list;
        }

        public int Post(TechnologyDto technology)
        {
            return _repository.Add(technology);
        }

        public void Delete(TechnologyDto technology)
        {
            throw new NotImplementedException();
        }

        public void Update(TechnologyDto technology)
        {
            throw new NotImplementedException();
        }
    }
}