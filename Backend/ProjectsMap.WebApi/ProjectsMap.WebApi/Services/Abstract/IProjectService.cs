﻿using ProjectsMap.WebApi.DTOs;
using ProjectsMap.WebApi.DTOs.POST;
using ProjectsMap.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectsMap.WebApi.Services.Abstract
{
    public interface IProjectService
    {
        IEnumerable<ProjectDto> GetProjectsByName(string name);
        ProjectDto GetProject(int id);
        int Post(CreateProject Project);
        void Delete(Project project);
        void Update(Project project);
        IEnumerable<ProjectDto> GetAllProjects();
        IEnumerable<ProjectDto> GetProjectsByTechnology(string technology);
    }
}