using Autofac;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Domain.MediatR
{
    internal static class ContainerBuilderExtensions
    {
        public static ContainerBuilder RegisterMediatR(this ContainerBuilder builder, params Assembly[] assembly)
        {
            builder.RegisterModule(new MediatRModule(assembly));

            return builder;
        }
    }
}
