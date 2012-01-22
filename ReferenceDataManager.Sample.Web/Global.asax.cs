using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Autofac;
using Autofac.Integration.Mvc;
using ReferenceDataManager.Sample.Web.Models;

namespace ReferenceDataManager.Sample.Web
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "ChangeSet", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );

        }

// ReSharper disable InconsistentNaming
        protected void Application_Start()
// ReSharper restore InconsistentNaming
        {
            var container = CreateContainer();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
        }

        private static IContainer CreateContainer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterSource(new Autofac.Features.ResolveAnything.AnyConcreteTypeNotAlreadyRegisteredSource());
            builder.RegisterControllers(typeof(MvcApplication).Assembly);
            builder.RegisterInstance(GetDataStore());
            return builder.Build();
        }

        private static IDataStore GetDataStore()
        {
            var dataStore = new InMemoryDataStore();
            var firstChangeSet = new UncommittedChangeSet(null, "First change set");
            dataStore.Store(firstChangeSet);
            var secondChangeSet = new UncommittedChangeSet(firstChangeSet.Id, "Second change set");
            dataStore.Store(secondChangeSet);
            dataStore.Store(new UncommittedChangeSet(secondChangeSet.Id, "First alternative branch after second change set"));
            dataStore.Store(new UncommittedChangeSet(secondChangeSet.Id, "Second alternative"));
            return dataStore;
        }
    }
}