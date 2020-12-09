namespace Veles.Application.Authentication
{
   using System;
   using Microsoft.Extensions.Caching.Memory;
   using Veles.Application.Authentication.Interfaces;
   using Veles.Application.DTO;

   public class JwtStorage : IJwtStorage
   {
      private readonly IMemoryCache _cache;
      public void Set(Guid commandId, AuthDto authDto) => _cache.Set(GetKey(commandId), authDto);

      public AuthDto Get(Guid commandId) => _cache.Get<AuthDto>(GetKey(commandId));

      private static string GetKey(Guid commandId) => $"users:tokens:{commandId}";
   }
}