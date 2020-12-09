namespace Veles.Application.Authentication.Interfaces
{
   using System.Collections.Generic;
   using JsonWebToken = Veles.Application.Authentication.JsonWebToken;

   public interface IJwtHandler
   {
      JsonWebToken CreateToken(string userId, string role = null, string audience = null, IDictionary<string, string> claims = null);
   }
}