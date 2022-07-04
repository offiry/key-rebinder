using Autofac;
using MediatR;
using MediatR.Pipeline;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Module = Autofac.Module;

namespace Domain.MediatR
{
    internal class MediatRModule : Module
    {
        private readonly Assembly[] _assembly;

        public MediatRModule(Assembly[] assembly)
        {
            _assembly = assembly ?? throw new ArgumentNullException(nameof(assembly));
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder
                .RegisterAssemblyTypes(typeof(IMediator).GetTypeInfo().Assembly)
                .AsImplementedInterfaces();

            var openHandlerTypes = new[]
            {
                typeof(IRequestHandler<,>),
                typeof(IRequestExceptionHandler<,>),
                typeof(IRequestExceptionAction<,>),
                typeof(INotificationHandler<>),
            };

            foreach (var openHandlerType in openHandlerTypes)
            {
                builder
                    .RegisterAssemblyTypes(_assembly)
                    .AsClosedTypesOf(openHandlerType);
            }

            builder.RegisterGeneric(typeof(RequestPostProcessorBehavior<,>)).As(typeof(IPipelineBehavior<,>));
            builder.RegisterGeneric(typeof(RequestPreProcessorBehavior<,>)).As(typeof(IPipelineBehavior<,>));
            builder.RegisterGeneric(typeof(RequestExceptionActionProcessorBehavior<,>)).As(typeof(IPipelineBehavior<,>));
            builder.RegisterGeneric(typeof(RequestExceptionProcessorBehavior<,>)).As(typeof(IPipelineBehavior<,>));

            builder.Register<ServiceFactory>(outerContext =>
            {
                var innerContext = outerContext.Resolve<IComponentContext>();
                return serviceType => innerContext.Resolve(serviceType);
            });
        }
    }
}
