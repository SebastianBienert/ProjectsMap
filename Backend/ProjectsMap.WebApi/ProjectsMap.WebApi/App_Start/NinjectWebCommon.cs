using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Microsoft.Web.Infrastructure.DynamicModuleHelper;
using Ninject;
using Ninject.Web.Common;
using Ninject.Web.Common.WebHost;
using Ninject.Web.WebApi;
using ProjectsMap.WebApi.App_Start;
using ProjectsMap.WebApi.Repositories.Abstract;
using ProjectsMap.WebApi.Repositories.Concrete;
using ProjectsMap.WebApi.Services;
using ProjectsMap.WebApi.Services.Abstract;
using ProjectsMap.WebApi.Services.Concrete;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(NinjectWebCommon), "Stop")]

namespace ProjectsMap.WebApi.App_Start
{
    public static class NinjectWebCommon
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        public static void Start()
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }

        public static void Stop()
        {
            bootstrapper.ShutDown();
        }

        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
            kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();
            RegisterServices(kernel);
            GlobalConfiguration.Configuration.DependencyResolver = new NinjectDependencyResolver(kernel);
            return kernel;
        }
        private static void RegisterServices(IKernel kernel)
        {
            //kernel.Bind<IRepo>().ToMethod(ctx => new Repo("Ninject Rocks!"));
            
            kernel.Bind<IEmployeeRepository>().To<EmployeeRepository>();
            kernel.Bind<IProjectRepository>().To<ProjectRepository>();
            kernel.Bind<IRoomRepository>().To<RoomRepository>();
            kernel.Bind<ISeatRepository>().To<SeatRepository>();
            kernel.Bind<IVertexRepository>().To<VertexRepository>();
            kernel.Bind<ITechnologyRepository>().To<TechnologyRepository>();
            kernel.Bind<ICompanyRepository>().To<CompanyRepository>();

            kernel.Bind<ICompanyService>().To<CompanyService>();
            kernel.Bind<IRoomService>().To<RoomService>();
            kernel.Bind<IEmployeeService>().To<EmployeeService>();
            kernel.Bind<ISeatService>().To<SeatService>();
            kernel.Bind<ITechnologyService>().To<TechnologyService>();
            kernel.Bind<IProjectService>().To<ProjectService>();

        }
    }
}