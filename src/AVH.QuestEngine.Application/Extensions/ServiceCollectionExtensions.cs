﻿using AVH.QuestEngine.Domain.LifeTime;
using Microsoft.Extensions.DependencyInjection;

namespace AVH.QuestEngine.Application.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddDependencyInjectionAutomatically(this IServiceCollection serviceCollection)
        {
            serviceCollection.Scan(scan => scan
                .FromApplicationDependencies(x =>
                {
                    var assemblyName = x.GetName().Name;
                    return assemblyName != null && assemblyName.StartsWith("AVH.QuestEngine");
                }
                )
                .AddClasses(c => c.AssignableTo<ITransientDependency>())
                    .AsImplementedInterfaces()
                    .WithTransientLifetime()
                .AddClasses(c => c.AssignableTo<IScopedDependency>())
                    .AsImplementedInterfaces()
                    .WithScopedLifetime()
                .AddClasses(c => c.AssignableTo<ISingletonDependency>())
                    .AsImplementedInterfaces()
                    .WithSingletonLifetime()
            );
        }
    }
}
