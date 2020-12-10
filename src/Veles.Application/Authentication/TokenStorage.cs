namespace Veles.Application.Authentication.Interfaces
{
   using System;
   using Microsoft.Extensions.Caching.Memory;
   using Veles.Application.DTO;

   public class TokenStorage : ITokenStorage
   {
      private readonly IMemoryCache _memoryCache;

      public TokenStorage(IMemoryCache memoryCache)
      {
         _memoryCache = memoryCache;
      }

      public void Set(Guid commandId, AuthDto token) => _memoryCache.Set(GetKey(commandId), token);

      public AuthDto Get(Guid commandId) => _memoryCache.Get<AuthDto>(GetKey(commandId));

      private static string GetKey(Guid commandId) => $"users:tokens:{commandId}";
   }
}