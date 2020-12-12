namespace Veles.Infrastructure.Authentication.Extensions
{
   using System;
   using System.Text;
   using Microsoft.Extensions.Configuration;
   using Microsoft.Extensions.DependencyInjection;
   using Microsoft.IdentityModel.Tokens;
   using Veles.Infrastructure.Authentication.Interfaces;

   public static class AuthenticationExtensions
   {
      public static void AddInternalAuthentication(this IServiceCollection services, IConfiguration config)
      {
         services.Configure<JwtOptions>(opt => config.GetSection("jwt").Bind(opt));
         services.AddSingleton<IJwtStorage, IJwtStorage>()
            .AddSingleton<IAccessTokenService, AccessTokenService>()
            .AddScoped<IJwtHandler, JwtHandler>();

         var options = config.GetSection("jwt").Get<JwtOptions>();
         services.AddAuthentication()
            .AddJwtBearer(cfg =>
            {
               cfg.TokenValidationParameters = new TokenValidationParameters
               {
                  IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.SecretKey)),
                  ValidIssuer = options.Issuer,
                  ValidAudience = options.ValidAudience,
                  ValidateAudience = options.ValidateAudience,
                  ValidateLifetime = options.ValidateLifetime,
                  ClockSkew = TimeSpan.Zero
               };
            });

         // Todo add middleware
      }
   }
}