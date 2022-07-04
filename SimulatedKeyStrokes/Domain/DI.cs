using Autofac;
using Domain.MediatR;
using Domain.MediatR.Pipeline;
using MediatR;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Domain
{
    public static class DI
    {
        public static Assembly Assembly = Assembly.GetExecutingAssembly();

        public static ContainerBuilder RegisterDomain(this ContainerBuilder builder, List<Assembly> assemblies)
        {
            builder
                .RegisterAssemblyTypes(Assembly)
                .AsImplementedInterfaces()
                .InstancePerDependency();

            builder
                .RegisterMediatR(assemblies.ToArray());

            builder
                .RegisterGeneric(typeof(ValidationBehavior<,>)).As(typeof(IPipelineBehavior<,>));

            return builder;
        }
    }
}
