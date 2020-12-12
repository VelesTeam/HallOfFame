namespace Veles.Infrastructure.Authentication
{
   using System;
   using Microsoft.Extensions.Caching.Memory;
   using Veles.Application.DTO;
   using IJwtStorage = Veles.Infrastructure.Authentication.Interfaces.IJwtStorage;

   public class JwtStorage : IJwtStorage
   {
      private readonly IMemoryCache _cache;
      public void Set(Guid commandId, AuthDto authDto) => _cache.Set(GetKey(commandId), authDto);

      public AuthDto Get(Guid commandId) => _cache.Get<AuthDto>(GetKey(commandId));

      private static string GetKey(Guid commandId) => $"users:tokens:{commandId}";
   }
}