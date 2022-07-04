using Application.Contracts;
using Application.Persistance.Repositories;
using Application.Queries.Contracts;
using Autofac;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Application.Persistance
{
    public static class DI
    {
        public static Assembly Assembly = Assembly.GetExecutingAssembly();

        public static ContainerBuilder RegisterApplicationPersistance(this ContainerBuilder builder)
        {
            builder.RegisterContext<ApplicationDbContext>();

            builder.RegisterType<InitialSqlLiteDatabase>().As<IInitialSqlLiteDatabase>().SingleInstance();

            builder.RegisterType<GameKeysQueryRepository>().As<IGameKeysQueryRepository>().InstancePerLifetimeScope();
            builder.RegisterType<GameKeysRepository>().As<IGameKeysRepository>().InstancePerLifetimeScope();
            builder.RegisterType<ValidationRepository>().As<IValidationRepository>().InstancePerLifetimeScope();

            return builder;
        }

        private static void RegisterContext<TContext>(this ContainerBuilder builder) where TContext : DbContext
        {
            builder
                .Register(componentContext =>
                {
                    //var serviceProvider = componentContext.Resolve<IServiceProvider>();
                    var dbContextOptions = new DbContextOptions<TContext>(new Dictionary<Type, IDbContextOptionsExtension>());
                    var optionsBuilder = new DbContextOptionsBuilder<TContext>(dbContextOptions)
                        //.UseApplicationServiceProvider(serviceProvider)
                        .UseSqlite("Data source=SimulatedKeyStrokes.db");

                    return optionsBuilder.Options;

                }).As<DbContextOptions<TContext>>().InstancePerLifetimeScope();

            builder
                .Register(context => context.Resolve<DbContextOptions<TContext>>())
                .As<DbContextOptions>()
                .InstancePerLifetimeScope();

            builder
                .RegisterType<TContext>()
                .AsSelf()
                .InstancePerLifetimeScope();
        }
    }
}
