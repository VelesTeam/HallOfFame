namespace Veles.Infrastructure.Authentication
{
   using System;
   using System.Collections.Generic;
   using System.IdentityModel.Tokens.Jwt;
   using System.Linq;
   using System.Security.Claims;
   using System.Text;
   using Microsoft.Extensions.Options;
   using Microsoft.IdentityModel.Tokens;
   using Veles.Application.Authentication;
   using IJwtHandler = Veles.Infrastructure.Authentication.Interfaces.IJwtHandler;
   using JwtRegisteredClaimNames = System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames;

   public class JwtHandler : IJwtHandler
   {
      private readonly JwtOptions _options;
      private readonly SigningCredentials _signingCredentials;

      public JwtHandler(IOptions<JwtOptions> jwtOptions)
      {
         _options = jwtOptions.Value;
         var issuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SecretKey));
         _signingCredentials = new SigningCredentials(issuerSigningKey, SecurityAlgorithms.HmacSha256);
      }

      public Infrastructure.Authentication.JsonWebToken CreateToken(string userId, string role = null, string audience = null, IDictionary<string, string> claims = null)
      {
         if(string.IsNullOrWhiteSpace(userId))
         {
            throw new ArgumentException("UserId claim can not be empty", nameof(userId));
         }

         var nowUtc = DateTime.UtcNow;
         var expires = nowUtc.AddMinutes(_options.ExpiryMinutes);
         var jwtClaims = new List<Claim>
         {
            new Claim(JwtRegisteredClaimNames.Sub, userId),
            new Claim(JwtRegisteredClaimNames.UniqueName, userId),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Iat, ToTimestamp(nowUtc).ToString()),
         };

         if(!string.IsNullOrWhiteSpace(role))
         {
            jwtClaims.Add(new Claim(ClaimTypes.Role, role));
         }
         var customClaims = claims?.Select(claim => new Claim(claim.Key, claim.Value)).ToArray()
                            ?? Array.Empty<Claim>();
         jwtClaims.AddRange(customClaims);

         var jwt = new JwtSecurityToken(
            issuer: _options.Issuer,
            claims: jwtClaims,
            notBefore: nowUtc,
            expires: expires,
            signingCredentials: _signingCredentials
         );
         var token = new JwtSecurityTokenHandler().WriteToken(jwt);

         return new Infrastructure.Authentication.JsonWebToken()
         {
            Id = userId,
            AccessToken = token,
            RefreshToken = string.Empty,
            Expires = ToTimestamp(expires),
         };
      }

      private long ToTimestamp(DateTime dateTime) => new DateTimeOffset(dateTime).ToUnixTimeSeconds();
   }
}