using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using Autofac;
using WeatherApp.Core.Repositories;
using WeatherApp.Core.Services;
using WeatherApp.Infrastructure;
using WeatherApp.Infrastructure.Repositories;
using WeatherApp.Infrastructure.Services;

namespace WeatherApp.Web.App_Start
{
    public class ApplicationModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<WeatherForecastContext>()
                .As<WeatherForecastContext>()
                .InstancePerDependency();

            builder.RegisterType<WeatherForecastRepository>()
                .As<IWeatherForecastRepository>()
                .InstancePerRequest();

            builder.Register(
                    x =>
                        new AccuweatherWeatherSerivce(
                            ConfigurationManager.AppSettings["AccuweatherWeatherService:ApiKey"]))
                .As<IWeatherService>()
                .InstancePerLifetimeScope();

        }
    }
}