namespace Veles.Application.Authentication
{
   using System;
   using System.Linq;
   using System.Threading.Tasks;
   using Microsoft.AspNetCore.Http;
   using Microsoft.Extensions.Caching.Memory;
   using Microsoft.Extensions.Options;
   using Microsoft.Extensions.Primitives;
   using Veles.Application.Authentication.Interfaces;

   public class AccessTokenService : IAccessTokenService
   {
      private readonly IMemoryCache _cache;
      private readonly IHttpContextAccessor _httpContextAccessor;
      private readonly IOptions<JwtOptions> _jwtOptions;

      public AccessTokenService(IHttpContextAccessor httpContextAccessor, IOptions<JwtOptions> jwtOptions, IMemoryCache cache)
      {
         _httpContextAccessor = httpContextAccessor;
         _jwtOptions = jwtOptions;
         _cache = cache;
      }

      public Task<bool> IsCurrentActiveToken()
         => IsActiveAsync(GetCurrentAsync());

      public Task DeactivateCurrentAsync()
         => DeactivateAsync(GetCurrentAsync());

      public Task<bool> IsActiveAsync(string token)
         => Task.FromResult(string.IsNullOrWhiteSpace(_cache.Get<string>(GetKey(token))));

      public Task DeactivateAsync(string token)
      {
         _cache.Set(GetKey(token), "revoked", new MemoryCacheEntryOptions
         {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(_jwtOptions.Value.ExpiryMinutes),
         });

         return Task.CompletedTask;
      }

      private string GetCurrentAsync()
      {
         var authorizationHeader = _httpContextAccessor
            .HttpContext.Request.Headers["authorization"];

         return authorizationHeader == StringValues.Empty
            ? string.Empty
            : authorizationHeader.Single().Split(" ").Last();
      }

      private static string GetKey(string token) => $"tokens:{token}:revoked";
   }
}