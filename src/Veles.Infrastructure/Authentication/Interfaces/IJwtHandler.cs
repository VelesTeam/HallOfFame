namespace Veles.Infrastructure.Authentication.Interfaces
{
   using System.Collections.Generic;
   using JsonWebToken = Veles.Infrastructure.Authentication.JsonWebToken;

   public interface IJwtHandler
   {
      JsonWebToken CreateToken(string userId, string role = null, string audience = null, IDictionary<string, string> claims = null);
   }
}