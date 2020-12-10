namespace Veles.Infrastructure.Extensions
{
   using System;
   using System.Text;
   using Microsoft.Extensions.Configuration;
   using Microsoft.Extensions.DependencyInjection;
   using Microsoft.IdentityModel.Tokens;
   using Veles.Application.Authentication;
   using Veles.Application.Authentication.Interfaces;
   using Veles.Infrastructure.CQRS.Command;
   using Veles.Infrastructure.CQRS.Command.Interfaces;
   using Veles.Infrastructure.CQRS.Query;
   using Veles.Infrastructure.CQRS.Query.Interfaces;

   public static class Extensions
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

      public static void AddInternalAuthentication(this IServiceCollection services, IConfiguration config)
      {
         
         services.Configure<JwtOptions>(opt => config.GetSection("jwt").Bind(opt));
         services.AddSingleton<IJwtStorage, IJwtStorage>()
            .AddSingleton<IAccessTokenService, AccessTokenService>()
            .AddScoped<IJwtHandler, JwtHandler>();

         var options = config.GetSection("jwt").Get<JwtOptions>();
         services.AddAuthentication().AddJwtBearer(cfg =>
         {
            cfg.TokenValidationParameters = new TokenValidationParameters
            {
               IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.SecretKey)),
               ValidIssuer = options.Issuer,
               ValidAudience = options.ValidAudience,
               ValidateAudience = options.ValidateAudience,
               ValidateLifetime = options.ValidateLifetime
            };
         });

         // Todo add middleware & add config
      }
   }
}