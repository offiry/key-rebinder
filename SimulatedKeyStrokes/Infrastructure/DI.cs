using Autofac;
using Domain.Contracts;
using Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Infrastructure
{
    public static class DI
    {
        public static Assembly Assembly = Assembly.GetExecutingAssembly();

        public static ContainerBuilder RegisterInfrastructure(this ContainerBuilder builder)
        {
            builder
                .RegisterType<FormHotKeyService>()
                .As<IHotKeyService>()
                .InstancePerDependency();

            builder
               .RegisterType<SendKeyService>()
               .As<ISendKeyService>()
               .InstancePerDependency();

            return builder;
        }
    }
}
