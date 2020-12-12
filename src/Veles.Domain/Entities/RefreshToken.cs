using System;

namespace Veles.Domain.Entities
{
   using Veles.Domain.Exceptions;

   public class RefreshToken
   {
      public Guid UserId { get; }

      public string Token { get; }

      public bool Revoked => RevokedAt.HasValue;

      public DateTime? RevokedAt { get; private set; }

      public DateTime CreatedAt { get; }

      public DateTime ExpiryTime { get; }


      private RefreshToken(Guid userId, string token, DateTime createdAt, DateTime expiryTime, DateTime? revokedAt = null)
      {
         if(string.IsNullOrWhiteSpace(token))
         {
            throw new DomainException(ErrorCode.InvalidToken, "Token is null or white space");
         }

         UserId = userId;
         Token = token;
         CreatedAt = createdAt;
         ExpiryTime = expiryTime;
         RevokedAt = revokedAt;
      }

      public void Revoke(DateTime revokedAt)
      {
         if(Revoked)
         {
            throw new DomainException(ErrorCode.InvalidToken, "RevokedTime is null");
         }

         RevokedAt = revokedAt;
      }

      public static RefreshToken CreateRefreshToken(Guid userId, string token, DateTime createdAt, DateTime expiryTime, DateTime? revokedAt = null)
         => new RefreshToken(userId, token, createdAt, expiryTime, revokedAt);
   }
}
