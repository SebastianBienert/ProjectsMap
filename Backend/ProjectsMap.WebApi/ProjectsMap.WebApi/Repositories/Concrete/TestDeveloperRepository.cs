﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProjectsMap.WebApi.Models;
using ProjectsMap.WebApi.Repositories.Abstract;

namespace ProjectsMap.WebApi.Repositories.Concrete
{
    public class TestDeveloperRepository : IDeveloperRepository
    {
        private List<Developer> _developers = new List<Developer>()
        {
            new Developer()
            {
                FirstName = "Witkor",
                Surname = "Bukowski",
                Id = 1,
                Technologies = new List<string>() {"#ObijanieSie", "#NicNieRobienie"}
            },
            new Developer()
            {
                FirstName = "Michal",
                Surname = "Radziwilko",
                Id = 2,
                Technologies = new List<string>() {"#ObijanieSie", "#NicNieRobienie", "#Picie"}
             }
        };

        public IEnumerable<Developer> Developers
        {
            get { return _developers; }
        }

        public Developer Get(int id)
        {
            return Developers.FirstOrDefault(x => x.Id == id);
        }

        public void Add(Developer developer)
        {
            _developers.Add(developer);
        }

        public void Delete(Developer developer)
        {
            _developers.Remove(Get(developer.Id));
        }

        public void Update(Developer developer)
        {
            var dev = _developers.FirstOrDefault(x => x.Id == developer.Id);
            if (dev != null)
            {
                dev.FirstName = developer.FirstName;
                dev.Surname = developer.Surname;
                dev.Technologies = developer.Technologies;
            }

        }
    }
}