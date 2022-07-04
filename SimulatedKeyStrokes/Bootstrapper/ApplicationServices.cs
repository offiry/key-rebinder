using Application;
using Application.Persistance;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Domain;
using Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Bootstrapper
{
    public static class ApplicationServices
    {
        public static Assembly Assembly = Assembly.GetExecutingAssembly();

        public static IServiceProvider RegisterAutofac(this IServiceCollection services)
        {
            return new AutofacServiceProvider(ContainerBuilder());
        }

        public static IContainer ContainerBuilder()
        {
            var builder = new ContainerBuilder();
            var assemblyList = new List<Assembly>
            {
                Assembly,
                Application.DI.Assembly,
                Application.Queries.DI.Assembly,
                Domain.DI.Assembly,
                Infrastructure.DI.Assembly
            };

            builder
                .RegisterApplication()
                .RegisterDomain(assemblyList)
                .RegisterInfrastructure()
                .RegisterApplicationPersistance();

            var container = builder.Build();
            return container;
        }
    }
}
