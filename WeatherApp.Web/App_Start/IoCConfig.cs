﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using WeatherApp.Core.Repositories;
using WeatherApp.Core.Services;
using WeatherApp.Infrastructure;
using WeatherApp.Infrastructure.Repositories;
using WeatherApp.Infrastructure.Services;

namespace WeatherApp.Web.App_Start
{
    public static class IoCConfig
    {
        public static void RegisterDependencies()
        {
            var builder = new ContainerBuilder();

            builder.RegisterModule(new ApplicationModule());

            builder.RegisterControllers(typeof(MvcApplication).Assembly)
                   .InstancePerRequest();

            builder.RegisterAssemblyModules(typeof(MvcApplication).Assembly);
            
            builder.RegisterModule<AutofacWebTypesModule>();

            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}