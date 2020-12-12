﻿namespace Veles.Infrastructure.CQRS
{
   using System;
   using Microsoft.Extensions.DependencyInjection;
   using Veles.Infrastructure.CQRS.Command;
   using Veles.Infrastructure.CQRS.Command.Interfaces;
   using Veles.Infrastructure.CQRS.Query;
   using Veles.Infrastructure.CQRS.Query.Interfaces;

   public static class CQRSExtensions
   {
      public static void AddCQRS(this IServiceCollection services)
      {
         services.Scan(scan => scan
            .FromAssemblies(AppDomain.CurrentDomain.GetAssemblies())
            .AddClasses(x => x.AssignableTo(typeof(ICommandHandler<>)))
            .AsImplementedInterfaces()
            .WithTransientLifetime()
         );

         services.Scan(scan => scan
            .FromAssemblies(AppDomain.CurrentDomain.GetAssemblies())
            .AddClasses(x => x.AssignableTo(typeof(IQueryHandler<,>)))
            .AsImplementedInterfaces()
            .WithTransientLifetime()
         );

         services.AddSingleton<ICommandDispatcher, CommandDispatcher>();
         services.AddSingleton<IQueryDispatcher, QueryDispatcher>();
      }
   }
}