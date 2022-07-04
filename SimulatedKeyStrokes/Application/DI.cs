using Application.Contracts;
using Application.Services;
using Autofac;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Application
{
    public static class DI
    {
        public static Assembly Assembly = Assembly.GetExecutingAssembly();

        public static ContainerBuilder RegisterApplication(this ContainerBuilder builder)
        {
            builder
                .RegisterAssemblyTypes(Assembly)
                .AsImplementedInterfaces()
                .InstancePerDependency();

            builder.RegisterType<GameCacheService>().As<IGameCacheService>().SingleInstance();

            return builder;
        }
    }
}
